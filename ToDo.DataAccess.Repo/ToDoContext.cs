using ToDo.DataAccess.DataModels;

namespace ToDo.DataAccess
{
    public interface IToDoContext
    {
        List<ToDoListRecord> ToDoLists { get; set; }

        List<ToDoItemRecord> ToDoItems { get; set; }

        List<UserRecord> Users { get; set; }

        List<ToDoListUserRecord> ToDoListUsers { get; set; }
    }

    public class ToDoContext: IToDoContext
    {
        public ToDoContext()
        {
            Users = [
                new() {
                    Id = 1,
                    FirstName = "Jace",
                    LastName = "Beleren"
                },
                new() {
                    Id = 2,
                    FirstName = "Chandra",
                    LastName = "Nalaar"
                },
                new() {
                    Id = 3,
                    FirstName = "Liliana",
                    LastName = "Vess"
                }
            ];
        }
        public List<ToDoListRecord> ToDoLists { get; set; } = [];

        public List<ToDoItemRecord> ToDoItems { get; set; } = [];

        public List<UserRecord> Users { get; set; } = [];

        public List<ToDoListUserRecord> ToDoListUsers { get; set; } = [];
    }
}
