using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.DataAccess.Repository;

namespace ToDo.Servicing;

public static class ServiceHelper
{
    public static IServiceCollection AddToDoServices(this IServiceCollection services)
        => services.AddToDoRepository()
            .AddTransient<IUserService, UserService>()
            .AddTransient<IToDoListService, ToDoListService>()
            .AddTransient<IToDoItemService, ToDoItemService>();
}
