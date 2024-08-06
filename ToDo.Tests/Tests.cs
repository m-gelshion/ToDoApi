using Moq;
using FluentAssertions;
using ToDo.BusinessModels;
using ToDo.DataAccess.Repository;
using ToDo.Servicing;

namespace ToDo.Tests
{
    [TestClass]
    public class Tests
    {
        
        [TestMethod]
        public async Task CreateListTestMethod()
        {
            var userId = 1;
            var mockRepo = new Mock<IToDoRepository>();

            mockRepo.Setup(
                repo => repo.CreateListAsync(It.IsAny<int>(), It.IsAny<ToDoList>())
            ).ReturnsAsync(new ToDoList { Id = 1, Title = "List 1" });

            var service = new ToDoListService(mockRepo.Object) { UserId = userId };

            var result = await service.CreateList("List 1");

            result.Should()
                .NotBeNull();
            result.Id.Should()
                .Be(1);
            result.Title.Should()
                .Be("List 1");
        }

        [TestMethod]
        public async Task AddItemToListTestMethod()
        {
            var userId = 1;
            var listId = 1;
            var mockRepo = new Mock<IToDoRepository>();

            mockRepo.Setup(
                repo => repo.AddItemToListAsync(It.IsAny<int>(), It.IsAny<ToDoItem>())
            ).ReturnsAsync(new ToDoItem { Id = 1, Title = "Item 1" });

            var service = new ToDoListService(mockRepo.Object) { UserId = userId };

            var result = await service.AddItemToList(listId, "Item 1");

            result.Should()
                .NotBeNull();
            result.Id.Should()
                .Be(1);
            result.Title.Should()
                .Be("Item 1");
        }
    }
}