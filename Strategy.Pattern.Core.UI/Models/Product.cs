using MongoDB.Bson.Serialization.Attributes;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Strategy.Pattern.Core.UI.Models
{
    public class Product
    {
        [BsonId] // For MongoDB
        [Key] 
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]  // For MongoDB
        public string Id { get; set; }
        public string Name { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        [BsonRepresentation(MongoDB.Bson.BsonType.Decimal128)]  // For MongoDB
        public decimal Price { get; set; }
        public string UserId { get; set; }
        [BsonRepresentation(MongoDB.Bson.BsonType.DateTime)]  // For MongoDB
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
