

using PhoneUserKICB.BLL.DTO;
using PhoneUserKICB.DAL.Entities;

namespace PhoneUserKICB.BLL.Interfaces
{
    /// <summary>
    /// Inerface for user services
    /// </summary>
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> GetAllUsersAsync();
        Task<UserDto> GetUserByIdAsync(int id);
        Task CreateUserAsync(UserDto user);
        Task UpdateUserAsync(UserDto user);
        Task DeleteUserAsync(int id);
        Task<User> GetUserByEmailAsync(string email);
        Task<List<User>> GetFilteredUsersAsync(string searchTerm);
    }
}
