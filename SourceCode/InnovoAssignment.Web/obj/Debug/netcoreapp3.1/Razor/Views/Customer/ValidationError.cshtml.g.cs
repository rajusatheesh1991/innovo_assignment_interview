#pragma checksum "D:\Satheesh\GitHub\innovo_assignment_interview\SourceCode\InnovoAssignment.Web\Views\Customer\ValidationError.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "5df7c279599a8451643bb43d7b0e83003cffabec"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Customer_ValidationError), @"mvc.1.0.view", @"/Views/Customer/ValidationError.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "D:\Satheesh\GitHub\innovo_assignment_interview\SourceCode\InnovoAssignment.Web\Views\_ViewImports.cshtml"
using InnovoAssignment.Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\Satheesh\GitHub\innovo_assignment_interview\SourceCode\InnovoAssignment.Web\Views\_ViewImports.cshtml"
using InnovoAssignment.Web.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"5df7c279599a8451643bb43d7b0e83003cffabec", @"/Views/Customer/ValidationError.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"9815129a5cb0bac0a37538be1b4430a0f871f5ee", @"/Views/_ViewImports.cshtml")]
    public class Views_Customer_ValidationError : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 1 "D:\Satheesh\GitHub\innovo_assignment_interview\SourceCode\InnovoAssignment.Web\Views\Customer\ValidationError.cshtml"
 if (ViewBag.Message != null)
{

#line default
#line hidden
#nullable disable
            WriteLiteral("<div class=\"card-footer bg-white\">\r\n        <label class=\"alert-danger\">");
#nullable restore
#line 3 "D:\Satheesh\GitHub\innovo_assignment_interview\SourceCode\InnovoAssignment.Web\Views\Customer\ValidationError.cshtml"
                               Write(ViewBag.Message);

#line default
#line hidden
#nullable disable
            WriteLiteral("</label>\r\n    </div>\r\n");
#nullable restore
#line 5 "D:\Satheesh\GitHub\innovo_assignment_interview\SourceCode\InnovoAssignment.Web\Views\Customer\ValidationError.cshtml"
}

#line default
#line hidden
#nullable disable
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591