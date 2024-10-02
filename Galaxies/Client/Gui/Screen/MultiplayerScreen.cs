using Galaxies.Core.Networking;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galaxies.Client.Gui.Screen;
public class MultiplayerScreen : AbstractScreen
{
    private static DirectoryInfo rootPath = new DirectoryInfo("C:\\Users\\MC\\Documents\\.galaxias\\");
    protected override void OnInit()
    {
        AddButton(new Widget.Button("Host", Width / 2 - 100, Height / 4 + 48, 200, 20, () =>
        {
            galaxias.SetCurrentScreen(new WorldSelectionScreen(false));

        }));
        AddButton(new Widget.Button("Add", Width / 2 - 100, Height / 4 + 78, 200, 20, () =>
        {
            NetPlayManager.InitClient("127.0.0.1", 9050);
            galaxias.SetCurrentScreen(null);
        }));
    }
}
