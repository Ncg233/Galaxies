using Galaxies.Client.Render;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galaxies.Client.Gui.Screen;
public class InGameMenuScreen : AbstractScreen
{
    protected override void OnInit()
    {
        AddWidget(new Widget.Button("Return To Main Menu", 0, 0, 200, 20, () =>
        {
            galaxias.QuitWorld();
        }));
    }
    public override void Render(IntegrationRenderer renderer, double mouseX, double mouseY)
    {
        base.Render(renderer, mouseX, mouseY);
    }
}
