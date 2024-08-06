using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.BusinessModels;

namespace ToDo.DataAccess.Repository;

public interface IToDoRepository
{
    Task<List<ToDoList>> GetUsersListsAsync(int userId);

    Task<int> CreateListAsync(int userId, ToDoList list);

    Task UpdateListAsync(int userId, ToDoList list);

    Task DeleteListAsync(int userId, int listId);

    Task AddItemToListAsync(int listId, ToDoItem item);

    Task UpdateItemAsync(ToDoItem item);

    Task DeleteItemAsync(int itemId);

    Task ToggleItemCompletionAsync(int itemId, bool isComplete);

    Task<List<User>> GetUsersAsync();
}
