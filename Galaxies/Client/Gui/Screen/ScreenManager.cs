using Galaxies.Client.Render;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galaxies.Client.Gui.Screen;
public class ScreenManager
{
    private float fadeTotal;
    private float fadeTime;
    private bool isFadeIn;
    private bool isFading;
    private Action afterAction;
    private float counter;
    private bool pressed;
    private MouseType mouseType;

    private Main galaxias;
    //private InventoryScreen inventoryScreen = new();
    public AbstractScreen CurrentScreen { get; set; }
    public AbstractScreen PreviousScreen { get; set; }
    public ScreenManager(Main client)
    {
        galaxias = client;
        //inventoryScreen.Init(galaxias, 0, 0);
    }

    public void SetCurrentScreen(AbstractScreen newScreen, int guiWidth, int guiHeight)
    {
        CurrentScreen?.Hid();
        PreviousScreen = CurrentScreen;
        CurrentScreen = newScreen;
        CurrentScreen?.OnResize(guiWidth, guiHeight);
    }
    public void Update(float deltaTime)
    {
        var ms = Mouse.GetState();
        if (ms.LeftButton == ButtonState.Pressed)
        {
            pressed = true;
            mouseType = MouseType.Left;
        } else if (ms.RightButton == ButtonState.Pressed)
        {
            pressed = true;
            mouseType = MouseType.Right;
        }
        else if(ms.MiddleButton == ButtonState.Pressed)
        {
            pressed = true;
            mouseType = MouseType.Middle;
        }
        if (CurrentScreen != null && pressed && GetMouseState(ms) == ButtonState.Released && !isFading)
        {
            Main.GetScreenPos(out var mouseX, out var mouseY);
            CurrentScreen.MouseClicked(mouseX, mouseY, mouseType);
            pressed = false;
        }
        CurrentScreen?.Update();
        if (fadeTime < fadeTotal)
        {
            fadeTime += deltaTime;
            if (fadeTime >= fadeTotal)
            {
                afterAction?.Invoke();
                isFading = false;
            }
        }
    }

    public ButtonState GetMouseState(MouseState s)
    {
        switch (mouseType)
        {
            case MouseType.Left: return s.LeftButton;
            case MouseType.Right: return s.RightButton;
            case MouseType.Middle: return s.MiddleButton;
        }
        return default;
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
        if (isFading)
        {
            float mod = isFadeIn ? 1 - fadeTime / fadeTotal : fadeTime / fadeTotal;
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

    internal void SetPreviousScreen()
    {
        if (PreviousScreen != null)
        {
            SetCurrentScreen(PreviousScreen, PreviousScreen.Width, PreviousScreen.Height);
        }
        else
        {
            SetCurrentScreen(null, 0, 0);
        }

    }
}
