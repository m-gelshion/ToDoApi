using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.BusinessModels;

namespace ToDo.Servicing;

public interface IToDoItemService
{
    int UserId { get; set; }

    Task<Dictionary<string, string[]>?> Authorize(int itemId);

    Task UpdateItem(ToDoItem item);

    Task DeleteItem(int itemId);

    Task ToggleItemCompletion(int itemId, bool isComplete);
}
