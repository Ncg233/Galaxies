using Galaxias.Client;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galaxias.Core.Audio;
public class Sound
{
    private SoundEffect effect;
    private Song song;
    private bool isSoundEffect;
    private string file;
    public Sound(string name, bool isSoundEffect)
    {
        this.isSoundEffect = isSoundEffect;
        file = name;
        
    }
    public void Load()
    {
        if (isSoundEffect)
        {
            effect = Main.GetInstance().Content.Load<SoundEffect>(file);
        }
        else
        {
            song = Main.GetInstance().Content.Load<Song>(file);
        }
    }
    public void PlayEffect(float volume, float pitch, float pan)
    {
        if (isSoundEffect) { 
            effect.Play(volume, pitch, pan);
        }
    }
    public void PlayEffect()
    {
        if (isSoundEffect)
        {
            effect.Play();
        }
    }
    public void PlayMusic(float volume)
    {
        if (!isSoundEffect)
        {
            // check the current state of the MediaPlayer.
            if (MediaPlayer.State != MediaState.Stopped)
            {
                MediaPlayer.Stop(); // stop current audio playback if playing or paused.
            }

            // Play the selected song reference.
            MediaPlayer.Volume = volume;
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Play(song);
        }
    }

}
