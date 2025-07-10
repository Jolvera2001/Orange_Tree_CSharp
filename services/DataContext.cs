using MongoDB.Driver;
using Orange_Tree.Models;

namespace Orange_Tree.services
{
    public class DataContext
    {
        private readonly string? _connectionString = Environment.GetEnvironmentVariable("ORANGE_TREE_URI");
        private readonly string? _databaseName = Environment.GetEnvironmentVariable("OTE_DB_NAME");
        private MongoClient? _mongoClient;
        private IMongoDatabase? _database;

        public DataContext()
        {
            if (string.IsNullOrEmpty(_connectionString))
            {
                throw new InvalidOperationException("Connection string is not set in the environment variables.");
            }

            _mongoClient = new MongoClient(_connectionString);
            _database = _mongoClient.GetDatabase(_databaseName);
        }

        public IMongoCollection<Blog> GetCollection()
        {
            if (_database == null)
            {
                throw new InvalidOperationException("Database is not initialized.");
            }

            return _database.GetCollection<Blog>("blogs");
        }
    }
}
