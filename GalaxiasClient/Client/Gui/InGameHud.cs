using ClientGalaxias.Client.Key;
using ClientGalaxias.Client.Main;
using ClientGalaxias.Client.Render;
using Galasias.Core.World.Inventory;
using Galaxias.Core.World.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace ClientGalaxias.Client.Gui;
public class InGameHud
{
    private readonly string _heartTexture = "Textures/Gui/heart";
    private readonly string _heartHalfTexture = "Textures/Gui/heart_half";
    private bool debug;
    private GalaxiasClient _client;
    private FrameCounter _frameCounter = new();
    public InGameHud(GalaxiasClient galaxias)
    {
        _client = galaxias;

    }
    public void Render(IntegrationRenderer renderer, int width, int height, float dTime)
    {
        Player player = _client.GetPlayer();

        _frameCounter.Update(dTime);
        if (KeyBind.DeBug.IsKeyDown())
        {
            if (debug == false) debug = true;
            else debug = false;
        }
        if (debug == true)
        {
            RenderString(renderer, "X:" + Math.Round(_client.GetPlayer().x, 1), 0, 0);
            RenderString(renderer, "Y:" + Math.Round(_client.GetPlayer().y, 1), 0, 6);
            RenderString(renderer, "FPS:" + Math.Round(_frameCounter.AverageFramesPerSecond, 1).ToString(), 0f, 12);
            RenderString(renderer, "Speed:" + Math.Round(Math.Sqrt(_client.GetPlayer().vx * _client.GetPlayer().vx + _client.GetPlayer().vy * _client.GetPlayer().vy), 1), 0, 18);
        }

        int health = (int)Math.Round(_client.GetPlayer().health / 10);
        for (int i = 4; i >= 0; i--)
        {
            if (health > 0)
            {
                health -= 2;
                if (health < 0)
                {
                    renderer.Draw(_heartHalfTexture, width / 2 - 58 - i * 8, 25, Color.White);
                }
                else
                {
                    renderer.Draw(_heartTexture, width / 2 - 58 - i * 8, 25, Color.White);
                }
            }
            else
            {
                break;
            }
        }
        var inv = player.GetInventory();
        for (int m = 0; m < 9; m++)
        {
            renderer.Draw("Textures/Gui/slot", width / 2 - 90 + m * 20, 0, Color.White);
            _client.GetItemRenderer().Render(renderer, inv.Hotbar[m], width / 2 - 90 + m * 20 + 10, 10, Color.White);
        }
        for (int g = 1; g <= 9; g++)
        {
            Inventory inventory = player.GetInventory();

        }
    }
    internal void RenderString(IntegrationRenderer renderer, string s, float x, float y, float scale = 1)
    {

        renderer.DrawString(s, x, y, Color.White, Color.Black, 0.5f);
    }

}
