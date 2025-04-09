using System;
using OpenTKGUI.Src.Utils;

namespace OpenTKGUI.Src.Models._3D.Composite;

public class Parte
{
    public List<Cara> Caras { get; } = [];
    public Texture Texture { get; }
    public Shader Shader { get; }

    public Parte(Shader shader, Texture texture)
    {
        Shader = shader;
        Texture = texture;
    }

    public void AddCara(Cara cara)
    {
        Caras.Add(cara);
    }

    public void CentrarParte()
    {
        foreach (var cara in Caras)
        {
            cara.Centrar();
        }
    }
}