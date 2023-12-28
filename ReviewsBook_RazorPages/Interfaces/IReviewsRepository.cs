using ReviewsBook_RazorPages.Models;

namespace ReviewsBook_RazorPages.Interfaces
{
    public interface IReviewsRepository
    {
        Task<List<Review>> GetReviews();
        Task<List<User>> GetUsers();
        Task<User?> FindUser(int id);
        Task CreateReview(Review reviewData);
        Task<bool> TryToRegister(User registerData);
        Task<string?> TryToLogin(User loginData);
    }
}
