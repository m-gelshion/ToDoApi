using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.DataAccess.Abstract;

namespace ToDo.DataAccess.DataModels;

public class ToDoItemRecord: RecordBase<ToDoItemRecord>
{
    public int Id { get; set; }

    [Required]
    public string Title { get; set; } = string.Empty;

    public bool IsComplete { get; set; }

    [Required]
    public int ToDoListId { get; set; }
}
