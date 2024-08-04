using Client.Code.Render;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Code.Gui;
public interface IRenderable
{
    public void Render(IntegrationRenderer renderer, double mouseX, double mouseY);
}
