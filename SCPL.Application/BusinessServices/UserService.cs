using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using SCPL.Application.BusinessInterfaces;
using SCPL.Core;
using SCPL.Core.DBEntities;
using System.Security.Cryptography;
using System.Text;

namespace SCPL.Application.BusinessServices
{
    public class UserService : IUserService
    {
        private readonly InboxContext _dbContext;

        private readonly ILoggingService _loggingService;
        private readonly IJwtTokenService _jwtTokenService;

        public UserService(InboxContext dbContext, ILoggingService loggingService, IJwtTokenService jwtTokenService)
        {
            _dbContext = dbContext;
            _loggingService = loggingService;
            _jwtTokenService = jwtTokenService;
    }

        public UserTable SignUpUser(UserTable user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user), "User cannot be null");
            }

            string hashedPassword = HashPassword(user.UserPassword);
            var existingUser = _dbContext.UserTable_5254
                .FirstOrDefault(u => u.UserEmail == user.UserEmail);

            if (existingUser != null)
            {
                throw new InvalidOperationException("User already exists");
            }

            user.UserPassword = hashedPassword;
            _dbContext.UserTable_5254.Add(user);
            _dbContext.SaveChanges();
            return user;
        }


        public async Task<string> LoginUserAsync(UserTable user, HttpContext context)
        {
            if (user == null || string.IsNullOrEmpty(user.UserEmail) || string.IsNullOrEmpty(user.UserPassword))
            {
                throw new ArgumentException("User email or password is required.");
            }

            string hashedPassword = HashPassword(user.UserPassword);

            var existingUser = await _dbContext.UserTable_5254
                .SingleOrDefaultAsync(item => item.UserEmail == user.UserEmail && item.UserPassword == hashedPassword);

            await _loggingService.LogLoginAttemptAsync(user.UserEmail, existingUser != null, context);

            if (existingUser == null)
            {
                throw new UnauthorizedAccessException("Invalid email or password.");
            }

            return _jwtTokenService.GenerateJwtToken(user.UserEmail);
        }

        private string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                foreach (byte b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}

