using Microsoft.AspNetCore.Mvc;
using ToDo.Servicing;

namespace ToDo.Controllers
{
    [Route("api/users/{userId}/[controller]")]
    [ApiController]
    public class ToDoItemController(IToDoItemService service) : ControllerBase
    {
        /// <summary>
        /// Update Item Title
        /// </summary>
        /// <remarks>
        /// This will modify an existing item's title
        /// </remarks>
        /// <param name="userId">
        /// The User authorized to update this Item
        /// </param>
        /// <param name="id">
        /// The Item identifier
        /// </param>
        /// <param name="itemTitle">
        /// The revised title
        /// </param>
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int userId, int id, [FromBody] string itemTitle)
        {
            service.UserId = userId;

            var authResult = await service.Authorize(id); // TODO: Incorporate into middleware?
            if (authResult != default)
            {
                return BadRequest(authResult);
            }

            await service.UpdateItem(
                new()
                {
                    Id = id,
                    Title = itemTitle
                }
            );
            return new NoContentResult();
        }

        /// <summary>
        /// Complete Item
        /// </summary>
        /// <remarks>Flags an Item as complete</remarks>
        /// <param name="userId">
        /// The User authorized to update this Item
        /// </param>
        /// <param name="id">
        /// The Item identifier
        /// </param>
        [HttpPut("{id}/complete")]
        public async Task<IActionResult> PutComplete(int userId, int id)
        {
            service.UserId = userId;

            var authResult = await service.Authorize(id);
            if (authResult != default)
            {
                return BadRequest(authResult);
            }

            await service.ToggleItemCompletion(id, true);
            return new NoContentResult();
        }

        /// <summary>
        /// Clear Item
        /// </summary>
        /// <remarks>Clears the completion status of an Item</remarks>
        /// <param name="userId">
        /// The User authorized to update this Item
        /// </param>
        /// <param name="id">
        /// The Item identifier
        /// </param>
        [HttpPut("{id}/clear")]
        public async Task<IActionResult> PutClear(int userId, int id)
        {
            service.UserId = userId;

            var authResult = await service.Authorize(id);
            if (authResult != default)
            {
                return BadRequest(authResult);
            }

            await service.ToggleItemCompletion(userId, false);
            return new NoContentResult();
        }

        /// <summary>
        /// Delete Item
        /// </summary>
        /// <remarks>Deletes an Item</remarks>
        /// <param name="userId">
        /// The User authorized to update this Item
        /// </param>
        /// <param name="id">
        /// The Item identifier
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

            await service.DeleteItem(id);
            return new NoContentResult();
        }
    }
}
