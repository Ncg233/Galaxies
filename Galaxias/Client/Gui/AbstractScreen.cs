using Galaxias.Client.Gui.Widget;
using Galaxias.Client.Main;
using Galaxias.Client.Render;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace Galaxias.Client.Gui;

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

    public virtual void Render(IntegrationRenderer renderer, double mouseX, double mouseY)
    {
        buttons.ForEach(button =>
        {
            button.Render(renderer, mouseX, mouseY);
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
