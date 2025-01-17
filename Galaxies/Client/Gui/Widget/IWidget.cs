using Galaxies.Client.Gui.Screen;
using Galaxies.Client.Render;

namespace Galaxies.Client.Gui.Widget;
public interface IWidget
{
    public void Render(IntegrationRenderer renderer, double mouseX, double mouseY);
    public bool MouseClicked(double mouseX, double mouseY, MouseType type);
}
