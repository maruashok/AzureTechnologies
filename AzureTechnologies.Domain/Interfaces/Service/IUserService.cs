using AzureTechnologies.Domain.Models;

namespace AzureTechnologies.Domain.Interfaces.Service
{
    public interface IUserService
    {
        Task<int?> AddAsync(UserDTO? user);

        Task<UserDTO?> GetByIdAsync(int? userId);

        Task<bool?> IsEmailExistsAsync(string? userEmail);
    }
}