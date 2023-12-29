using ReviewsBook_RazorPages.Models;

namespace ReviewsBook_RazorPages.Interfaces
{
    public interface IReviewsRepository
    {
        Task<List<UserReviewVM>> GetReviews();
        Task<List<User>> GetUsers();
        Task<User?> FindUser(int id);
        Task CreateReview(UserReviewVM reviewVM);
        Task<bool> TryToRegister(RegisterVM registerVM);
        Task<int?> TryToLogin(LoginVM loginVM);
    }
}
