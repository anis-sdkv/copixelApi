using CopixelApi.Domain.Models;

namespace CopixelApi.Domain.Repositories;

public interface IArtRepository
{
    Task<IEnumerable<Art>> GetAllAsync(CancellationToken cancellationToken = default);
    Task AddAsync(Art art, CancellationToken cancellationToken = default);
    Task UpdateAsync(Art art, CancellationToken cancellationToken = default);
    Task DeleteAsync(Art art, CancellationToken cancellationToken = default);
    Task<Art?> GetUserByIdAsync(string id, CancellationToken cancellationToken = default);
    Task<IEnumerable<Art>> GetArtsByUser(string userId, CancellationToken cancellationToken = default);
}