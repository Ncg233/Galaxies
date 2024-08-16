using Galaxias.Client.Main;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galaxias.Client.Render;
public class BackgroundRenderer
{
    private Texture2D layer5;
    private float baseX5;
    public BackgroundRenderer()
    {

    }
    public void Render(IntegrationRenderer renderer, float x, float y, int scaleWidth, int scaleHeight, Color color)
    {
        renderer.Draw("Textures/Skys/back", x - scaleWidth / 2, y - scaleHeight / 2, color: color);

        //float xOffset5 = x * 0.27f;
        //float yOffset5 = 0.7f;
        //float yOffsetHard5 = -120;
        //while (baseX5 - xOffset5 < layer5.Width)
        //{
        //    baseX5 += layer5.Width * 2;
        //}
        //
        //while (baseX5 - xOffset5 >= -layer5.Width)
        //{
        //    baseX5 -= layer5.Width * 2;
        //}
        //renderer.Draw(layer5, x - scaleWidth / 2, y - scaleHeight / 2 - 150
        //    , scaleWidth / 2, scaleHeight / 2, 2, 2, color: color);
    }

    internal void LoadContents()
    {
        layer5 = Main.GalaxiasClient.GetInstance().GetTextureManager().LoadTexture2D("Textures/Skys/layer5");
    }
}
