using System;
using OpenTKGUI.Properties;
using OpenTKGUI.Src.Utils;

namespace OpenTKGUI.Src.Models._3D.Composite;

public class Parte
{
    public List<Cara> Caras { get; } = [];
    public Texture Texture { get; }
    public Shader Shader { get; }

    public Parte(Texture texture)
    {
        Texture = texture;
        Shader = new Shader(Resources.Shaders.Objeto3DVert, Resources.Shaders.Objeto3DFrag);
    }

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