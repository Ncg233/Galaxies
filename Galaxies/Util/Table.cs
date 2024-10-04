using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galaxies.Util;
public class Table<A, B, C>
{
    private readonly Dictionary<A, Dictionary<B, C>> table = [];
    public void Put(A col, B row, C value)
    {
        var r = table.GetValueOrDefault(col);
        if (r == null)
        {
            r = [];
            table.Add(col, r);
        }
        r[row] = value;

    }
    public C Get(A col, B row)
    {
        var r = table.GetValueOrDefault(col);
        if (r == null)
        {
            return default;
        }
        return r.GetValueOrDefault(row);
    }
}
