using OpenTK.Mathematics;

namespace OpenTKGUI.Src.Models._3D.Composite;
public class Objeto
{
    public string Name = "Default";
    public List<Parte> Partes { get; } = [];
    public Transformation Transformation { get; } = new Transformation();

    public Objeto () { }
    public Objeto (string name, Parte parte)
    {
        Name = name;
        Add(parte);
    }

    public void Add(Parte parte)
    {
        Partes.Add(parte);
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

    public void Draw(Matrix4? modelPadre = null)
    {
        modelPadre ??= Matrix4.Identity;
        foreach (var parte in Partes)
        {
            parte.Draw(CalculateModelMatrix() * (Matrix4)modelPadre);
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

    public Parte GetParte(string name = "Default")
    {
        var obj = Partes.Find(o => o.Name == name);
        return obj ?? throw new Exception($"No se encontro el objeto {name}");
    }

    public void Dispose()
    {
        foreach (var parte in Partes)
        {
            parte.Dispose();
        }
    }
}
