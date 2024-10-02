using System;
using System.Collections.Generic;

namespace Galaxies.Core.World.Particles;
public class ParticleManager
{
    public readonly List<Particle> _particles = [];
    public void update(float dTime)
    {
        for (int i = _particles.Count - 1; i >= 0; i--)
        {
            Particle particle = _particles[i];
            particle.Update(dTime);

            if (particle.IsDead)
            {
                _particles.RemoveAt(i);
            }
        }
    }
    public void Clear()
    {
        _particles.Clear();
    }
    public void AddParticle(Particle particle)
    {
        _particles.Add(particle);
    }

}

