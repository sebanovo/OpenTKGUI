using System;
using StbImageSharp;
using U.Properties;
using U.Src.Models._3D.Composite;
using U.Src.Utils;

namespace U.Src.Models._3D.Factories;

public static class EntityFactory
{
    public static Objeto3D CreateFromShapeData
    (
        string nombre,
        Resources.Config.ShapeData shapeData,
        ImageResult textureImage,
        FlyCamera camera
    )
    {
        var objeto = new Objeto3D(nombre, camera);
        var shader = new Shader(Resources.Shaders.Objeto3DVert, Resources.Shaders.Objeto3DFrag);
        var texture = new Texture(textureImage);
        var parte = new Parte(shader, texture);

        // Convertir los datos a vértices
        var vertices = new List<Vertice>();
        for (int i = 0; i < shapeData.Vertices.Length; i += 5)
        {
            vertices.Add(new Vertice(
                shapeData.Vertices[i],
                shapeData.Vertices[i + 1],
                shapeData.Vertices[i + 2],
                shapeData.Vertices[i + 3],
                shapeData.Vertices[i + 4]
            ));
        }

        // Crear caras basadas en los índices
        var cara = new Cara(vertices, shapeData.Indices);
        cara.Centrar();

        parte.AddCara(cara);
        objeto.Partes.Add(parte);
        objeto.Load();

        return objeto;
    }
}