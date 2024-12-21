using Galaxies.Client.Gui.Widget;
using Galaxies.Client.Render;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Galaxies.Client.Gui.Screen;

public abstract class AbstractScreen
{
    public int Width;
    public int Height;
    public int xSize;
    public int ySize;
    public int LeftPos;
    public int TopPos;
    protected Main galaxias;
    private readonly List<IWidget> widgets = [];
    public bool CanCloseWithEsc = true;
    public void OnResize(int guiWidth, int guiHeight)
    {
        galaxias = Main.GetInstance();
        Width = guiWidth;
        Height = guiHeight;
        widgets.Clear();
        OnInit();
    }

    protected abstract void OnInit();
    public virtual void Render(IntegrationRenderer renderer, double mouseX, double mouseY)
    {
        widgets.ForEach(widget =>
        {
            widget.Render(renderer, mouseX, mouseY);
        });
    }

    public virtual void Update()
    {

    }

    public virtual void Hid()
    {

    }
    protected IWidget AddWidget(IWidget widget)
    {
        widgets.Add(widget);
        return widget;
    }
    public virtual bool MouseClicked(double mouseX, double mouseY, MouseType pressedKey)
    {
        foreach (IWidget widget in widgets) {
            if (widget.MouseClicked(mouseX, mouseY, pressedKey))
            {
                return true;
            }
        }
        return false;
        
    }
}
