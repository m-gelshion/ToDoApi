using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDo.DataAccess.Repository;

public static class RepoHelper
{
    public static IServiceCollection AddToDoRepository(this IServiceCollection services)
        => services.AddToDoContext()
            .AddTransient<IToDoRepository, ToDoRepository>();
}
