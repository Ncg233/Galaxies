using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galaxies.Core.Data;
public interface IDataPart
{
    public byte GetId();
    public void Write(BinaryWriter writer);
    public void Read(BinaryReader reader);
}
