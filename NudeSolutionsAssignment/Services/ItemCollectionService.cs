using NudeSolutionsAssignment.Data;
using NudeSolutionsAssignment.Model;
using MongoDB.Driver;
using NudeSolutionsAssignment.ViewModel;
using System.Text.Json;

namespace NudeSolutionsAssignment.Services
{
    public class ItemCollectionService : IItemCollectionService
    {
        private readonly IMongoCollection<ItemCollection> _itemCollection;
        private readonly IMongoCollection<Category> _itemCategories;

        public ItemCollectionService(IMongoDbSettings mongoDbSettings, IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase(mongoDbSettings.DatabaseName);
            _itemCollection = database.GetCollection<ItemCollection>(mongoDbSettings.MongoDbCollectionName);
            _itemCategories = database.GetCollection<Category>("itemCategories");
        }

        //get all item collection
        //user userid to get itemcollection by userid
        public ItemCollection GetItemCollection(String userId)
        {
            var textBytes = System.Text.Encoding.UTF8.GetBytes(userId);
            String _userId = Convert.ToBase64String(textBytes);
            List<ItemCollection> itemCollections = _itemCollection.Find(itemCollection => itemCollection.userId == _userId).ToList();
            if (itemCollections.Count == 0 || itemCollections == null)
            {
                return new ItemCollection();
            }
            return itemCollections[0];
        }

        //adding fresh data
        public async Task AddItemCollection(dynamic itemCollectionData, string userId)
        {
            Dictionary<string, IList<Item>> categoryItemMap = JsonSerializer.Deserialize<Dictionary<string, IList<Item>>>(itemCollectionData);
            var textBytes = System.Text.Encoding.UTF8.GetBytes(userId);
            String _userId = Convert.ToBase64String(textBytes);
            //new uid off ip address
            ItemCollection itemCollection = new ItemCollection()
            {
                userId = _userId,
                categoryItemsMap = categoryItemMap
            };
            await _itemCollection.InsertOneAsync(itemCollection);
        }

        public async Task UpdateItemCollection(dynamic itemCollectionData, string collectionId, string userId)
        {
            var textBytes = System.Text.Encoding.UTF8.GetBytes(userId);
            String _userId = Convert.ToBase64String(textBytes);
            Dictionary<string, IList<Item>> categoryItemMap = JsonSerializer.Deserialize<Dictionary<string, IList<Item>>>(itemCollectionData);
            ItemCollection itemCollection = new ItemCollection()
            {
                collectionId = collectionId,
                userId = _userId,
                categoryItemsMap = categoryItemMap
            };
            await _itemCollection.ReplaceOneAsync(itemCol => itemCollection.collectionId == itemCol.collectionId, itemCollection);
        }

        //not implemented 
        public async Task DeleteItemCollection(string collectionId)
        {
            await _itemCollection.DeleteOneAsync(itemCol => itemCol.collectionId == collectionId);
        }

        public List<Category> GetItemCategories()
        {
            List<Category> categories = _itemCategories.Find(itemCategory => true).ToList();
            return categories;
        }

    }
}
