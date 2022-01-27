#pragma checksum "E:\Repository\sadorov_11009\2021\2_kurs\1_Sem\Sem\BeatTim\beattim\Pages\Account\EditingProfile.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "ef8f10fae97059befe3b80782e0709cf64067a1e"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(BeatTim.Pages.Account.Pages_Account_EditingProfile), @"mvc.1.0.razor-page", @"/Pages/Account/EditingProfile.cshtml")]
namespace BeatTim.Pages.Account
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"ef8f10fae97059befe3b80782e0709cf64067a1e", @"/Pages/Account/EditingProfile.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"439d0162bace885fda97fccbeb9831291093c81a", @"/Pages/_ViewImports.cshtml")]
    public class Pages_Account_EditingProfile : global::Microsoft.AspNetCore.Mvc.RazorPages.Page
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 4 "E:\Repository\sadorov_11009\2021\2_kurs\1_Sem\Sem\BeatTim\beattim\Pages\Account\EditingProfile.cshtml"
  
    Layout = "_Layout";
    ViewData["Title"] = "Редактирование профиля";
    if (int.TryParse(HttpContext.Items[nameof(UserToken)]?.ToString(), out var clientId)) 
        Model.UserProfile = await Model.AccountService.GetUserProfileAsync(clientId);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
<div class=""modal fade"" id=""input-img"" tabindex=""-1"">
    <div class=""modal-dialog modal-dialog-centered"">
        <div class=""modal-content bg-dark"">
            <div class=""modal-body border border-dark rounded"">
                <div class=""container-fluid"">
                    <div class=""d-flex flex-column"">
                        <div class=""d-flex justify-content-center p-2 mb-2"">
                            <h5>Загрузка фотографии</h5>
                        </div>
                        <button class=""d-flex bg-dark flex-column align-items-center p-5 border border-2 border-secondary rounded-3 "" id=""btn-download-user-photo"">
                            <svg xmlns=""http://www.w3.org/2000/svg"" width=""80"" height=""80"" fill=""#6c757d"" class=""bi bi-cloud-arrow-down-fill"" viewBox=""0 0 16 16"">
                                <path d=""M8 2a5.53 5.53 0 0 0-3.594 1.342c-.766.66-1.321 1.52-1.464 2.383C1.266 6.095 0 7.555 0 9.318 0 11.366 1.708 13 3.781 13h8.906C14.502 13 16 11.57 16 9.773c0-1.636-1.");
            WriteLiteral(@"242-2.969-2.834-3.194C12.923 3.999 10.69 2 8 2zm2.354 6.854-2 2a.5.5 0 0 1-.708 0l-2-2a.5.5 0 1 1 .708-.708L7.5 9.293V5.5a.5.5 0 0 1 1 0v3.793l1.146-1.147a.5.5 0 0 1 .708.708z""/>
                            </svg>
                            <div class=""text-white"">
                                <h6>Выберете файл</h6>
                                <span>Минимальный размер 300 пикселей и соотношение 1:1</span>
                            </div>
                        </button>
                        <input id=""file-input"" type=""file"" accept=""image/jpeg,image/jpg,image/pjpeg,image/png"" name=""files[]"" hidden=""true"">
                        <div class=""mt-4 align-self-center"">
                            <button type=""button"" class=""btn btn-secondary"" data-bs-dismiss=""modal"" id=""model-closed-btn-user-photo"">Вернуться обратно</button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
<div class=""modal fade"" id");
            WriteLiteral(@"=""error-message-user-photo"" tabindex=""-1"">
    <div class=""modal-dialog"">
        <div class=""modal-content bg-dark"">
            <div class=""model-body d-flex justify-content-center p-3"">
                <span id=""error-message-content"" class=""text-center""></span>
            </div>
        </div>
    </div>
</div>
<div class=""container-xl"">
    <div class=""d-flex flex-column"">
        <div class=""d-flex flex-row"">
");
#nullable restore
#line 51 "E:\Repository\sadorov_11009\2021\2_kurs\1_Sem\Sem\BeatTim\beattim\Pages\Account\EditingProfile.cshtml"
              
                await Html.RenderPartialAsync("UserProfileEditing", Model.UserProfile);
                await Html.RenderPartialAsync("EditBox", Model);
            

#line default
#line hidden
#nullable disable
            WriteLiteral("        </div>\r\n    </div>\r\n</div>\r\n\r\n");
            DefineSection("Scripts", async() => {
                WriteLiteral("\r\n    <script src=\"/js/editingProfile.js\"></script>\r\n");
            }
            );
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<BeatTim.Pages.Account.EditingProfile> Html { get; private set; }
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<BeatTim.Pages.Account.EditingProfile> ViewData => (global::Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<BeatTim.Pages.Account.EditingProfile>)PageContext?.ViewData;
        public BeatTim.Pages.Account.EditingProfile Model => ViewData.Model;
    }
}
#pragma warning restore 1591
