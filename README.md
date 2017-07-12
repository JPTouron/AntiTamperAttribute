<h1 id="antitamperattribute">AntiTamperAttribute</h1>

<p>This repository is a somewhat copy of the code posted by @sakopov (Sergey Akopov) over: <a href="http://sergeyakopov.com/tamper-proof-hidden-fields-in-asp-net-mvc/">http://sergeyakopov.com/tamper-proof-hidden-fields-in-asp-net-mvc/</a></p>



<h1 id="why">Why?</h1>

<p>I thought the code was very useful and since it helped me out, it could help someone else. Also, by hosting this here,  <br>
people could actually contribute to it</p>



<h1 id="how">How</h1>

<p>It works simply enough.</p>

<blockquote>
  <ol>
  <li>download the code</li>
  <li>reference the DLL / project</li>
  <li>Setup an attribute to your controller’s action like this:  <br>
  <strong>[ValidateSecureHiddenInputs(“Id”,”Version”)]</strong> <br>
  public ActionResult DeleteConfirmed([Bind(Include = “Id,Version”)] ViewModel model) <br>
  { <br>
  /<strong>some code here…</strong>/ <br>
  }    </li>
  <li>Reference the namespace <em>AntiTamperAttribute.Extensions</em> into the web.config within the views folder</li>
  <li>invoke the HTML new methods within a form like so: <br>
  <strong>@Html.SecureHiddenFor(x =&gt; x.Id)</strong> <br>
  <strong>@Html.SecureHiddenFor(x =&gt; x.Version)</strong> <br>
  @Html.HiddenFor(x =&gt; x.Id) //&lt; – this is your normal hidden input <br>
  @Html.HiddenFor(x =&gt; x.Version) //&lt; – this is your normal hidden input</li>
  <li>That’s it! when you get your data posted into your controller, then the attribute will throw an exception if the data was tampered with.</li>
  </ol>
</blockquote>
