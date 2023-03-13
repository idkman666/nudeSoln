using Microsoft.AspNetCore.Mvc;
using NudeSolutionsAssignment.Controllers;
using Xunit;
using Moq;
using NudeSolutionsAssignment.Services;
using NudeSolutionsAssignment.Tests.MockData;

using NudeSolutionsAssignment.Model;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace NudeSolutionsAssignment.Tests
{
    public class ItemControllerTest

    {
        [Fact]
        public void Get_Item_Collection()
        {
            var itemCollectionService = new Mock<IItemCollectionService>();
            itemCollectionService.Setup(_ => _.GetItemCollection("11")).Returns(MockData.MockData.GetItemsList);
            var sut = new ItemsController(itemCollectionService.Object);

            var result = sut.GetItemCollection("11");
            
            var actionResult = Assert.IsType<ActionResult<ItemCollection>>(result);
            Assert.IsType<OkObjectResult>(actionResult.Result);
        }
    }
}