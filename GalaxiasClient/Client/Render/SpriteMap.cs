using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientGalaxias.Client.Render;
public class SpriteMap
{
    public Texture2D SourceTexture { get; private set; }
    private Rectangle[] sourceRect;
    public int Width;
    public int Height;
    public SpriteMap(Texture2D source, int row, int col)
    {
        SourceTexture = source;
        sourceRect = new Rectangle[row * col];
        Width = source.Width / col;
        Height = source.Height / row;
        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < col; j++)
            {
                sourceRect[i * row + j] = new Rectangle(i * Height, j * Width, Width, Height);
            }
        }
    }

    public Rectangle GetSourceRect(int pos)
    {
        return sourceRect[pos];
    }
}
