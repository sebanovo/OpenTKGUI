using System;

namespace OpenTKGUI.Src.Models._3D.Composite;

public class Cara
{
    public List<Vertice> Vertices { get; } = [];
    public uint[] Indices { get; set; } = [];

    public Cara(List<Vertice> vertices, uint[] indices)
    {
        Vertices.AddRange(vertices);
        Indices = indices;
    }

    public void Centrar()
    {
        // Implementación similar a CenterVerticesXYZ pero para una sola cara
        if (Vertices.Count == 0) return;

        float centerX = (Vertices.Min(v => v.X) + Vertices.Max(v => v.X)) / 2f;
        float centerY = (Vertices.Min(v => v.Y) + Vertices.Max(v => v.Y)) / 2f;
        float centerZ = (Vertices.Min(v => v.Z) + Vertices.Max(v => v.Z)) / 2f;

        foreach (var vertice in Vertices)
        {
            vertice.X -= centerX;
            vertice.Y -= centerY;
            vertice.Z -= centerZ;
        }
    }
}