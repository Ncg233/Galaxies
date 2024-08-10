using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientGalaxias.Client.Render;
public class BackgroundRenderer
{
    public void Render(IntegrationRenderer renderer,float x, float y, int scaleWidth, int scaleHeight, Color color)
    {
        renderer.Draw("Textures/Skys/back", x - scaleWidth / 2, y - scaleHeight / 2, color: color);
    }
}
