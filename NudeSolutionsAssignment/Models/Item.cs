using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace NudeSolutionsAssignment.Model
{
    [BsonIgnoreExtraElements]
    public class Item
    {
        [BsonId]
        public string itemId { get; set; }

        [BsonElement("itemName")]
        public string itemName { get; set; }

        [BsonElement("price")]
        public float price { get; set; }

        //[BsonElement("categoryId")]
        //public int CategoryId {get; set; }

        //[BsonElement("categoryName")]
        //public string CategoryName { get; set; }
    }
}
