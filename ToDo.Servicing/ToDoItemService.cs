using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.BusinessModels;
using ToDo.DataAccess.Repository;

namespace ToDo.Servicing;

public class ToDoItemService(IToDoRepository repository): IToDoItemService
{
    public int UserId { get; set; }

    public async Task<Dictionary<string, string[]>?> Authorize(int itemId)
    {
        var userLists = await repository.GetUsersListsAsync(UserId);
        if (!userLists.Any(l => l.Items.Any(i => i.Id == itemId)))
        {
            return new() { { "List", [$"User '{UserId}' is not authorized to view or modify Item '{itemId}'"] } };
        }
        return null;
    }

    public async Task UpdateItem(ToDoItem item)
        => await repository.UpdateItemAsync(item);

    public async Task DeleteItem(int itemId)
        => await repository.DeleteItemAsync(itemId);

    public async Task ToggleItemCompletion(int itemId, bool isComplete)
        => await repository.ToggleItemCompletionAsync(itemId, isComplete);
}
