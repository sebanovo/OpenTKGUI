using System;
using StbImageSharp;
using OpenTKGUI.Properties;
using OpenTKGUI.Src.Models._3D.Composite;
using OpenTKGUI.Src.Utils;

namespace OpenTKGUI.Src.Models._3D.Factories;

public static class EntityFactory
{
    //public static Objeto CreateFromShapeData
    //(
    //    string nombre,
    //    Resources.Config.ShapeData shapeData,
    //    string textureImage,
    //    ArcRotateCamera camera
    //)
    //{
    //    var objeto = new Objeto(nombre, camera);
    //    var shader = new Shader(Resources.Shaders.Objeto3DVert, Resources.Shaders.Objeto3DFrag);
    //    var texture = new Texture(textureImage);
    //    var parte = new Parte(shader, texture);

    //    // Convertir los datos a vértices
    //    var vertices = new List<Vertice>();
    //    for (int i = 0; i < shapeData.Vertices.Length; i += 5)
    //    {
    //        vertices.Add(new Vertice(
    //            shapeData.Vertices[i],
    //            shapeData.Vertices[i + 1],
    //            shapeData.Vertices[i + 2],
    //            shapeData.Vertices[i + 3],
    //            shapeData.Vertices[i + 4]
    //        ));
    //    }

    //    // Crear caras basadas en los índices
    //    var cara = new Cara(vertices, shapeData.Indices);
    //    cara.Centrar();

    //    parte.AddCara(cara);
    //    objeto.Partes.Add(parte);
    //    objeto.Load();

    //    return objeto;
    //}
}