using Galaxias.Core.World.Entities;
using Galaxias.Core.World.Tiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;

namespace Galaxias.Core.Render;
public class Camera
{
    public Matrix TransfromMatrix
        => Matrix.CreateTranslation(_pos)
        * Matrix.CreateScale(scale) * 
        Matrix.CreateTranslation(viewWidth / 2, viewHeight / 2, 0);
    public Matrix GuiMatrix => Matrix.CreateScale(guiScale);

    public Vector3 _pos = new();
    private float _zoom = 0.4f, displayRadio, scale, guiScale;
    private int viewWidth, viewHeight;
    private float guiWidth, guiHeight;
    public void Update(Player player, int viewWidth, int viewHeight, float dTime)
    {
        if (false)
        {
            float xTarg = (float)-player.x * GameConstants.TileSize;
            float yTarg = ((float)player.y + 2) * GameConstants.TileSize;
            _pos.X += (xTarg - _pos.X) * dTime * 3; _pos.Y += (yTarg - _pos.Y) * dTime * 3;
        }
        else
        {
            _pos.X = (float)-player.x * GameConstants.TileSize; _pos.Y = ((float)player.y + 2) * GameConstants.TileSize;
        }
        
        if (Keyboard.GetState().IsKeyDown(Keys.OemPlus))
            _zoom += 0.22f * dTime;
        else if (Keyboard.GetState().IsKeyDown(Keys.OemMinus))
            _zoom -= 0.2f * dTime;
         _zoom = MathHelper.Clamp(_zoom, 0.2f, 0.5f);
        scale = displayRadio * _zoom;


    }
    public float GetX()
    {
        return _pos.X;
    }
    public float GetScale()
    {
        return scale; 
    }
    public float GetY()
    {
        return _pos.Y;
    }
    public Vector2 ScreenToWorldSpace(Vector2 screenPoint)
    {
        return Vector2.Transform(screenPoint, Matrix.Invert(TransfromMatrix));
    }
    public Vector2 ScreenToWorldSpace(Point screenPoint)
    {
        Vector2 screenPos = new Vector2(screenPoint.X, screenPoint.Y);
        return Vector2.Transform(screenPos, Matrix.Invert(TransfromMatrix));
    }

    internal void OnResize(int width, int height)
    {
        viewWidth = width;
        viewHeight = height;
        displayRadio = Math.Min(viewWidth / 16f, viewHeight / 9f) * 0.1f;
        guiScale = displayRadio / 2F;
        guiWidth = (width / guiScale);
        guiWidth = (height / guiScale);

    }
}

