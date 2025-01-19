using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhoneUserKICB.BLL.DTO;
using PhoneUserKICB.BLL.Interfaces;
using PhoneUserKICB.BLL.Validations;
using PhoneUserKICB.DAL.Repository;

namespace PhoneUserKICB.BLL.Services
{
    public class PhoneService : IUserService
    {
        private readonly IRepository<User> _userRepository;

        public UserService(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAllAsync();
            return users.Select(u => new UserDto
            {
                Id = u.Id,
                Name = u.Name,
                Email = u.Email,
                DateOfBirth = u.DateOfBirth
            });
        }

        public async Task<UserDto> GetUserByIdAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null) throw new ArgumentException("User not found");

            return new UserDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                DateOfBirth = user.DateOfBirth
            };
        }

        public async Task CreateUserAsync(UserDto user)
        {
            if (!EmailValidator.IsValid(user.Email))
                throw new ArgumentException("Invalid email format");

            var entity = new User
            {
                Name = user.Name,
                Email = user.Email,
                DateOfBirth = user.DateOfBirth
            };

            await _userRepository.AddAsync(entity);
        }

        public async Task UpdateUserAsync(UserDto user)
        {
            var existingUser = await _userRepository.GetByIdAsync(user.Id);
            if (existingUser == null) throw new ArgumentException("User not found");

            if (!EmailValidator.IsValid(user.Email))
                throw new ArgumentException("Invalid email format");

            existingUser.Name = user.Name;
            existingUser.Email = user.Email;
            existingUser.DateOfBirth = user.DateOfBirth;

            await _userRepository.UpdateAsync(existingUser);
        }

        public async Task DeleteUserAsync(int id)
        {
            await _userRepository.DeleteAsync(id);
        }
    }
}
