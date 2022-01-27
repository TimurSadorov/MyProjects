using System.Threading.Tasks;
using BeatTim.DTO;
using BeatTim.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BeatTim.Pages.Account
{
    public class EditingProfile : PageModel
    {
        public static readonly string PathToPage = $"/Account/{nameof(EditingProfile)}";
        [BindProperty] public UserProfileDto UserProfile { get; set; }
        [BindProperty] public IFormFile UserPhoto { get; set; }
        public readonly AccountService AccountService;
        public readonly CityService CityService;
        private readonly ImageService _imageService;

        public EditingProfile(AccountService accountService,
            CityService cityService,
            ImageService imageService)
        {
            AccountService = accountService;
            CityService = cityService;
            _imageService = imageService;
        }

        public IActionResult OnGet()
        {
            if (!int.TryParse(HttpContext.Items[nameof(UserToken)]?.ToString(), out _))
                return RedirectToPage(Entry.PathToPage);
            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            if (!int.TryParse(HttpContext.Items[nameof(UserToken)]?.ToString(), out var clientId))
                return RedirectToPage(Entry.PathToPage);
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(nameof(UserProfile), "Введены некорректные данные");
                return Page();
            }

            if (UserPhoto is not null)
            {
                if (_imageService.TryConvertToImage(UserPhoto, out var img))
                    UserProfile.UserPhoto = img;
                else
                {
                    ModelState.AddModelError(nameof(UserPhoto), "Некорректный формат фотографии");
                    return Page();
                }

                if (!_imageService.IsValidImageSize(UserProfile.UserPhoto))
                {
                    ModelState.AddModelError(nameof(UserPhoto), "Некорректный размер фотографии");
                    return Page();
                }   
            }

            UserProfile.ClientId = clientId;
            await AccountService.UpdateUserProfileAsync(UserProfile);

            return RedirectToPage(PathToPage);
        }
    }
}