using System;
using OpenTK.Mathematics;
using OpenTKGUI.Src.Models._2D;
using OpenTKGUI.Src.Models._3D;

namespace OpenTKGUI.Src.Models._3D.Composite;

public class Escenario
{
    public List<Objeto> Objetos { get; } = [];

    public void Add(Objeto objeto)
    {
        Objetos.Add(objeto);
    }

    public void Draw()
    {
        foreach (var objeto in Objetos)
        {
            objeto.Draw();
        }
    }

    public Objeto GetObjeto(string name)
    {
        var obj =  Objetos.Find(o => o.Name == name);
        return obj ?? throw new Exception($"No se encontro el objeto {name}");
    }

    public void Dispose()
    {
        foreach (var objeto in Objetos)
        {
            objeto.Dispose();
        }
    }
}