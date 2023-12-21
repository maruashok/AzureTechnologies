using AzureTechnologies.Domain.Entities;

namespace AzureTechnologies.Domain.Interfaces.Repositiry
{
    public interface IUserRepository : IRepository<User>
    {
        Task<int?> AddUserAsync(User user);

        Task<bool> IsEmailExistsAsync(string? emailId);
    }
}