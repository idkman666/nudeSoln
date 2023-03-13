namespace NudeSolutionsAssignment.Data
{
    public interface IMongoDbSettings
    {
        string MongoDbCollectionName { get; set; }
        string ConnectionString { get; set; }   
        string DatabaseName { get; set; }   
    }
}
