using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.DataAccess.Abstract;

namespace ToDo.DataAccess.DataModels;

public class UserRecord: RecordBase<UserRecord>
{
    public int Id { get; set; }

    [Required]
    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;
}
