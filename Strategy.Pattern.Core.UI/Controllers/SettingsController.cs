using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver.Linq;
using Strategy.Pattern.Core.UI.Models;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Strategy.Pattern.Core.UI.Controllers
{
    [Authorize]
    public class SettingsController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public SettingsController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            Settings settings = new Settings();
            if (User.Claims.Where(x => x.Type == Settings.claimDatabaseType).FirstOrDefault() != null)
            {
                settings.DatabaseType = (EDatabaseTypes)int.Parse(User.Claims.First(x => x.Type == Settings.claimDatabaseType).Value);
            }
            else
            {
                settings.DatabaseType = settings.GetDefault;
            }
            return View(settings);
        }
        [HttpPost]
        public async Task<IActionResult> ChangeDatabase(int databaseType)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);  // Kişi Bilgilerini Okuma 
            var newClaim = new Claim(Settings.claimDatabaseType, databaseType.ToString());  // View dan Gelen Veriyi Okuma
            var claims = await _userManager.GetClaimsAsync(user);  // User a Ait Claimleri Okuma
            var hasDatabaseTypeClaim = claims.FirstOrDefault(x => x.Type == Settings.claimDatabaseType); // Claim Kontrolü
            if (hasDatabaseTypeClaim != null)
            {
                await _userManager.ReplaceClaimAsync(user, hasDatabaseTypeClaim, newClaim);  // Claimi Güncelleme
            }
            else
            {
                await _userManager.AddClaimAsync(user, newClaim); // Yeni Claim Oluşturma
            }
            await _signInManager.SignOutAsync();   // Çıkış Yapma
            var authenticateResult = await HttpContext.AuthenticateAsync();  // Cookie ve Token Bilgilerini Okuma
            await _signInManager.SignInAsync(user, authenticateResult.Properties); // Tekrar Giriş Yapma
            return RedirectToAction(nameof(Index));  // Index e Yönelt
        }
    }
}
