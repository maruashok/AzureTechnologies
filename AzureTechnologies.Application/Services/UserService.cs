using System.Security.Cryptography;
using System.Text;
using AzureTechnologies.Domain.Entities;
using AzureTechnologies.Domain.Interfaces.Repositiry;
using AzureTechnologies.Domain.Interfaces.Service;
using AzureTechnologies.Domain.Models;

namespace AzureTechnologies.Application.Services
{
    public class UserService : IUserService
    {
        private const int keySize = 64;
        private const int iterations = 350000;
        private readonly IUserRepository repository;
        private HashAlgorithmName hashAlgorithm = HashAlgorithmName.SHA512;

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

        private string HashPasword(string password, out byte[] salt)
        {
            salt = RandomNumberGenerator.GetBytes(keySize);
            var hash = Rfc2898DeriveBytes.Pbkdf2(Encoding.UTF8.GetBytes(password), salt, iterations, hashAlgorithm, keySize);
            return Convert.ToHexString(hash);
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
                        Password = user.Password
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