using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Case3Vitour.Controllers
{
    public class LanguageController : Controller
    {
        [HttpGet]
        public IActionResult SetLanguage(string culture, string returnUrl)
        {
            // Kullanıcının seçtiği dili bir çerez (cookie) olarak tarayıcıya kaydediyoruz.
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );

            // Kullanıcı hangi sayfadaysa dil değiştikten sonra tekrar o sayfaya dönmesini sağlıyoruz.
            return LocalRedirect(returnUrl ?? "/");
        }
    }
}