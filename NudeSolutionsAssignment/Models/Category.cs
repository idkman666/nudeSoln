using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace NudeSolutionsAssignment.Model
{
    [BsonIgnoreExtraElements]
    public class Category
    {
        [BsonId]
        public int Id { get; set; }

        [BsonElement("categoryName")]
        public string CategoryName { get; set; }        

    }
}
