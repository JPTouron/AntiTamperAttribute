
# AntiTamperAttribute
This repository is a somewhat copy of the code posted by @sakopov (Sergey Akopov) over: http://sergeyakopov.com/tamper-proof-hidden-fields-in-asp-net-mvc/

# Why?
I thought the code was very useful and since it helped me out, it could help someone else. Also, by hosting this here, 
people could actually contribute to it

#How
It works simply enough.

> 
 1. download the code
 2. reference the DLL / project
 3. Setup an attribute to your controller's action like this: 
**[ValidateSecureHiddenInputs("Id","Version")]**
public ActionResult DeleteConfirmed([Bind(Include = "Id,Version")] ViewModel model)
    {
    /**some code here...**/
    }    
 4. Reference the namespace *AntiTamperAttribute.Extensions* into the web.config within the views folder
 5. invoke the HTML new methods within a form like so:
**@Html.SecureHiddenFor(x => x.Id)**
**@Html.SecureHiddenFor(x => x.Version)**
@Html.HiddenFor(x => x.Id) //< -- this is your normal hidden input
@Html.HiddenFor(x => x.Version) //< -- this is your normal hidden input
 6. That's it! when you get your data posted into your controller, then the attribute will throw an exception if the data was tampered with.
