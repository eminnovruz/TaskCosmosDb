using TaskApiCosmos.Models;
using TaskApiCosmos.Models.DTO;

namespace TaskApiCosmos.Services.Interfaces
{
    public interface IUserService
    {
        Task<User?> RegisterAsync(RegisterDTO model);
        Task<bool> LoginAsync(LoginDTO model);
        public Task<User?> FindUserByEmailAsync(string email);
    }
}
