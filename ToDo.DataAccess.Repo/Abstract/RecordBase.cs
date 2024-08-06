using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDo.DataAccess.Abstract;

public abstract class RecordBase
{
    static readonly Dictionary<Type, List<int>> _usedIds = [];
    static readonly Random _random = new Random();

    protected static int nextId<TRecord>()
        where TRecord : RecordBase<TRecord>
    {
        var nextId = default(int);
        var key = typeof(TRecord);
        _usedIds.TryAdd(key, []);
        while((nextId = _random.Next(1, 100000)) > 0 && !_usedIds[key].Contains(nextId))
        {
            _usedIds[key].Add(nextId);
        }
        return nextId;
    }
}


public abstract class RecordBase<TRecord>: RecordBase
    where TRecord : RecordBase<TRecord>
{
    public static int GenerateNextId()
        => nextId<TRecord>();
}
