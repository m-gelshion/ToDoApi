using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.BusinessModels;
using ToDo.DataAccess.Repository;

namespace ToDo.Servicing;

public class ToDoListService(IToDoRepository repository) : IToDoListService
{
    public async Task<Dictionary<string, string[]>?> Authorize(int listId)
    {
        var userLists = await repository.GetUsersListsAsync(UserId);
        if (userLists.Count == 0)
        {
            return new() { { "List", [$"User '{UserId}' is not authorized to view or modify List '{listId}'"] } };
        }
        return null;
    }

    public required int UserId { get; set; }

    public async Task<ToDoItem> AddItemToList(int listId, string itemTitle)
    {
        var item = new ToDoItem
        {
            Title = itemTitle
        };
        return await repository.AddItemToListAsync(listId, item);
    }

    public async Task<ToDoList> CreateList(string listTitle)
    {
        var list = new ToDoList
        {
            Title = listTitle,
        };
        return await repository.CreateListAsync(UserId, list);
    }

    public async Task UpdateList(ToDoList list)
        => await repository.UpdateListAsync(UserId, list);

    public async Task DeleteList(int listId)
        => await repository.DeleteListAsync(UserId, listId);

    public async Task<List<ToDoList>> GetUserLists()
        => await repository.GetUsersListsAsync(UserId);
}
