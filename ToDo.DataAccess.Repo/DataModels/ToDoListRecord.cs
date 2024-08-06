using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using ToDo.DataAccess.Abstract;

namespace ToDo.DataAccess.DataModels;

public class ToDoListRecord: RecordBase<ToDoListRecord>
{  
    public int Id { get; set; }

    [Required]
    public string Title { get; set; } = string.Empty;

    [Required]
    public DateTime CreatedOn { get; set; } = DateTime.Now;

    [Required]
    public int OwnerUserId { get; set; }

    public DateTime? ModifiedOn { get; set; }

    public int? ModifierUserId { get; set; }

    public List<ToDoItemRecord> Items { get; set; } = [];

    public ToDoListUserRecord ListUserRecord { get; set; } = new();
}
