using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ReviewsBook_RazorPages.Interfaces;
using ReviewsBook_RazorPages.Models;
using System.Security.Cryptography;
using System.Text;

namespace ReviewsBook_RazorPages.Repositories
{
    public class ReviewsRepository:IReviewsRepository
    {
        private readonly ReviewsContext _context;
        public ReviewsRepository(ReviewsContext context)
        {
            _context = context;
        }
        public async Task<List<Review>> GetReviews()
        {
            var col = await _context.Reviews.ToListAsync();
            //List<Review> res = new();
            //foreach (var item in col)
            //{
            //    var Uuser = await _context.Users.FindAsync(item.UserId);
            //    UserReviewVM appitem = new()
            //    {
            //        UserLogin = Uuser.Login,
            //        ReviewText = item.ReviewText,
            //        ReviewDate = item.ReviewDate
            //    };
            //    res.Add(appitem);
            //}
            return col;
        }
        public async Task<List<User>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }
        public async Task<User?> FindUser(int id)
        {
            return await _context.Users.FindAsync(id);
        }
        public async Task CreateReview(Review reviewData)
        {
            var uuser = await _context.Users.FirstOrDefaultAsync(x => x.Id == reviewData.UserId);
            Review review = new Review
            {
                UserId = uuser.Id,
                ReviewText = reviewData.ReviewText,
                ReviewDate = DateTime.Now
            };
            await _context.Reviews.AddAsync(review);
            await _context.SaveChangesAsync();
        }
        public async Task<bool> TryToRegister(User registerData)
        {
            var IsFinded = await _context.Users.FirstOrDefaultAsync(x => x.Login == registerData.Login);
            // Якщо такого користувача знайдено, значить, такий логін вже зайнято
            if (IsFinded != null)
            {
                return false;
            }
            // Якщо такого користувача немає - зареєструвати і повернути true
            else
            {
                User user = new User();
                user.FirstName = registerData.FirstName;
                user.LastName = registerData.LastName;
                user.Login = registerData.Login;

                byte[] saltbuf = new byte[16];

                RandomNumberGenerator randomNumberGenerator = RandomNumberGenerator.Create();
                randomNumberGenerator.GetBytes(saltbuf);

                StringBuilder sb = new StringBuilder(16);
                for (int i = 0; i < 16; i++)
                    sb.Append(string.Format("{0:X2}", saltbuf[i]));
                string salt = sb.ToString();

                //переводим пароль в байт-массив  
                byte[] password = Encoding.Unicode.GetBytes(salt + registerData.Password);

                //создаем объект для получения средств шифрования  
                var md5 = MD5.Create();

                //вычисляем хеш-представление в байтах  
                byte[] byteHash = md5.ComputeHash(password);

                StringBuilder hash = new StringBuilder(byteHash.Length);
                for (int i = 0; i < byteHash.Length; i++)
                    hash.Append(string.Format("{0:X2}", byteHash[i]));

                user.Password = hash.ToString();
                user.Salt = salt;
                _context.Users.Add(user);
                _context.SaveChanges();
                return true;
            }
        }
        public async Task<string?> TryToLogin(User loginData)
        {
            if (_context.Users.Count() == 0) return null;
            var user = await _context.Users.SingleOrDefaultAsync(x => x.Login.Equals(loginData.Login));
            if (user == null)
            {
                return null;
            }
            else
            {
                string? salt = user.Salt;

                //переводим пароль в байт-массив  
                byte[] password = Encoding.Unicode.GetBytes(salt + loginData.Password);

                //создаем объект для получения средств шифрования  
                var md5 = MD5.Create();

                //вычисляем хеш-представление в байтах  
                byte[] byteHash = md5.ComputeHash(password);

                StringBuilder hash = new StringBuilder(byteHash.Length);
                for (int i = 0; i < byteHash.Length; i++)
                    hash.Append(string.Format("{0:X2}", byteHash[i]));

                if (user.Password != hash.ToString())
                {
                    return null;
                }
                return $"{user.Id}";
            }
        }
    }
}
