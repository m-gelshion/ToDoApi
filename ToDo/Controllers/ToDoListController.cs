using Microsoft.AspNetCore.Mvc;
using ToDo.BusinessModels;
using ToDo.Servicing;


namespace ToDo.Controllers
{
    [Route("api/users/{userId}/[controller]")]
    [ApiController]
    public class ToDoListController(IToDoListService service) : ControllerBase
    {
        /// <summary>
        /// Get User Lists
        /// </summary>
        /// <remarks>Retrieve all Lists assigned to the provided User</remarks>
        /// <param name="userId">
        /// The User to serve as a filter
        /// </param>
        [HttpGet]
        public async Task<IActionResult> GetUserLists(int userId)
        {
            service.UserId = userId;
            return Ok(await service.GetUserLists());
        }

        /// <summary>
        /// Create List
        /// </summary>
        /// <remarks>Adds a new List to the system</remarks>
        /// <param name="userId">
        /// The User defined as the owner of the List
        /// </param>
        /// <param name="listTitle">
        /// The name for the List
        /// </param>
        [HttpPost]
        public async Task<IActionResult> Post(int userId, [FromBody] string listTitle)
        {
            service.UserId = userId;
            return Ok(await service.CreateList(listTitle));
        }

        /// <summary>
        /// Add Item to List
        /// </summary>
        /// <remarks>
        /// Adds a new Item to the provided List
        /// </remarks>
        /// <param name="userId">
        /// The User authorized to update this List
        /// </param>
        /// <param name="id">
        /// The List identifier
        /// </param>
        /// <param name="itemTitle">
        /// The name for the new Item
        /// </param>
        [HttpPost("{id}/item")]
        public async Task<IActionResult> Post(int userId, int id, [FromBody] string itemTitle)
        {
            service.UserId = userId;

            var authResult = await service.Authorize(id);
            if (authResult != default)
            {
                return BadRequest(authResult);
            }

            return Ok(await service.AddItemToList(id, itemTitle));
        }

        /// <summary>
        /// Update List Title
        /// </summary>
        /// <remarks>Updates the title for a List</remarks>
        /// <param name="userId">
        /// The User authorized to update this List
        /// </param>
        /// <param name="id">
        /// The List identifier
        /// </param>
        /// <param name="listTitle">
        /// The revised List title
        /// </param>
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int userId, int id, [FromBody] string listTitle)
        {
            service.UserId = userId;

            var authResult = await service.Authorize(id);
            if (authResult != default)
            {
                return BadRequest(authResult);
            }

            await service.UpdateList(
                new()
                {
                    Id = id,
                    Title = listTitle
                }
            );
            return new NoContentResult();
        }

        /// <summary>
        /// Delete List
        /// </summary>
        /// <remarks>
        /// Removes a List from the system
        /// </remarks>
        /// <param name="userId">
        /// The User authorized to remove this List
        /// </param>
        /// <param name="id">
        /// The List identifier
        /// </param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int userId, int id)
        {
            service.UserId = userId;

            var authResult = await service.Authorize(id);
            if (authResult != default)
            {
                return BadRequest(authResult);
            }

            await service.DeleteList(id);
            return new NoContentResult();
        }
    }
}
