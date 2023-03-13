namespace NudeSolutionsAssignment.Data
{
    public class MongDbSettings : IMongoDbSettings
    {
        public string MongoDbCollectionName { get; set; } = "itemCollections";
        public string ConnectionString { get; set; } = "mongodb+srv://user:fzffGfKSAbMaZQgp@cluster0.l4e3a.mongodb.net/?retryWrites=true&w=majority";
        public string DatabaseName { get; set; } = "collections";
    }
}
