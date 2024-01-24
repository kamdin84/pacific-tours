using ccse_cw1.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ccse_cw1.Pages
{
    [Authorize]
    public class UserModel : PageModel
    {
        public readonly UserManager<ApplicationUser> UserManager;
        public ApplicationUser? appUser;
        public UserModel(UserManager<ApplicationUser> userManager)
        {
            UserManager = userManager;
        }
        public void OnGet()
        {
            var task = UserManager.GetUserAsync(User);
            task.Wait();
            appUser = task.Result;
        }
    }


}
