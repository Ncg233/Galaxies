using Galaxies.Core.World.Entities;
using Galaxies.Core.World.Tiles;
using Galaxies.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using SharpDX.Direct3D9;
using System;

namespace Galaxies.Client.Render;
public class Camera
{
    public Matrix TransfromMatrix
        => Matrix.CreateTranslation(_pos)
        * Matrix.CreateScale(scale) *
        Matrix.CreateTranslation(viewWidth / 2, viewHeight / 2, 0);
    public Matrix GuiMatrix => Matrix.CreateScale(guiScale);

    public Vector3 _pos = new();
    private float _zoom = 3f;
    private float displayRadio, guiScale;
    private float scale;
    private int viewWidth, viewHeight;
    public int guiWidth, guiHeight;
    public float worldWidth, worldHeight;
    public void Update(AbstractPlayerEntity player, float dTime)
    {
        if (false)
        {
            Utils.Lerp(ref _pos, new Vector3(-player.X * GameConstants.TileSize, (player.Y + 2) * GameConstants.TileSize, 0), dTime);

        }
        else
        {
            _pos.X = -player.X * GameConstants.TileSize; _pos.Y = (player.Y + 3) * GameConstants.TileSize;
        }

        if (Keyboard.GetState().IsKeyDown(Keys.OemPlus))
        {
            _zoom += dTime;
        }     
        else if (Keyboard.GetState().IsKeyDown(Keys.OemMinus))
        {
            _zoom -= dTime;
        }
        _zoom = MathHelper.Clamp(_zoom, 2.6f, 4);
        scale = _zoom * displayRadio;

        worldWidth = viewWidth / scale;
        worldHeight = viewHeight / scale;
        
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

        displayRadio = Math.Max(viewWidth / 1920f, viewHeight / 1080f);
        
        guiScale = getScaleFactor(3);
        guiWidth = Utils.Ceil(width / guiScale);
        guiHeight = Utils.Ceil(height / guiScale);



    }
    public int getScaleFactor(int guiScale)
    {
        int i;
        for (i = 1; i != guiScale && i < viewWidth && i < viewHeight && viewWidth / (i + 1) >= 320 && viewHeight / (i + 1) >= 240; ++i)
        {

        }

        if (i % 2 != 0)
        {
            ++i;
        }

        return i;
    }
}

