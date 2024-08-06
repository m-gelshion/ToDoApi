using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.DataAccess.Abstract;

namespace ToDo.DataAccess.DataModels;

public class ToDoListUserRecord: RecordBase<ToDoListUserRecord>
{
    public int Id { get; set; }

    [Required]
    public int UserId { get; set; }

    [Required]
    public int ToDoListId { get; set; }
}
