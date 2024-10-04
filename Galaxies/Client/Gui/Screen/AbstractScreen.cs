using Galaxies.Client.Gui.Widget;
using Galaxies.Client.Render;
using System;
using System.Collections.Generic;

namespace Galaxies.Client.Gui.Screen;

public abstract class AbstractScreen
{
    public int Width;
    public int Height;
    protected Main galaxias;
    private List<Button> buttons = [];
    public bool CanCloseWithEsc = true;
    public void OnResize(int guiWidth, int guiHeight)
    {
        galaxias = Main.GetInstance();
        Width = guiWidth;
        Height = guiHeight;
        buttons.Clear();
        OnInit();
    }

    protected abstract void OnInit();
    public virtual void Render(IntegrationRenderer renderer, double mouseX, double mouseY)
    {
        buttons.ForEach(button =>
        {
            button.Render(renderer, mouseX, mouseY);
        });
    }

    public virtual void Update()
    {

    }

    public virtual void Hid()
    {

    }
    protected Button AddButton(Button button)
    {
        buttons.Add(button);
        return button;
    }
    public void MouseClicked(double mouseX, double mouseY)
    {
        buttons.ForEach(button =>
        {
            button.MouseClicked(mouseX, mouseY);
        });
    }
}
