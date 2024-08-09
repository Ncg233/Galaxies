using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galaxias.Util;
public struct RectangleF
{
    public float X;
    public float Y;
    public float Width;
    public float Height;
    public RectangleF(float x, float y, float width, float height)
    {
        X = x; Y = y; Width = width; Height = height;   
    }
}
