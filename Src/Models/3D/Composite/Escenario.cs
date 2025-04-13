using System;
using OpenTK.Mathematics;
using OpenTKGUI.Src.Models._2D;
using OpenTKGUI.Src.Models._3D;
using OpenTKGUI.Src.Utils;

namespace OpenTKGUI.Src.Models._3D.Composite;

public class Escenario
{
    public List<Objeto> Objetos { get; } = [];
    public Axis Ejes;
    public Escenario() { }
    public Escenario(ArcRotateCamera camera)
    {
        Ejes = new Axis(camera);
        Ejes.Load();
    }
    public bool EsNombreRepetido(string name)
    {
        return Objetos.Any(o => o.Name == name);
    }

    public void Add(Objeto objeto)
    {
        if (EsNombreRepetido(objeto.Name))
        {
            throw new Exception($"Ya existe un objeto con el nombre {objeto.Name}");
        }
        Objetos.Add(objeto);
    }

    public void Draw()
    {
        foreach (var objeto in Objetos)
        {
            objeto.Draw();
        }
    }
    public void DrawEjes()
    {
        Ejes.Bind();
        Ejes.Draw();
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