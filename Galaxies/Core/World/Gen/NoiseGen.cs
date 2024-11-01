using Galaxies.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace Galaxies.Core.World.Gen;
public class NoiseGen
{

    private static readonly Grad[] GRAD_3 = {
            new Grad(1, 1, 0), new Grad(-1, 1, 0), new Grad(1, -1, 0), new Grad(-1, -1, 0),
            new Grad(1, 0, 1), new Grad(-1, 0, 1), new Grad(1, 0, -1), new Grad(-1, 0, -1),
            new Grad(0, 1, 1), new Grad(0, -1, 1), new Grad(0, 1, -1), new Grad(0, -1, -1)};

    private static readonly double F2 = 0.5 * (Math.Sqrt(3) - 1);
    private static readonly double G2 = (3 - Math.Sqrt(3)) / 6;

    private static readonly int[] perm = new int[512];
    private static readonly int[] perm12 = new int[512];

    public NoiseGen(int seed)
    {
        List<int> permutations = new();
        for (int i = 0; i < 256; i++)
        {
            permutations.Add(i);
        }
        permutations.OrderBy(c => new Random(seed));

        for (int i = 0; i < 512; i++)
        {
            perm[i] = permutations.ElementAt(i & 255);
            perm12[i] = perm[i] % 12;
        }
    }

    private static double dot(Grad g, double x, double y)
    {
        return g.x * x + g.y * y;
    }
    public static double Make2dNoise(double x, double y)
    {
        double n0, n1, n2;

        double s = (x + y) * F2;
        int i = Utils.Floor(x + s);
        int j = Utils.Floor(y + s);
        double t = (i + j) * G2;

        double x0 = x - (i - t);
        double y0 = y - (j - t);

        int i1, j1;
        if (x0 > y0)
        {
            i1 = 1;
            j1 = 0;
        }
        else
        {
            i1 = 0;
            j1 = 1;
        }

        double x1 = x0 - i1 + G2;
        double y1 = y0 - j1 + G2;
        double x2 = x0 - 1 + 2 * G2;
        double y2 = y0 - 1 + 2 * G2;

        int ii = i & 255;
        int jj = j & 255;
        int gi0 = perm12[ii + perm[jj]];
        int gi1 = perm12[ii + i1 + perm[jj + j1]];
        int gi2 = perm12[ii + 1 + perm[jj + 1]];

        double t0 = 0.5 - x0 * x0 - y0 * y0;
        if (t0 < 0)
        {
            n0 = 0;
        }
        else
        {
            t0 *= t0;
            n0 = t0 * t0 * dot(GRAD_3[gi0], x0, y0);
        }
        double t1 = 0.5 - x1 * x1 - y1 * y1;
        if (t1 < 0)
        {
            n1 = 0;
        }
        else
        {
            t1 *= t1;
            n1 = t1 * t1 * dot(GRAD_3[gi1], x1, y1);
        }
        double t2 = 0.5 - x2 * x2 - y2 * y2;
        if (t2 < 0)
        {
            n2 = 0;
        }
        else
        {
            t2 *= t2;
            n2 = t2 * t2 * dot(GRAD_3[gi2], x2, y2);
        }

        return (70 * (n0 + n1 + n2) + 1) / 2;
    }

    internal static void SetSeed(int seed)
    {
        List<int> permutations = new();
        for (int i = 0; i < 256; i++)
        {
            permutations.Add(i);
        }
        permutations.OrderBy(c => new Random(seed));

        for (int i = 0; i < 512; i++)
        {
            perm[i] = permutations.ElementAt(i & 255);
            perm12[i] = perm[i] % 12;
        }
    }
}
class Grad
{

    public readonly double x;
    public readonly double y;
    public readonly double z;
    public readonly double w;

    public Grad(double x, double y, double z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
        w = 0;
    }

    public Grad(double x, double y, double z, double w)
    {
        this.x = x;
        this.y = y;
        this.z = z;
        this.w = w;
    }
}
