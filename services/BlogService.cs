using MongoDB.Bson;
using MongoDB.Driver;
using Orange_Tree.Models;

namespace Orange_Tree.services
{
    public class BlogService(DataContext _context)
    {
        private readonly DataContext context = _context;

        public async Task<List<Blog>> GetBlogsAsync()
        {
            var collection = context.GetCollection();

            return await collection.Find(Builders<Blog>.Filter.Empty)
                .Project<Blog>(Builders<Blog>.Projection
                    .Include(x => x.Title)
                    .Include(x => x.Description))
                .ToListAsync();
        }

        public async Task<Blog?> GetBlogBySlugAsync(string slug)
        {
            var collection = context.GetCollection();
            return await collection.Find(Builders<Blog>.Filter.Eq(x => x.BlogSlug, slug))
                .FirstOrDefaultAsync();
        }

        public async Task<Blog?> GetBlogByIdAsync(string id)
        {
            var collection = context.GetCollection();
            return await collection.Find(Builders<Blog>.Filter.Eq(x => x.Id, new ObjectId(id)))
                .FirstOrDefaultAsync();
        }
    }
}
