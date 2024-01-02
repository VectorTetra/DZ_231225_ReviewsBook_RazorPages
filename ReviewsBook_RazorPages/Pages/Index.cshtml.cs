using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.IdentityModel.Tokens;
using ReviewsBook_RazorPages.Interfaces;
using ReviewsBook_RazorPages.Models;

namespace ReviewsBook_RazorPages.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IReviewsRepository _repo;
        [BindProperty(SupportsGet = true)]
        public int? UserId { get; set; }
        [BindProperty(SupportsGet = true)]
        public string? FirstName { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? LastName { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? Login { get; set; }

        [BindProperty(SupportsGet = true)]
        public UserReviewVM MyReview { get; set; }

        [BindProperty(SupportsGet = true)]
        public ICollection<UserReviewVM> Reviews { get; set; }
        public IndexModel(IReviewsRepository repo)
        {
            _repo = repo;
        }
        public async Task OnGetAsync()
        {
            var UserId = HttpContext.Session.GetInt32("UserId");
            Reviews = await _repo.GetReviews();
            if (UserId != null)
            {
                var User = await _repo?.FindUser((int)UserId);
                if (User != null)
                {
                    FirstName = User.FirstName;
                    LastName = User.LastName;
                    Login = User.Login;
                    this.UserId = UserId; 
                }

            }
        }
        public async Task<IActionResult> OnGetLogoutAsync()
        {
            HttpContext.Session.Clear();
            FirstName = null;
            LastName = null;
            Login = null; 
            UserId = null;
            Reviews = await _repo.GetReviews();
            string url = Url.Page("Index");
            return Redirect(url);
        }

        public async Task<IActionResult> OnPostAsync()
        {   //Если введён хоть какой-то текст, добавить новый отзыв
            if (!MyReview.ReviewText.IsNullOrEmpty())
            {
                MyReview.UserLogin = Login;
                MyReview.ReviewDate = DateTime.Now;
                await _repo.CreateReview(MyReview);
                return RedirectToPage("Index");
            }
            return new OkResult();
        }
    }
}
