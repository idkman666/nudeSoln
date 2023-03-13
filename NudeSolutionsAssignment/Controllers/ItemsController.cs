using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson.IO;
using NudeSolutionsAssignment.Model;
using NudeSolutionsAssignment.Services;
using NudeSolutionsAssignment.ViewModel;
using System.Collections;
using System.Reflection.Metadata;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace NudeSolutionsAssignment.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
     
        readonly IItemCollectionService _listService;

        public ItemsController(IItemCollectionService listService)
        {
            _listService = listService;
        }

        [HttpGet]
        [Route("getAllItemCollection/{userId}")]
        public ActionResult<ItemCollection> GetItemCollection(string userId)
        {
            ItemCollection itemCollection = _listService.GetItemCollection(userId);
            return Ok(itemCollection);
        }

        [HttpPost]
        [Route("addItemCollection/{userId}")]
        public async Task AddItemCollection([FromBody] dynamic itemCollectionData, string userId )
        {           
            await _listService.AddItemCollection(itemCollectionData, userId);
        }


        //this could be in different controller
        [HttpGet]
        [Route("getAllCategories")]
        public ActionResult<List<Category>> GetAllCategories()
        {
            List<Category> categories =  _listService.GetItemCategories();
            return Ok(categories);
        }

        [HttpPost]
        [Route("deleteItemCollection/{collectionId}")]
        public async Task<ActionResult> DeleteItemCollection(string collectionId)
        {
            await _listService.DeleteItemCollection(collectionId); 
            return Ok();
        }

        [HttpPatch]
        [Route("updateItemCollection/{userId}/{collectionId}")]
        public async Task<ActionResult> UpdateItemCollection([FromBody] dynamic itemCollectionData, string collectionId, string userId)
        {            
            await _listService.UpdateItemCollection(itemCollectionData, collectionId, userId);
            return Ok(); 
        }


    }
}
