using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using T1PR2_Client.Tools;

namespace T1PR2_Client.Pages
{
    public class UsersXatModel : PageModel
    {
        public string DisplayName { get; set; } = "Unknown user";
        
        public IActionResult OnGet()
        {
            var token = HttpContext.Session.GetString("AuthToken");
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToPage("/Login");
            }

            if (TokenHelper.IsTokenExpired(token))
            {
                HttpContext.Session.Remove("AuthToken");
                return RedirectToPage("/Login");
            }

            var principal = TokenHelper.GetPrincipalFromToken(token);
            if (principal != null)
            {
                DisplayName = principal.Claims
                                  .FirstOrDefault(c => c.Type == "DisplayName")?.Value 
                              ?? principal.Identity?.Name 
                              ?? "Unknown user";
            }

            return Page();
        }
    }
}
