using Microsoft.AspNetCore.Mvc;
using ToDo.BusinessModels;
using ToDo.Servicing;

namespace ToDo.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController(IUserService service) : Controller
{
    [HttpGet]
    public async Task<IEnumerable<User>> GetUsers()
        => await service.GetUsers();
}
