using Microsoft.EntityFrameworkCore;
using TaskApiCosmos.Data;
using TaskApiCosmos.Models;
using TaskApiCosmos.Models.DTO;
using TaskApiCosmos.Services.Interfaces;

namespace TaskApiCosmos.Services
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _context;
        private readonly IStorageManager _storageManager;

        public UserService(AppDbContext context, IStorageManager storageManager)
        {
            _context = context;
            _storageManager = storageManager;
        }

        public async Task<User?> FindUserByEmailAsync(string email)
        {
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
                return user;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<bool> LoginAsync(LoginDTO model)
        {
            var user = await FindUserByEmailAsync(model.Email);
            if (user is null)
                return false;

            return BCrypt.Net.BCrypt.Verify(model.Password, user.PasswordHash);
        }

        public async Task<User?> RegisterAsync(RegisterDTO model)
        {
            try
            {
                var passwordHash = BCrypt.Net.BCrypt.HashPassword(model.Password);
                var photoId = await _storageManager.UploadFileAsync(model.ProfilePicture.OpenReadStream(), model.ProfilePicture.FileName, model.ProfilePicture.ContentType);

                var user = new User()
                {
                    Id=Guid.NewGuid().ToString(),
                    Name = model.Name,
                    Surname = model.Surname,
                    Email = model.Email,
                    Age = model.Age,
                    PasswordHash = passwordHash,
                    ProfilePhoto = photoId,
                };

                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();

                return user;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
