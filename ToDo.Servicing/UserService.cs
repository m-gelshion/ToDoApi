using ToDo.BusinessModels;
using ToDo.DataAccess.Repository;

namespace ToDo.Servicing;

public class UserService(IToDoRepository repository) : IUserService
{
    public async Task<List<User>> GetUsers()
        => await repository.GetUsersAsync();
}
