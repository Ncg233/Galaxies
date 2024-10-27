using Galaxies.Client.Gui.Screen;
using Galaxies.Client.Key;
using Galaxies.Client.Render;
using Galaxies.Core.World.Entities;
using Microsoft.Xna.Framework;
using System;

namespace Galaxies.Client.Gui;
public class InGameHud
{
    private readonly string _heartTexture = "Textures/Gui/heart";
    private readonly string _heartHalfTexture = "Textures/Gui/heart_half";
    private bool debug = true;
    private int guiWidth, guiHeight;
    private Main _client;
    private FrameCounter _frameCounter = new();
    private InventoryScreen inventoryScreen = new InventoryScreen();
    public InGameHud(Main galaxias)
    {
        _client = galaxias;

    }
    public void Render(IntegrationRenderer renderer, double mouseX, double mouseY, float dTime)
    {
        AbstractPlayerEntity player = _client.GetPlayer();

        _frameCounter.Update(dTime);
        if (KeyBind.DeBug.IsKeyPressed())
        {
            debug = !debug;
        }
        if (debug)
        {
            RenderString(renderer, "X:" + Math.Round(player.X, 1), 0, 0);
            RenderString(renderer, "Y:" + Math.Round(player.Y, 1), 0, 6);
            RenderString(renderer, "FPS:" + Math.Round(_frameCounter.AverageFramesPerSecond, 1).ToString(), 0f, 12);
            RenderString(renderer, "Speed:" + Math.Round(Math.Sqrt(player.vx * player.vx + player.vy * player.vy) * 30, 0) + "tps", 0, 18);
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
                    renderer.Draw(_heartHalfTexture, guiWidth / 2 + 150 - i * 8, 1, Color.White);
                }
                else
                {
                    renderer.Draw(_heartTexture, guiWidth / 2 + 150 - i * 8, 1, Color.White);
                }
            }
            else
            {
                break;
            }
        }

        if (_client.GetWorld() != null)
        {
            inventoryScreen.Render(renderer, mouseX, mouseY);
        }
    }
    public void OnResize(int guiWidth, int guiHeight)
    {
        this.guiHeight = guiHeight;
        this.guiHeight = guiHeight;
        inventoryScreen.OnResize(guiWidth, guiHeight);
    }
    public void ToggleInventory()
    {
        inventoryScreen.Toggle();
    }
    internal void RenderString(IntegrationRenderer renderer, string s, float x, float y, float scale = 1)
    {

        renderer.DrawString(s, x, y, Color.White, Color.Black, 0.5f);
    }

}
