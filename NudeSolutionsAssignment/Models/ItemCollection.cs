using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace NudeSolutionsAssignment.Model
{
    [BsonIgnoreExtraElements]
    public class ItemCollection
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string collectionId { get; set; }

        [BsonElement("userId")]
        public string userId { get; set; }


        [BsonElement("itemCollection")]
        public Dictionary<string, IList<Item>> categoryItemsMap { get; set; }
    }
}
