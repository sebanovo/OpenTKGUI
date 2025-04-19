using OpenTK.Mathematics;
using OpenTKGUI.Src.Utils;

namespace OpenTKGUI.Src.Models._3D.Composite;

public class Escenario
{

    public List<Objeto> Objetos { get; } = [];
    public Transformation Transformation { get; } = new Transformation();
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


    public void Escalar(Vector3 scalation)
    {
        Transformation.Scale = new Vector3
        (
            Transformation.Scale.X + scalation.X,
            Transformation.Scale.Y + scalation.Y,
            Transformation.Scale.Z + scalation.Z
        );
    }

    public void Trasladar(Vector3 translation)
    {
        Transformation.Position = new Vector3
        (
            Transformation.Position.X + translation.X,
            Transformation.Position.Y + translation.Y,
            Transformation.Position.Z + translation.Z
        );
    }

    public void Rotar(Vector3 rotation)
    {
        Transformation.Rotation = new Vector3
        (
            Transformation.Rotation.X + rotation.X,
            Transformation.Rotation.Y + rotation.Y,
            Transformation.Rotation.Z + rotation.Z
        );
    }

    public void Draw()
    {
        foreach (var objeto in Objetos)
        {
            objeto.Draw(CalculateModelMatrix());
        }
    }

    private Matrix4 CalculateModelMatrix()
    {
        return
       Matrix4.CreateScale(Transformation.Scale) *
       Matrix4.CreateRotationX(MathHelper.DegreesToRadians(Transformation.Rotation.X)) *
       Matrix4.CreateRotationY(MathHelper.DegreesToRadians(Transformation.Rotation.Y)) *
       Matrix4.CreateRotationZ(MathHelper.DegreesToRadians(Transformation.Rotation.Z)) *
       Matrix4.CreateTranslation(Transformation.Position);
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