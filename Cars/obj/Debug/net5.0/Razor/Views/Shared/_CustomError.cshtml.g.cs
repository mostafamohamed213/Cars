#pragma checksum "C:\Users\HUAWEI\Documents\GitHub\Cars\Cars\Views\Shared\_CustomError.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "b174f89b718e056372088772971ee58a3c800ded"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Shared__CustomError), @"mvc.1.0.view", @"/Views/Shared/_CustomError.cshtml")]
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
#line 1 "C:\Users\HUAWEI\Documents\GitHub\Cars\Cars\Views\_ViewImports.cshtml"
using Cars;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\HUAWEI\Documents\GitHub\Cars\Cars\Views\_ViewImports.cshtml"
using Cars.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 1 "C:\Users\HUAWEI\Documents\GitHub\Cars\Cars\Views\Shared\_CustomError.cshtml"
using Microsoft.AspNetCore.Mvc.Localization;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"b174f89b718e056372088772971ee58a3c800ded", @"/Views/Shared/_CustomError.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"e81401665ddb2321f0f7045eb187670fb10a21d3", @"/Views/_ViewImports.cshtml")]
    public class Views_Shared__CustomError : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 3 "C:\Users\HUAWEI\Documents\GitHub\Cars\Cars\Views\Shared\_CustomError.cshtml"
  
    ViewData["Title"] = "View Order";   


#line default
#line hidden
#nullable disable
            WriteLiteral("<div class=\"wrapper vh-100\">\r\n    <div class=\"align-items-center h-100 d-flex w-50 mx-auto\">\r\n        <div class=\"mx-auto text-center\">\r\n            <h1 class=\"display-1 m-0 font-weight-bolder text-muted\" style=\"font-size:80px;\">");
#nullable restore
#line 10 "C:\Users\HUAWEI\Documents\GitHub\Cars\Cars\Views\Shared\_CustomError.cshtml"
                                                                                       Write(localizer["Error"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h1>\r\n            <h3 class=\"mb-1 text-muted font-weight-bold\">");
#nullable restore
#line 11 "C:\Users\HUAWEI\Documents\GitHub\Cars\Cars\Views\Shared\_CustomError.cshtml"
                                                    Write(localizer["ErrorOccurred"]);

#line default
#line hidden
#nullable disable
            WriteLiteral(" </h3>\r\n");
            WriteLiteral("            <a href=\"/\" class=\"btn btn-lg btn-primary px-5\">");
#nullable restore
#line 13 "C:\Users\HUAWEI\Documents\GitHub\Cars\Cars\Views\Shared\_CustomError.cshtml"
                                                       Write(localizer["BackToHomepage"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("</a>\r\n        </div>\r\n    </div>\r\n</div>");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public IViewLocalizer localizer { get; private set; }
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
