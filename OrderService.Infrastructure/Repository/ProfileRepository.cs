using Microsoft.EntityFrameworkCore;
using OrderService.Domain.Aggregates.ProfileAggregate;
using OrderService.Domain.RepositoryContracts;
using OrderService.Infrastructure.Data;

namespace OrderService.Infrastructure.Repository;

public class ProfileRepository : RepositoryAbstract, IProfileRepository
{
    private readonly ApplicationDbContext _applicationDbContext;

    public ProfileRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
        _applicationDbContext = dbContext;
    }

    public async Task<Profile> AddUserAsync(Profile newUser)
    {
        var record =  await _applicationDbContext.Profiles.AddAsync(newUser);
        return record.Entity;
    }

    public async Task<Profile> GetUserAsync(string username)
    {
        return await _applicationDbContext.Profiles.FirstOrDefaultAsync(x => x.Username == username);
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _applicationDbContext.SaveChangesAsync(new CancellationToken());
    }
}
