using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Repositories
{
    public interface ISaleRepository
    {
        Task<Sale> UpdateAsync(Sale sale, CancellationToken cancellationToken = default);

        Task<Sale?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

        Task CreateAsync(Sale sale, CancellationToken cancellationToken = default);

        Task<int> GetCurrentNumber(CancellationToken cancellationToken = default);

        Task<(IEnumerable<Sale?>, int)> FilterAsync(int page, int size, string? order, CancellationToken cancellationToken = default);
    }
}