#pragma checksum "E:\Repository\sadorov_11009\2021\2_kurs\1_Sem\Sem\BeatTim\beattim\Pages\Navbar\BeatsContent.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "d4912a421cf15c2fb44435fdaea067ef58583698"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(BeatTim.Pages.Navbar.Pages_Navbar_BeatsContent), @"mvc.1.0.view", @"/Pages/Navbar/BeatsContent.cshtml")]
namespace BeatTim.Pages.Navbar
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
#line 1 "E:\Repository\sadorov_11009\2021\2_kurs\1_Sem\Sem\BeatTim\beattim\Pages\_ViewImports.cshtml"
using BeatTim;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"d4912a421cf15c2fb44435fdaea067ef58583698", @"/Pages/Navbar/BeatsContent.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"439d0162bace885fda97fccbeb9831291093c81a", @"/Pages/_ViewImports.cshtml")]
    public class Pages_Navbar_BeatsContent : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<BeatTim.Services.DTO.OutputDTO.PublicBeatDto>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("onclick", new global::Microsoft.AspNetCore.Html.HtmlString("playNewBeat($(this))"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("beat_cover play_button h-100 rounded cursor-pointer"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/img/user_beats_covers/default.jpg"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("alt", new global::Microsoft.AspNetCore.Html.HtmlString("?????????????? ????????"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("text-decoration-none text-info cursor-pointer hover-grey"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "E:\Repository\sadorov_11009\2021\2_kurs\1_Sem\Sem\BeatTim\beattim\Pages\Navbar\BeatsContent.cshtml"
 foreach (var beat in Model)
{

#line default
#line hidden
#nullable disable
            WriteLiteral(@"    <div class=""beat flex-grow-1 mt-4"">
        <div
            class=""
                                                                             d-flex
                                                                             flex-row
                                                                             beat-line-container
                                                                             justify-content-between"">
            <div class=""d-flex align-items-center h-100"">
");
#nullable restore
#line 13 "E:\Repository\sadorov_11009\2021\2_kurs\1_Sem\Sem\BeatTim\beattim\Pages\Navbar\BeatsContent.cshtml"
                 if (beat.CoverLink is null)
                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("img", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "d4912a421cf15c2fb44435fdaea067ef585836986096", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n");
#nullable restore
#line 20 "E:\Repository\sadorov_11009\2021\2_kurs\1_Sem\Sem\BeatTim\beattim\Pages\Navbar\BeatsContent.cshtml"
                }
                else
                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <img\r\n                        onclick=\"playNewBeat($(this))\"\r\n                        class=\"beat_cover play_button h-100 rounded cursor-pointer\"");
            BeginWriteAttribute("src", "\r\n                        src=\"", 1180, "\"", 1226, 1);
#nullable restore
#line 26 "E:\Repository\sadorov_11009\2021\2_kurs\1_Sem\Sem\BeatTim\beattim\Pages\Navbar\BeatsContent.cshtml"
WriteAttributeValue("", 1211, beat.CoverLink, 1211, 15, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral("\r\n                        alt=\"?????????????? ????????\"/>\r\n");
#nullable restore
#line 28 "E:\Repository\sadorov_11009\2021\2_kurs\1_Sem\Sem\BeatTim\beattim\Pages\Navbar\BeatsContent.cshtml"
                }

#line default
#line hidden
#nullable disable
            WriteLiteral("                <audio class=\"beat_audio\"");
            BeginWriteAttribute("src", " src=\"", 1335, "\"", 1355, 1);
#nullable restore
#line 29 "E:\Repository\sadorov_11009\2021\2_kurs\1_Sem\Sem\BeatTim\beattim\Pages\Navbar\BeatsContent.cshtml"
WriteAttributeValue("", 1341, beat.BeatLink, 1341, 14, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral("></audio>\r\n                <div class=\"d-flex flex-column justify-content-around ms-5\">\r\n                    <span class=\"title_beat fs-5\">");
#nullable restore
#line 31 "E:\Repository\sadorov_11009\2021\2_kurs\1_Sem\Sem\BeatTim\beattim\Pages\Navbar\BeatsContent.cshtml"
                                             Write(beat.Title);

#line default
#line hidden
#nullable disable
            WriteLiteral("</span>\r\n                    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "d4912a421cf15c2fb44435fdaea067ef585836989392", async() => {
#nullable restore
#line 32 "E:\Repository\sadorov_11009\2021\2_kurs\1_Sem\Sem\BeatTim\beattim\Pages\Navbar\BeatsContent.cshtml"
                                                                                                                                                  Write(beat.UserName);

#line default
#line hidden
#nullable disable
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_4);
            BeginWriteTagHelperAttribute();
#nullable restore
#line 32 "E:\Repository\sadorov_11009\2021\2_kurs\1_Sem\Sem\BeatTim\beattim\Pages\Navbar\BeatsContent.cshtml"
                                                                                      WriteLiteral(UserAccount.PathToPage);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Page = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-page", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Page, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 32 "E:\Repository\sadorov_11009\2021\2_kurs\1_Sem\Sem\BeatTim\beattim\Pages\Navbar\BeatsContent.cshtml"
                                                                                                                             WriteLiteral(beat.UserId);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n                </div>\r\n            </div>\r\n            <div class=\"d-flex align-items-center\">\r\n                <button");
            BeginWriteAttribute("onclick", " onclick=\"", 1806, "\"", 1867, 4);
            WriteAttributeValue("", 1816, "sendVerificationResult($(this),", 1816, 31, true);
#nullable restore
#line 36 "E:\Repository\sadorov_11009\2021\2_kurs\1_Sem\Sem\BeatTim\beattim\Pages\Navbar\BeatsContent.cshtml"
WriteAttributeValue(" ", 1847, beat.BeatId, 1848, 12, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 1860, ",", 1860, 1, true);
            WriteAttributeValue(" ", 1861, "true)", 1862, 6, true);
            EndWriteAttribute();
            WriteLiteral(@" class=""btn btn-outline-success me-4 h-48px"">
                    <svg xmlns=""http://www.w3.org/2000/svg"" width=""22"" fill=""currentColor"" class=""bi bi-check"" viewBox=""0 0 16 16"">
                        <path d=""M10.97 4.97a.75.75 0 0 1 1.07 1.05l-3.99 4.99a.75.75 0 0 1-1.08.02L4.324 8.384a.75.75 0 1 1 1.06-1.06l2.094 2.093 3.473-4.425a.267.267 0 0 1 .02-.022z""/>
                    </svg>
                </button>
                <button");
            BeginWriteAttribute("onclick", " onclick=\"", 2314, "\"", 2376, 4);
            WriteAttributeValue("", 2324, "sendVerificationResult($(this),", 2324, 31, true);
#nullable restore
#line 41 "E:\Repository\sadorov_11009\2021\2_kurs\1_Sem\Sem\BeatTim\beattim\Pages\Navbar\BeatsContent.cshtml"
WriteAttributeValue(" ", 2355, beat.BeatId, 2356, 12, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 2368, ",", 2368, 1, true);
            WriteAttributeValue(" ", 2369, "false)", 2370, 7, true);
            EndWriteAttribute();
            WriteLiteral(@" class=""btn btn-outline-danger me-4 h-48px"">
                    <svg xmlns=""http://www.w3.org/2000/svg"" width=""22"" fill=""currentColor"" class=""bi bi-x"" viewBox=""0 0 16 16"">
                        <path d=""M4.646 4.646a.5.5 0 0 1 .708 0L8 7.293l2.646-2.647a.5.5 0 0 1 .708.708L8.707 8l2.647 2.646a.5.5 0 0 1-.708.708L8 8.707l-2.646 2.647a.5.5 0 0 1-.708-.708L7.293 8 4.646 5.354a.5.5 0 0 1 0-.708z""/>
                    </svg>
                </button>
            </div>
        </div>
    </div>
");
#nullable restore
#line 49 "E:\Repository\sadorov_11009\2021\2_kurs\1_Sem\Sem\BeatTim\beattim\Pages\Navbar\BeatsContent.cshtml"
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<BeatTim.Services.DTO.OutputDTO.PublicBeatDto>> Html { get; private set; }
    }
}
#pragma warning restore 1591
