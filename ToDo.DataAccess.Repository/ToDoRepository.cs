using ToDo.BusinessModels;
using ToDo.DataAccess.DataModels;

namespace ToDo.DataAccess.Repository;

public class ToDoRepository(IToDoContext context) : IToDoRepository
{
    public async Task<List<User>> GetUsersAsync()
    {
        var users = context.Users.Select(
            u => new User
            {
                Id = u.Id,
                FirstName = u.FirstName,
                LastName = u.LastName
            }
        ).ToList();
        return await Task.FromResult(users);
    }

    public async Task<User?> GetUserAsync(int userId)
    {
        var userRecord = context.Users.First(u => u.Id == userId);
        var user = default(User);
        if (userRecord != default)
        {
            user = new User
            {
                Id = userRecord.Id,
                FirstName = userRecord.FirstName,
                LastName = userRecord.LastName
            };
        }
        return await Task.FromResult(user);
    }

    public async Task DeleteListAsync(int userId, int listId)
    {
        var list = context.ToDoLists.FirstOrDefault(
            l => l.Id == listId && l.ListUserRecord.UserId == userId
        );
        if (list != default)
        {
            context.ToDoListUsers.Remove(list.ListUserRecord);
            context.ToDoItems.RemoveAll(item => item.ToDoListId == listId);
            context.ToDoLists.Remove(list);
        }
        await Task.CompletedTask;
    }

    public async Task<ToDoList> CreateListAsync(int userId, ToDoList list)
    {
        var listRecord = new ToDoListRecord
        {
            Id = ToDoListRecord.GenerateNextId(),
            Title = list.Title,
            CreatedOn = DateTime.Now,
            OwnerUserId = userId
        };

        var userListRecord = new ToDoListUserRecord
        {
            Id = ToDoListUserRecord.GenerateNextId(),
            ToDoListId = listRecord.Id,
            UserId = userId
        };

        listRecord.ListUserRecord = userListRecord;

        context.ToDoListUsers.Add(userListRecord);
        context.ToDoLists.Add(listRecord);

        list.Id = listRecord.Id;

        return await Task.FromResult(list);
    }

    public async Task<List<ToDoList>> GetUsersListsAsync(int userId)
    {
        var lists = context.ToDoLists.Where(l => l.ListUserRecord.UserId == userId)
            .Select(
                l => new ToDoList
                {
                    Id = l.Id,
                    Title = l.Title,
                    Items = l.Items.Select(
                        item => new ToDoItem
                        {
                            Id = item.Id,
                            Title = item.Title,
                            IsComplete = item.IsComplete
                        }
                    ).ToList()
                }
            ).ToList();
        return await Task.FromResult(lists);
    }

    public async Task<ToDoItem> AddItemToListAsync(int listId, ToDoItem item)
    {
        var itemRecord = new ToDoItemRecord
        {
            Id = ToDoItemRecord.GenerateNextId(),
            Title = item.Title,
            ToDoListId = listId
        };
        var list = context.ToDoLists.FirstOrDefault(l => l.Id == listId);
        if (list != null)
        {
            context.ToDoItems.Add(itemRecord);
            list.Items.Add(itemRecord);
        }

        item.Id = itemRecord.Id;
        return await Task.FromResult(item);
    }

    public async Task UpdateListAsync(int userId, ToDoList list)
    {
        var listRecord = context.ToDoLists.FirstOrDefault(l => l.Id == list.Id);
        if (listRecord != default)
        {
            listRecord.Title = list.Title;
            listRecord.ModifiedOn = DateTime.Now;
            listRecord.ModifierUserId = userId;
        }
        await Task.CompletedTask; 
    }

    public async Task UpdateItemAsync(ToDoItem item)
    {
        var itemRecord = context.ToDoItems.FirstOrDefault(item => item.Id == item.Id);
        if (itemRecord != default)
        {
            itemRecord.Title = item.Title;
            RefreshListItems(itemRecord);
        }
        await Task.CompletedTask;
    }

    private void RefreshListItems(ToDoItemRecord itemRecord)
    {
        var list = context.ToDoLists.FirstOrDefault(l => l.Id == itemRecord.ToDoListId);
        if (list != default)
        {
            var index = list.Items.FindIndex(item => item.Id == itemRecord.Id);
            if (index > -1)
            {
                list.Items[index] = itemRecord;
            }
        }
    }

    public async Task ToggleItemCompletionAsync(int itemId, bool isComplete)
    {
        var itemRecord = context.ToDoItems.FirstOrDefault(item => item.Id == itemId);
        if (itemRecord != default)
        {
            itemRecord.IsComplete = isComplete;
            RefreshListItems(itemRecord);

        }
        await Task.CompletedTask;
    }

    public async Task DeleteItemAsync(int itemId)
    {
        var itemRecord = context.ToDoItems.FirstOrDefault(item => item.Id == itemId);
        if (itemRecord != default)
        {
            var list = context.ToDoLists.FirstOrDefault(l => l.Id == itemRecord.ToDoListId);
            if (list != default)
            {
                var index = list.Items.FindIndex(item => item.Id == itemId);
                if (index > -1)
                {
                    list.Items.RemoveAt(index);
                    context.ToDoItems.Remove(itemRecord);
                }
            }
        }
        await Task.CompletedTask;
    }
}