using OpenTK.Mathematics;
using OpenTKGUI.Estructura;
using System.Text.Json;

namespace OpenTKGUI.Utils;
public class ModeloObjeto
{
    public string? Name { get; set; }
    public Transformacion? Transformation { get; set; }
    public List<ModeloParte>? Partes { get; set; }
}

public class ModeloParte
{
    public string? Name { get; set; }
    public string? Texture { get; set; }
    public Transformacion? Transformation { get; set; }
    public List<Vertice>? Vertices { get; set; }
    public List<uint>? Indices { get; set; }
}

public class Transformacion
{
    public Punto? Position { get; set; }
    public Punto? Rotation { get; set; }
    public Punto? Scale { get; set; }
}

public class Punto
{
    public float X { get; set; }
    public float Y { get; set; }
    public float Z { get; set; }
}

class Modelo
{
    public static Objeto CargarObjeto(string path, ArcRotateCamera camera)
    {
        string jsonContent = File.ReadAllText(path);
        ModeloObjeto modeloObjeto = JsonSerializer.Deserialize<ModeloObjeto>(jsonContent) ??
             throw new Exception("Error al deserializar el objeto JSON.");

        Objeto newObjeto = new()
        {
            Name = modeloObjeto.Name
        };

        newObjeto.Transformation.Scale = new Vector3(
            modeloObjeto.Transformation.Scale.X,
            modeloObjeto.Transformation.Scale.Y,
            modeloObjeto.Transformation.Scale.Z
        );
        newObjeto.Transformation.Position = new Vector3(
            modeloObjeto.Transformation.Position.X,
            modeloObjeto.Transformation.Position.Y,
            modeloObjeto.Transformation.Position.Z
        );
        newObjeto.Transformation.Rotation = new Vector3(
            modeloObjeto.Transformation.Rotation.X,
            modeloObjeto.Transformation.Rotation.Y,
            modeloObjeto.Transformation.Rotation.Z
        );

        foreach (var parte in modeloObjeto.Partes)
        {
            List<float> listaVertices = [];
            foreach (var vertice in parte.Vertices)
            {
                listaVertices.Add(vertice.X);
                listaVertices.Add(vertice.Y);
                listaVertices.Add(vertice.Z);
                listaVertices.Add(vertice.U);
                listaVertices.Add(vertice.V);
            }
            Parte newParte = new Parte(parte.Name, listaVertices, parte.Indices, parte.Texture, camera);

            newParte.Transformation.Position = new Vector3(
                    parte.Transformation.Position.X,
                    parte.Transformation.Position.Y,
                    parte.Transformation.Position.Z
            );

            newParte.Transformation.Rotation = new Vector3(
                    parte.Transformation.Rotation.X,
                    parte.Transformation.Rotation.Y,
                    parte.Transformation.Rotation.Z
            );

            newParte.Transformation.Scale = new Vector3(
                    parte.Transformation.Scale.X,
                    parte.Transformation.Scale.Y,
                    parte.Transformation.Scale.Z
            );
            newObjeto.Add(newParte);
        }
        return newObjeto;
    }

    public static ModeloObjeto GenerarModelo(Objeto objeto, string filePath)
    {
        var FileName = Path.GetFileNameWithoutExtension(filePath);

        List<ModeloParte> partes = [];
        foreach (var parte in objeto.Partes.Values)
        {
            List<Vertice> listaVertices = [];
            for (int i = 0; i < parte.Vertices.Count; i += 5)
            {
                if (i + 4 >= parte.Vertices.Count) break;
                listaVertices.Add(new Vertice()
                {
                    X = parte.Vertices[i],
                    Y = parte.Vertices[i + 1],
                    Z = parte.Vertices[i + 2],
                    U = parte.Vertices[i + 3],
                    V = parte.Vertices[i + 4]
                });
            }

            partes.Add(
                new ModeloParte
                {
                    Name = parte.Name,
                    Texture = parte.Texture,
                    Transformation = new Transformacion()
                    {
                        Position = new Punto()
                        {
                            X = parte.Transformation.Position.X,
                            Y = parte.Transformation.Position.Y,
                            Z = parte.Transformation.Position.Z
                        },
                        Rotation = new Punto()
                        {
                            X = parte.Transformation.Rotation.X,
                            Y = parte.Transformation.Rotation.Y,
                            Z = parte.Transformation.Rotation.Z
                        },
                        Scale = new Punto()
                        {
                            X = parte.Transformation.Scale.X,
                            Y = parte.Transformation.Scale.Y,
                            Z = parte.Transformation.Scale.Z
                        }
                    },
                    Vertices = listaVertices,
                    Indices = parte.Indices
                }
             );
        }

        return new ModeloObjeto
        {
            Name = FileName,
            Transformation = new Transformacion
            {
                Position = new Punto()
                {
                    X = objeto.Transformation.Position.X,
                    Y = objeto.Transformation.Position.Y,
                    Z = objeto.Transformation.Position.Z
                },
                Rotation = new Punto()
                {
                    X = objeto.Transformation.Rotation.X,
                    Y = objeto.Transformation.Rotation.Y,
                    Z = objeto.Transformation.Rotation.Z
                },
                Scale = new Punto()
                {
                    X = objeto.Transformation.Scale.X,
                    Y = objeto.Transformation.Scale.Y,
                    Z = objeto.Transformation.Scale.Z
                }

            },
            Partes = partes
        };
    }
}
