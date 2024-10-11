using Galaxies.Core.Networking;
using Galaxies.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galaxies.Client.Gui.Screen;
public class WorldSelectionScreen : AbstractScreen
{
    private static string rootPath = "C:\\Users\\MC\\Documents\\.galaxias\\";
    private bool isSingle;
    public WorldSelectionScreen(bool isSingle)
    {
        this.isSingle = isSingle;
    }

    protected override void OnInit()
    {
        AddButton(new Widget.Button("1", Width / 2 - 100, Height / 4, 200, 20, () =>
        {
            galaxias.ScreenManager.FadeOut(1f, () =>
            {
                //galaxias.SetupServer(false, out GalaxiasServer server);
                //NetPlayManager.InitLocalServer(server);
                galaxias.StartWorld(new DirectoryInfo(rootPath + "1\\"));
            });
            if (isSingle)
            {
                Log.Info("Single player");
            }
            else
            {
                Log.Info("Multiplayer");
                NetPlayManager.InitServer("127.0.0.1", 9050);
            }
        }));

    }
}
