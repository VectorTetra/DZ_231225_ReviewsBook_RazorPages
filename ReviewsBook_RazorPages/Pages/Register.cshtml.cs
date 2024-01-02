using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ReviewsBook_RazorPages.Interfaces;
using ReviewsBook_RazorPages.Models;

namespace ReviewsBook_RazorPages.Pages
{
    public class RegisterModel : PageModel
    {
        private readonly IReviewsRepository _repo;
        public RegisterModel(IReviewsRepository repo)
        {
            _repo = repo;
        }
        [BindProperty(SupportsGet = true)]
        public RegisterVM RegisterVM { get; set; }
        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || RegisterVM == null)
            {
                return Page();
            }
            var isSuccessfulReg = await _repo.TryToRegister(RegisterVM);
            if (!isSuccessfulReg)
            {
                ModelState.AddModelError("", "Такий логін вже зайнятий!");
                return Page();
            }
            return RedirectToPage("Login");
        }
    }
}
