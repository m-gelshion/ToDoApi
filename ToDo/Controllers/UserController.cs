using Microsoft.AspNetCore.Mvc;
using ToDo.BusinessModels;
using ToDo.Servicing;

namespace ToDo.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController(IUserService service) : Controller
{
    /// <summary>
    /// Get Users
    /// </summary>
    /// <remarks>Retrieves the system Users</remarks>
    [HttpGet]
    public async Task<IEnumerable<User>> GetUsers()
        => await service.GetUsers();
}
