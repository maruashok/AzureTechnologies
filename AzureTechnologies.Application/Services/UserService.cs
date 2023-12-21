using AzureTechnologies.Domain.Entities;
using AzureTechnologies.Domain.Interfaces.Repositiry;
using AzureTechnologies.Domain.Interfaces.Service;
using AzureTechnologies.Domain.Models;

namespace AzureTechnologies.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository repository;

        public UserService(IUserRepository repository)
        {
            this.repository = repository;
        }

        public async Task<int?> AddAsync(UserDTO? user)
        {
            if (user != null)
            {
                var userDO = new User()
                {
                    Email = user.Email,
                    FullName = user.FullName,
                    Password = BCrypt.Net.BCrypt.HashPassword(user.Password)
                };

                return await repository.AddUserAsync(userDO);
            }

            return null;
        }

        public async Task<UserDTO?> GetByIdAsync(int? userId)
        {
            if (userId.HasValue && userId.GetValueOrDefault(0) > 0)
            {
                var user = await repository.GetByIDAsync(userId);

                if (user != null)
                {
                    return new UserDTO()
                    {
                        Email = user.Email,
                        FullName = user.FullName,
                    };
                }
            }

            return default;
        }

        public async Task<bool?> IsEmailExistsAsync(string? userEmail)
        {
            if (!string.IsNullOrEmpty(userEmail))
            {
                return await repository.IsEmailExistsAsync(userEmail);
            }

            return default;
        }
    }
}