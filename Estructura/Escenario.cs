using OpenTK.Mathematics;
using OpenTKGUI.Utils;

namespace OpenTKGUI.Estructura;

public class Escenario
{

    public Dictionary<string, Objeto> Objetos { get; } = [];
    public Transformation Transformation { get; } = new Transformation();
    public Ejes Ejes;
    public Escenario() { }
    public Escenario(ArcRotateCamera camera)
    {
        Ejes = new Ejes(camera);
        Ejes.Load();
    }
    public bool EsNombreRepetido(string name)
    {
        return Objetos.ContainsKey(name);
    }

    public void Add(Objeto objeto)
    {
        if (EsNombreRepetido(objeto.Name))
        {
            throw new Exception($"Ya existe un objeto con el nombre {objeto.Name}");
        }
        Objetos.Add(objeto.Name, objeto);
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
        foreach (var objeto in Objetos.Values)
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
        if(Objetos.TryGetValue(name, out var obj))
        {
            return obj;
        }
        throw new Exception($"No se encontro el objeto {name}");
    }

    public void Dispose()
    {
        foreach (var objeto in Objetos.Values)
        {
            objeto.Dispose();
        }
    }
}