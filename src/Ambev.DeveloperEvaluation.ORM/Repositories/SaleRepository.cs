using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using MongoDB.Driver;

namespace Ambev.DeveloperEvaluation.ORM.Repositories
{
    public class SaleRepository(MongoClient mongoClient) : ISaleRepository
    {
        private IMongoCollection<Sale> _saleCollection = mongoClient.GetDatabase("DeveloperEvaluation").GetCollection<Sale>("Sales");

        public async Task CreateAsync(Sale sale, CancellationToken cancellationToken = default)
        {
            await _saleCollection.InsertOneAsync(sale);
        }

        public async Task<(IEnumerable<Sale?>, int)> FilterAsync(int page, int size, string? order, CancellationToken cancellationToken = default)
        {
            bool descending = !string.IsNullOrEmpty(order) && order.Trim().EndsWith("desc");
            var field = string.IsNullOrEmpty(order) ? "Number" : order.Replace("asc", "").Replace("desc", "").Trim();

            var filter = Builders<Sale>.Filter.Empty;

            var sort = descending
                ? Builders<Sale>.Sort.Descending(field)
                : Builders<Sale>.Sort.Ascending(field);

            var sales = await _saleCollection
                .Find(filter)
                .Sort(sort)
                .Skip((page - 1) * size)
                .Limit(size)
                .ToListAsync();

            return (sales, (int)await _saleCollection.CountDocumentsAsync(filter));
        }

        public async Task<Sale?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var query = await _saleCollection.FindAsync(x => x.Id == id);
            return query.FirstOrDefault();
        }

        public async Task<int> GetCurrentNumber(CancellationToken cancellationToken = default)
        {
            var query = await _saleCollection.FindAsync(x => true);
            var lastSale = query.ToList().LastOrDefault();
            return lastSale?.Number + 1 ?? 1;
        }

        public async Task<Sale> UpdateAsync(Sale sale, CancellationToken cancellationToken = default)
        {
            var options = new FindOneAndReplaceOptions<Sale> { ReturnDocument = ReturnDocument.Before };
            return await _saleCollection.FindOneAndReplaceAsync(x => x.Id == sale.Id, sale, options);
        }
    }
}