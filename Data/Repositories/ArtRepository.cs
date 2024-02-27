using CopixelApi.Domain.Models;
using CopixelApi.Domain.Repositories;

namespace CopixelApi.Data.Repositories;

public class ArtRepository : IArtRepository
{
    private static readonly List<Art> ArtDb = new();

    public Task<IEnumerable<Art>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return Task.FromResult(ArtDb.AsEnumerable());
    }

    public Task AddAsync(Art art, CancellationToken cancellationToken = default)
    {
        ArtDb.Add(art);
        return Task.CompletedTask;
    }

    public Task UpdateAsync(Art art, CancellationToken cancellationToken = default)
    {
        for (var i = 0; i < ArtDb.Count; i++)
            if (ArtDb[i].Id == art.Id)
            {
                ArtDb[i] = art;
                return Task.CompletedTask;
            }

        throw new Exception();
    }

    public Task DeleteAsync(Art art, CancellationToken cancellationToken = default)
    {
        for (var i = 0; i < ArtDb.Count; i++)
            if (ArtDb[i].Id == art.Id)
            {
                ArtDb.RemoveAt(i);
                return Task.CompletedTask;
            }

        throw new Exception();
    }

    public Task<Art?> GetUserByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(ArtDb.Find(art => art.Id == id));
    }

    public Task<IEnumerable<Art>> GetArtsByUser(string userId, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(ArtDb.Where(x => x.UserId == userId));
    }
}