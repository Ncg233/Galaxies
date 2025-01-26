using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galaxies.Core.Data;
public abstract class BasicDataPart<T> : IDataPart
{

    protected T Data;
    public BasicDataPart(T data)
    {
        Data = data;
    }
    public BasicDataPart()
    {
        Data = default;
    }
    public virtual T GetData()
    {
        return Data;
    }

    public abstract byte GetId();

    public abstract void Read(BinaryReader reader);

    public abstract void Write(BinaryWriter writer);
}
