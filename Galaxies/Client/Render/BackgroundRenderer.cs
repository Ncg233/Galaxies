using Galaxies.Client.Resource;
using Galaxies.Core.World.Tiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galaxies.Client.Render;
public class BackgroundRenderer
{
    private Texture2D moutain1;
    private Texture2D moutain2;
    private float baseX5;
    public BackgroundRenderer()
    {

    }
    public void Render(IntegrationRenderer renderer, int scaleWidth, int scaleHeight, Color color)
    {
        int waterlevel = 125;
        float x = Main.GetInstance().GetPlayer().X;
        float y = Main.GetInstance().GetPlayer().Y;
        float offsetY = y - waterlevel;
        renderer.Draw("Textures/Skys/back", x * GameConstants.TileSize, -y * GameConstants.TileSize, scaleWidth / 2f, scaleHeight, color: color);
        renderer.Draw(moutain1, x * GameConstants.TileSize, -(waterlevel + offsetY * 0.8f) * GameConstants.TileSize , scaleWidth / 2f, scaleHeight, color: color);
        renderer.Draw(moutain2, x * GameConstants.TileSize, -(waterlevel + offsetY * 0.6f) * GameConstants.TileSize , scaleWidth / 2f, scaleHeight, color: color);

        //float xOffset5 = x * 0.27f;
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
        moutain1 = TextureManager.LoadTexture2D("Textures/Skys/moutain1");
        moutain2 = TextureManager.LoadTexture2D("Textures/Skys/moutain2");
    }
}
