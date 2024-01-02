using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ReviewsBook_RazorPages.Interfaces;
using ReviewsBook_RazorPages.Models;

namespace ReviewsBook_RazorPages.Pages
{
    public class LoginModel : PageModel
    {
        private readonly IReviewsRepository _repo;
        public LoginModel(IReviewsRepository repo)
        {
            _repo = repo;
        }
        [BindProperty]
        public LoginVM LoginVM { get; set; } = default!;
        
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || LoginVM == null)
            {
                ModelState.AddModelError("", "Неправильний логін або пароль!");
                return Page();
            }
            var LoginedUserId = await _repo.TryToLogin(LoginVM);
            if (LoginedUserId == null)
            {
                ModelState.AddModelError("", "Неправильний логін або пароль!");
                return Page();
            }
            else
            {
                HttpContext.Session.SetInt32("UserId", (int)LoginedUserId);
                string url = Url.Page("Index");
                return Redirect(url);
                //LoginedUserId
                
            }
        }
    }
}
