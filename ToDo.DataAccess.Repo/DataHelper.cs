using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDo.DataAccess;

public static class DataHelper
{
    public static IServiceCollection AddToDoContext(this IServiceCollection services)
        => services.AddSingleton<IToDoContext, ToDoContext>();
}
