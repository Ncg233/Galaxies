using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace Galaxies.Util;
public class JsonUtils
{
    public static T GetValue<T>(JObject o, string key)
    {
        return o.GetValue(key).ToObject<T>();
    }
    public static bool TryGetValue<T>(JObject o, string key, out T value)
    {
        return TryGetValue(o, key, out value, default);
    }
    public static bool TryGetValue<T>(JObject o, string key, out T value, T defaultValue)
    {
        if(o.TryGetValue(key, out var v))
        {
            value = v.ToObject<T>();
            return true;
        }
        value = defaultValue;
        return false;
    }
}

