using Microsoft.AspNetCore.Mvc;
using ToDo.Servicing;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

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
        /// The User authorized to update this item
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

            var authResult = await service.Authorize(id);
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
