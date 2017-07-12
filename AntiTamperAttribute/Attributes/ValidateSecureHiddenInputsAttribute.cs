using AntiTamperAttribute.Exceptions;
using System;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Security;

namespace AntiTamperAttribute.Attributes
{
    /// <summary>
    /// <para>class to extend Html helper in order to provide scurization inputs</para>
    /// extracted from:http://sergeyakopov.com/tamper-proof-hidden-fields-in-asp-net-mvc/
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public sealed class ValidateSecureHiddenInputsAttribute : FilterAttribute, IAuthorizationFilter
    {
        private readonly string[] properties;

        public ValidateSecureHiddenInputsAttribute(params string[] properties)
        {
            if (properties == null || !properties.Any())
                throw new ArgumentException("properties");

            this.properties = properties;
        }

        public void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext == null)
                throw new ArgumentNullException("filterContext");

            this.properties.ToList().ForEach(property => Validate(filterContext, property));
        }

        private static void Validate(AuthorizationContext filterContext, string property)
        {
            var protectedValue = filterContext.HttpContext.Request.Form[string.Format("__{0}Token", property)];
            var decodedValue = Convert.FromBase64String(protectedValue);

            var decryptedValue = MachineKey.Unprotect(decodedValue, "Protected Hidden Input Token");

            if (decryptedValue == null)
                throw new HttpSecureHiddenInputException("A required security token was not supplied or was invalid.");

            protectedValue = Encoding.Unicode.GetString(decryptedValue);

            var originalValue = filterContext.HttpContext.Request.Form[property];

            var identity = filterContext.HttpContext.User.Identity;

            if (!string.IsNullOrEmpty(identity.Name))
                originalValue = string.Format("{0}_{1}", identity.Name, originalValue);

            if (!protectedValue.Equals(originalValue))
                throw new HttpSecureHiddenInputException("A required security token was not supplied or was invalid.");
        }
    }
}