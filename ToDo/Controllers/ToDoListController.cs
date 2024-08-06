using Microsoft.AspNetCore.Mvc;
using ToDo.Servicing;


namespace ToDo.Controllers
{
    [Route("api/users/{userId}/[controller]")]
    [ApiController]
    public class ToDoListController(IToDoListService service) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetUserLists(int userId)
        {
            service.UserId = userId;
            return Ok(await service.GetUserLists());
        }

        [HttpPost]
        public async Task<int> Post(int userId, [FromBody] string listTitle)
        {
            service.UserId = userId;
            return await service.CreateList(listTitle);
        }

        [HttpPost("{id}/item")]
        public async Task<IActionResult> Post(int userId, int id, string itemTitle)
        {
            service.UserId = userId;

            var authResult = await service.Authorize(id);
            if (authResult != default)
            {
                return BadRequest(authResult);
            }

            await service.AddItemToList(id, itemTitle);
            return new NoContentResult();
        }

        [HttpPut]
        public async Task<IActionResult> Put(int userId, int id, string listTitle)
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
