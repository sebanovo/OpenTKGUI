using System;
using OpenTK.Mathematics;
using OpenTKGUI.Src.Models._2D;
using OpenTKGUI.Src.Models._3D;

namespace OpenTKGUI.Src.Models._3D.Composite;

public class Escenario
{
    public List<Objeto3D> Objetos { get; } = [];

    private Axis _ejes;
    public Axis Ejes
    {
        get => _ejes;
        set
        {
            _ejes = value;
            _ejes.Load();
        }
    }

    private CrossHair _crossHair;
    public CrossHair CrossHair
    {
        get => _crossHair;
        set
        {
            _crossHair = value;
            _crossHair.Load();
        }
    }

    public void AddObjeto(Objeto3D objeto)
    {
        Objetos.Add(objeto);
    }

    public void Render()
    {
        foreach (var objeto in Objetos)
        {
            objeto.Draw();
        }
    }

    public Objeto3D GetObjeto(string nombre)
    {
        var obj = Objetos.FirstOrDefault(o => o.Nombre == nombre)
                ?? throw new Exception("No se encontro el nombre del objeto");
        return obj;
    }

    public void Dispose()
    {
        foreach (var objeto in Objetos)
        {
            objeto.Dispose();
        }
        _ejes.Dispose();
    }
}