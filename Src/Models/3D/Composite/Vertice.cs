using System;

namespace OpenTKGUI.Src.Models._3D.Composite;

public class Vertice
{
    public float X { get; set; }
    public float Y { get; set; }
    public float Z { get; set; }
    public float U { get; set; } // Coordenada de textura
    public float V { get; set; } // Coordenada de textura

    public Vertice() { }
    public Vertice(float x, float y, float z, float u, float v)
    {
        X = x;
        Y = y;
        Z = z;
        U = u;
        V = v;
    }

    public float[] ToArray() => [X, Y, Z, U, V];
}