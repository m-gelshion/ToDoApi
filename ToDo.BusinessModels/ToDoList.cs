using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDo.BusinessModels;

public class ToDoList
{
    public int Id { get; set; }

    [Required]
    public string Title { get; set; } = string.Empty;

    public List<ToDoItem> Items { get; set; } = [];

    public bool IsComplete
        => Items.Count > 0 && Items.All(item => item.IsComplete);
}
