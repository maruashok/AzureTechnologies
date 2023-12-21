using AzureTechnologies.Domain.Entities;
using AzureTechnologies.Domain.Interfaces.Repositiry;
using AzureTechnologies.Infrastructure.Data;

namespace AzureTechnologies.Infrastructure.Repository
{
    public class UserRepository(AzureTechContext context) : Repository<User>(context), IUserRepository
    {
        public Task<bool> IsEmailExistsAsync(string? emailId)
        {
            return IsExistsAsync(user => user.Email.Equals(emailId));
        }

        public async Task<int?> AddUserAsync(User user)
        {
            await InsertAsync(user);
            if (await SaveChangesAsync() > 0)
            {
                return user.Id;
            }

            return default;
        }
    }
}