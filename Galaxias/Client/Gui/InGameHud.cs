using Galaxias.Client.Key;
using Galaxias.Client.Main;
using Galaxias.Client.Render;
using Galaxias.Core.World.Entities;
using Microsoft.Xna.Framework;
using System;

namespace Galaxias.Client.Gui;
public class InGameHud
{
    private readonly string _heartTexture = "Textures/Gui/heart";
    private readonly string _heartHalfTexture = "Textures/Gui/heart_half";
    private bool debug = true;
    private Main.GalaxiasClient _client;
    private FrameCounter _frameCounter = new();
    public InGameHud(Main.GalaxiasClient galaxias)
    {
        _client = galaxias;

    }
    public void Render(IntegrationRenderer renderer, int width, int height, float dTime)
    {
        Player player = _client.GetPlayer();

        _frameCounter.Update(dTime);
        if (KeyBind.DeBug.IsKeyPressed())
        {
            debug = !debug;
        }
        if (debug)
        {
            RenderString(renderer, "X:" + Math.Round(_client.GetPlayer().x, 1), 0, 0);
            RenderString(renderer, "Y:" + Math.Round(_client.GetPlayer().y, 1), 0, 6);
            RenderString(renderer, "FPS:" + Math.Round(_frameCounter.AverageFramesPerSecond, 1).ToString(), 0f, 12);
            RenderString(renderer, "Speed:" + Math.Round(Math.Sqrt(_client.GetPlayer().vx * _client.GetPlayer().vx + _client.GetPlayer().vy * _client.GetPlayer().vy) * 60, 0)  + "tps" , 0, 18);
            int currentTime = (int)_client.GetWorld().currnetTime;
            RenderString(renderer, "Time:" + currentTime / 60 + ":" + (currentTime % 60).ToString("D2"), 0, 24);

        }

        int health = (int)Math.Round(_client.GetPlayer().health / 10);
        for (int i = 4; i >= 0; i--)
        {
            if (health > 0)
            {
                health -= 2;
                if (health < 0)
                {
                    renderer.Draw(_heartHalfTexture, width / 2 + 150 - i * 8, 1, Color.White);
                }
                else
                {
                    renderer.Draw(_heartTexture, width / 2 + 150 - i * 8, 1, Color.White);
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
            if (m == _client.GetPlayer().GetInventory().onHand) renderer.Draw("Textures/Gui/slot_onHand", width / 2 - 90 + _client.GetPlayer().GetInventory().onHand * 20, 0, Color.White);
            else renderer.Draw("Textures/Gui/slot", width / 2 - 90 + m * 20, 0, Color.White);
            _client.GetItemRenderer().RenderInGui(renderer, inv.Hotbar[m], width / 2 - 90 + m * 20 + 10, 10, Color.White);
        }

    }
    internal void RenderString(IntegrationRenderer renderer, string s, float x, float y, float scale = 1)
    {

        renderer.DrawString(s, x, y, Color.White, Color.Black, 0.5f);
    }

}
