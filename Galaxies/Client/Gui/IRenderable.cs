using Galaxies.Client.Render;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galaxies.Client.Gui;
public interface IRenderable
{
    public void Render(IntegrationRenderer renderer, double mouseX, double mouseY);
}
