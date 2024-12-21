using Galaxies.Client.Gui.Screen;
using Galaxies.Client.Render;
using Galaxies.Core.Audio;
using SharpDX.Direct3D9;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galaxies.Client.Gui.Widget;
public interface IWidget
{
    public void Render(IntegrationRenderer renderer, double mouseX, double mouseY);
    public bool MouseClicked(double mouseX, double mouseY, MouseType type);
}
