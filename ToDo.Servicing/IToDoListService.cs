using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.BusinessModels;

namespace ToDo.Servicing;

public interface IToDoListService
{
    int UserId { get; set; }

    Task<Dictionary<string, string[]>?> Authorize(int listId);

    Task<int> CreateList(string listTitle);

    Task<List<ToDoList>> GetUserLists();

    Task UpdateList(ToDoList list);

    Task DeleteList(int listId);

    Task AddItemToList(int listId, string itemTitle);
}
