using ToDo.BusinessModels;

namespace ToDo.Servicing;

public interface IToDoListService
{
    int UserId { get; set; }

    Task<Dictionary<string, string[]>?> Authorize(int listId);

    Task<ToDoList> CreateList(string listTitle);

    Task<List<ToDoList>> GetUserLists();

    Task UpdateList(ToDoList list);

    Task DeleteList(int listId);

    Task<ToDoItem> AddItemToList(int listId, string itemTitle);
}
