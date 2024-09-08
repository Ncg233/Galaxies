using Galaxias.Client.Main;
using Galaxias.Client.Render;
using Galaxias.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galaxias.Client.Gui.Screen;
public class ScreenManager
{
    private float fadeTotal;
    private float fadeTime;
    private bool isFadeIn;
    private bool isFading;
    private Action afterAction;

    private GalaxiasClient galaxias;
    //private InventoryScreen inventoryScreen = new();
    public AbstractScreen CurrentScreen { get; set; }
    public ScreenManager(GalaxiasClient client)
    {
        galaxias = client;
        //inventoryScreen.Init(galaxias, 0, 0);
    }

    public void SetCurrentScreen(AbstractScreen newScreen, int guiWidth, int guiHeight)
    {
        CurrentScreen?.Hid();
        CurrentScreen = newScreen;
        CurrentScreen?.OnResize(guiWidth, guiHeight);
    }
    public void Update(float deltaTime)
    {
        if (CurrentScreen != null && galaxias.IsActive && Mouse.GetState().LeftButton == ButtonState.Pressed && !isFading)
        {
            Point p = Mouse.GetState().Position;
            double mouseX = p.X * CurrentScreen.Width / galaxias.GetWindowWidth();
            double mouseY = p.Y * CurrentScreen.Height / galaxias.GetWindowHeight();
            CurrentScreen.MouseClicked(mouseX, mouseY);
            
        }
        CurrentScreen?.Update();
        if (fadeTime < fadeTotal)
        {
            fadeTime += deltaTime;
            if (fadeTime >= fadeTotal) { 
                afterAction?.Invoke();
                isFading = false;
            }
        }
    }
    public void FadeIn(float totalSeconds)
    {
        isFading = true;
        isFadeIn = true;
        fadeTotal = totalSeconds;
        fadeTime = 0;
    }
    public void FadeOut(float totalSeconds, Action afterAction)
    {
        isFading = true;
        isFadeIn = false;
        fadeTotal = totalSeconds;
        fadeTime = 0;
        this.afterAction = afterAction;
    }
    public void Render(IntegrationRenderer renderer, double mouseX, double mouseY)
    {
        //if(galaxias.GetWorld() != null)
        //{
        //    inventoryScreen.Render(renderer, mouseX, mouseY);
        //}
        CurrentScreen?.Render(renderer, mouseX, mouseY);
        if (isFading) {
            float mod = isFadeIn ? 1 - (fadeTime / fadeTotal) : fadeTime / fadeTotal;
            renderer.Draw("Textures/Misc/blank", new Rectangle(0, 0, CurrentScreen.Width, CurrentScreen.Height), Color.Black * mod);
        }

        
    }
    public bool ShouldRender()
    {
        return CurrentScreen != null;
    }

    public void OnResize(int guiWidth, int guiHeight)
    {
        //inventoryScreen.OnResize(guiWidth, guiHeight);
        CurrentScreen?.OnResize(guiWidth, guiHeight);
    }
    public void ToggleInventory()
    {
        //inventoryScreen.Toggle();
    }
}
