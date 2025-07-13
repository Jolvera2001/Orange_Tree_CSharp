using MongoDB.Bson;
using MongoDB.Driver;
using Orange_Tree.Models;

namespace Orange_Tree.services
{
    public class BlogService(IMongoClient client)
    {
        private readonly string? _databaseName = Environment.GetEnvironmentVariable("OTE_DB_NAME");
        
        public async Task<List<Blog>> GetBlogsAsync()
        {
            var collection = GetBlogCollection();
            return await collection.Find(Builders<Blog>.Filter.Empty)
                .Project<Blog>(Builders<Blog>.Projection
                    .Include(x => x.Title)
                    .Include(x => x.Description)
                    .Include(x => x.BlogSlug))
                .ToListAsync();
        }

        public async Task<Blog?> GetBlogBySlugAsync(string slug)
        {
            var collection = GetBlogCollection();
            return await collection.Find(Builders<Blog>.Filter.Eq(x => x.BlogSlug, slug))
                .FirstOrDefaultAsync();
        }

        public async Task<Blog?> GetBlogByIdAsync(string id)
        {
            var collection = GetBlogCollection();
            return await collection.Find(Builders<Blog>.Filter.Eq(x => x.Id, new ObjectId(id)))
                .FirstOrDefaultAsync();
        }

        private IMongoCollection<Blog> GetBlogCollection()
        {
            return client.GetDatabase(_databaseName).GetCollection<Blog>("blogs");
        }
    }
}
