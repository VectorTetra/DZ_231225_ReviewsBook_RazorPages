using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ReviewsBook_RazorPages.Interfaces;
using ReviewsBook_RazorPages.Models;

namespace ReviewsBook_RazorPages.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IReviewsRepository _repo;
        //[BindProperty(SupportsGet = true)]
        //public int? UserId { get; set; }

        //[BindProperty(SupportsGet = true)]
        //public string? FirstName { get; set; }

        //[BindProperty(SupportsGet = true)]
        //public string? LastName { get; set; }

        //[BindProperty(SupportsGet = true)]
        //public string? Login { get; set; }


        [BindProperty(SupportsGet = true)]
        public ICollection<Review> Reviews { get; set; }
        public IndexModel(IReviewsRepository repo)
        {
            _repo = repo;
        }
        public async Task<string> GetUserLogin(int userId)
        {
            var user = await _repo.FindUser(userId);
            return user.Login.ToString();
        }
        //public async Task OnGetCheckUser(string userid)
        //{
        //    Reviews = await _repo.GetReviews();

        //    int intid = Convert.ToInt32(userid);
        //    var User = await _repo?.FindUser(intid);
        //    if (User != null) 
        //    {
        //        FirstName = User.FirstName;
        //        LastName = User.LastName;
        //        Login = User.Login;
        //    }
            
        //}
        public async Task OnGetAsync()
        {
            Reviews = await _repo.GetReviews();
        }
    }
}
