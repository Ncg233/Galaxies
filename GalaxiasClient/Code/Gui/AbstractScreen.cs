using Client.Code.Gui.Widget;
using Client.Code.Main;
using Client.Code.Render;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace Client.Code.Gui;

public abstract class AbstractScreen
{
    protected int width;
    protected int height;
    protected GalaxiasClient galaxias;
    private List<Button> buttons = [];
    public void Init(GalaxiasClient galaxias, int width, int height)
    {
        this.galaxias = galaxias;
        this.width = width;
        this.height = height;
        buttons.Clear();
        OnInit();
    }

    protected virtual void OnInit()
    {

    }

    public virtual void Render(IntegrationRenderer renderer)
    {
        buttons.ForEach(button =>
        {
            button.Render(renderer, 0, 0);
        });
    }
    protected Button AddButton(Button button)
    {
        buttons.Add(button);
        return button;
    }
    public virtual void Update()
    {

    }

    public virtual void Hid()
    {

    }
    public void MouseClicked(double mouseX, double mouseY)
    {
        buttons.ForEach(button =>
        {
            button.MouseClicked(mouseX, mouseY);
        });
    }
    public void OnResize(int guiWidth, int guiHeight)
    {
        Init(galaxias, guiWidth, guiHeight);
    }
}
