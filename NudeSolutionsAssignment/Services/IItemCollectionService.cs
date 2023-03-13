using NudeSolutionsAssignment.Model;

namespace NudeSolutionsAssignment.Services
{
    public interface IItemCollectionService
    {
        Task AddItemCollection(dynamic itemCollectionData, string userId);
        Task DeleteItemCollection(string collectionId);
        List<Category> GetItemCategories();
        ItemCollection GetItemCollection(string userId);
        Task UpdateItemCollection(dynamic itemCollectionData, string collectionId, string userId);
    }
}