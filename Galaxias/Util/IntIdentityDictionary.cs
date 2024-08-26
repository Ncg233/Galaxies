using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galaxias.Util;
public class IntIdentityDictionary<T> : IEnumerable<T>
{
    private int nextId;
    private readonly Dictionary<T, int> map;
    private readonly List<T> list;
    public IntIdentityDictionary()
    {
        list = new List<T>(256);
        map = new Dictionary<T, int>(256);
    }
    public void Put(T key, int value)
    {
        map[key] = value;
        list.Add(key);
    }
    public void Add(T key)
    {
        Put(key, nextId++);
    }
    public int Get(T key)
    {
        return map.GetValueOrDefault(key, -1);
    }
    public T Get(int value)
    {
        return value >= 0 ? list[value] : default;
    }
    public IEnumerator<T> GetEnumerator()
    {
        throw new NotImplementedException();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        throw new NotImplementedException();
    }
}
