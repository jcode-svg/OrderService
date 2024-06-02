using OrderService.Domain.Aggregates.ProfileAggregate;

namespace OrderService.Domain.RepositoryContracts;

public interface IProfileRepository
{
    Task<Profile> AddUserAsync(Profile newUser);
    Task<Profile> GetUserAsync(string username);
    Task<int> SaveChangesAsync();
}
