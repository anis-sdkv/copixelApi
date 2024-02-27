using CopixelApi.Domain.Models;
using CopixelApi.Domain.Repositories;

namespace CopixelApi.Data.Repositories;

public class UserRepository : IUserRepository
{
    public static readonly List<User> UsersDb = new List<User>();

    public Task<IEnumerable<User>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return Task.FromResult(UsersDb.AsEnumerable());
    }

    public Task AddAsync(User user, CancellationToken cancellationToken = default)
    {
        UsersDb.Add(user);
        return Task.CompletedTask;
    }

    public Task UpdateAsync(User user, CancellationToken cancellationToken = default)
    {
        for (var i = 0; i < UsersDb.Count; i++)
            if (UsersDb[i].Id == user.Id)
            {
                UsersDb[i] = user;
                break;
            }

        return Task.CompletedTask;
    }

    public Task DeleteAsync(User user, CancellationToken cancellationToken = default)
    {
        for (var i = 0; i < UsersDb.Count; i++)
            if (UsersDb[i].Id == user.Id)
            {
                UsersDb.RemoveAt(i);
                break;
            }
        return Task.CompletedTask;
    }

    public Task<User?> GetUserByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(UsersDb.Find(user => user.Id == id));
    }

    public Task<User?> GetUserByNameAsync(string normalizedName, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(UsersDb.Find(user => user.NormalizedUserName == normalizedName));
    }
}