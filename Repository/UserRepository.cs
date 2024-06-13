using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using asp_net_ecommerce_api.Configurations;
using asp_net_ecommerce_api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace asp_net_ecommerce_api.Repository
{


    public interface IUserRepository
    {
        string GenerateJwtToken(User user);
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User> GetUserByUsernameAsync(string username);
        Task<User> CreateUserAsync(User user);
        string HashPassword(string password);
        bool IsValidPassword(string providedPassword, string hashedPassword);
        // You can add more methods for update, delete, etc.
    }
    public class UserRepository: IUserRepository
    {

        private readonly ApplicationDbContext _context;
        private readonly JwtSettings _jwtSettings;

        public UserRepository(ApplicationDbContext context, IOptions<JwtSettings> jwtSettings)
            {
                _context = context;
            _jwtSettings = jwtSettings.Value ?? throw new ArgumentNullException(nameof(jwtSettings));
        }
        public async Task<IEnumerable<User>> GetAllUsersAsync(){
            return await _context.Users.ToListAsync();
        } 
        public async Task<User> GetUserByUsernameAsync(string username) {
                    return await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
        }
        public async Task<User> CreateUserAsync(User user){
            user.Password = HashPassword(user.Password);
          _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public string GenerateJwtToken(User user)        {
            // Ensure the JWT key is at least 256 bits (32 bytes) long for HMAC-SHA256 algorithm
            if (string.IsNullOrEmpty(_jwtSettings.Key) || _jwtSettings.Key.Length < 32)
            {
                // Generate a new key if the current one is null, empty, or too short
                var newKey = GenerateRandomKey(32); // Generate a 256-bit (32-byte) random key
                _jwtSettings.Key = Convert.ToBase64String(newKey); // Convert the key to a string for storage
                // Save the new key to your configuration or wherever it's stored
            }

            if (string.IsNullOrEmpty(_jwtSettings.Key))
            {
                throw new ArgumentNullException(nameof(_jwtSettings.Key), "JWT Key is null or empty.");
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var keyBytes = Convert.FromBase64String(_jwtSettings.Key); // Convert the key string back to bytes
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, user.Username)
                }),
                Expires = DateTime.UtcNow.AddMinutes(_jwtSettings.ExpiresInMinutes),
                Issuer = _jwtSettings.Issuer,
                Audience = _jwtSettings.Audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keyBytes), SecurityAlgorithms.HmacSha256)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private byte[] GenerateRandomKey(int lengthInBytes){
            byte[] key = new byte[lengthInBytes];
            using (var rng = new System.Security.Cryptography.RNGCryptoServiceProvider())
            {
                rng.GetBytes(key);
            }
            return key;
        }  
        public  string HashPassword(string password) {
                using (var sha256 = SHA256.Create())
                {
                    var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                    return Convert.ToBase64String(hashedBytes);
                }
        } 
        public  bool IsValidPassword(string providedPassword, string hashedPassword)
            {
                // Hash the provided password using the same method used during registration
                var hashedProvidedPassword = HashPassword(providedPassword);

                // Compare the hashed provided password with the hashed password stored in the database
                return hashedProvidedPassword == hashedPassword;
        }

    }
}