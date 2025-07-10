using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Data;

namespace Orange_Tree.Models
{
    public class Blog
    {
        public ObjectId Id { get; set; }

        [BsonElement("title")]
        public string Title { get; set; }

        [BsonElement("description")]
        public string Description { get; set; }

        [BsonElement("blog_slug")]
        public string BlogSlug { get; set; }

        [BsonElement("content")]
        public string Content { get; set; }
        
        [BsonElement("created_at")]
        public DateTime CreatedAt { get; set; }
        
        [BsonElement("updated_at")]
        public DateTime UpdatedAt { get; set; }
    }
}
