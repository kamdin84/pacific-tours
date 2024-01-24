using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ccse_cw1.Pages
{
    [Authorize(Roles = "admin")]

    public class AdminModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
