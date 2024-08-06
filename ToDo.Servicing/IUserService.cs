using ToDo.BusinessModels;

namespace ToDo.Servicing;

public interface IUserService
{
    Task<List<User>> GetUsers();
}
