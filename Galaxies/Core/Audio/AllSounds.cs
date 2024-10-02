using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galaxies.Core.Audio;
public class AllSounds
{
    private static readonly Dictionary<string, Sound> sounds = [];
    public static readonly Sound Galaxias = Register("Assets/Musics/galaxias", false);
    public static readonly Sound EarthForest = Register("Assets/Musics/earth_forest", false);
    public static readonly Sound ButtonClick = Register("Assets/Sounds/button_click", true);

    private static Sound Register(string name, bool isEffect)
    {
        var s = new Sound(name, isEffect);
        sounds.Add(name, s);
        return s;
    }
    public static void LoadContent()
    {
        foreach (var sound in sounds.Values)
        {
            sound.Load();
        }
    }
}
