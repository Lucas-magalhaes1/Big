using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Big.Data;
using Big.Models;

namespace Big.Services
{
    public class AuthService
    {
        private readonly ApplicationDbContext _context;
        private readonly JwtService _jwtService;

        public AuthService(ApplicationDbContext context, JwtService jwtService)
        {
            _context = context;
            _jwtService = jwtService;
        }

        public async Task<string?> LoginAsync(string email, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null || !VerifyPassword(password, user.PasswordHash))
            {
                return null; 
            }

            var roles = new List<string> { "User" }; 
            return _jwtService.GenerateToken(user.Id.ToString(), user.UserName, roles);
        }

        public async Task<bool> RegisterAsync(string userName, string email, string password)
        {
            if (await _context.Users.AnyAsync(u => u.Email == email))
            {
                return false; 
            }

            var passwordHash = HashPassword(password);
            var newUser = new ApplicationUser
            {
                UserName = userName,
                Email = email,
                PasswordHash = passwordHash 
            };

            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ResetPasswordAsync(string email, string newPassword)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null)
            {
                return false; 
            }

            user.PasswordHash = HashPassword(newPassword);

            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return true;
        }

        private static string HashPassword(string password)
        {
            using var hmac = new HMACSHA256();
            var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(hash); 
        }

        private static bool VerifyPassword(string password, string storedHash)
        {
            var hashToCompare = HashPassword(password); 
            return hashToCompare == storedHash; 
        }
    }
}
