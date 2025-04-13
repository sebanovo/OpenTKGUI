using System;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using OpenTKGUI.Properties;
using OpenTKGUI.Src.Utils;
using static OpenTKGUI.Properties.Resources;

namespace OpenTKGUI.Src.Models._3D.Composite;
public class Transformation
{
    public Vector3 Position { get; set; } = Vector3.Zero;
    public Vector3 Rotation { get; set; } = Vector3.Zero;
    public Vector3 Scale { get; set; } = Vector3.One;
}

public class Parte
{

    private int _vao, _vbo, _ebo;
    public string Name { get; set; } = "Default";
    public Transformation Transformation { get; set; } = new Transformation();
    public List<float> Vertices { get; } = [];
    public List<uint> Indices { get; set;  } = [];
    public Shader Shader { get; } = new Shader(Shaders.Objeto3DVert, Shaders.Objeto3DFrag);
    public string Texture { get; }
    private Texture TextureObj { get; }
    private ArcRotateCamera Camera { get; }

    public Parte() { }

    public Parte(string name, List<float> vertices, List<uint> indices, string texture, ArcRotateCamera camera)
    {
        Name = name;
        Vertices.AddRange(CenterVerticesXYZ([..vertices]));
        Indices.AddRange(indices);

        Texture = texture;
        TextureObj = new Texture(Texture);
        Camera = camera;
        Load();
    }

    private static float[] CenterVerticesXYZ(float[] vertices)
    {
        // 1. Extraer todas las coordenadas X, Y, Z (cada vértice tiene 5 valores: x, y, z, u, v)
        int vertexCount = vertices.Length / 5;
        float[] xCoords = new float[vertexCount];
        float[] yCoords = new float[vertexCount];
        float[] zCoords = new float[vertexCount];

        for (int i = 0; i < vertexCount; i++)
        {
            int baseIndex = i * 5;
            xCoords[i] = vertices[baseIndex];
            yCoords[i] = vertices[baseIndex + 1];
            zCoords[i] = vertices[baseIndex + 2];
        }

        // 2. Calcular centros geométricos
        float centerX = (xCoords.Min() + xCoords.Max()) / 2f;
        float centerY = (yCoords.Min() + yCoords.Max()) / 2f;
        float centerZ = (zCoords.Min() + zCoords.Max()) / 2f;

        // 3. Crear nuevo array de vértices centrados
        float[] centeredVertices = new float[vertices.Length];
        for (int i = 0; i < vertices.Length; i++)
        {
            centeredVertices[i] = (i % 5) switch
            {
                0 => vertices[i] - centerX,
                1 => vertices[i] - centerY,
                2 => vertices[i] - centerZ,
                _ => vertices[i],
            };
        }

        return centeredVertices;
    }

    public void Load()
    {

        _vao = GL.GenVertexArray();
        GL.BindVertexArray(_vao);

        _vbo = GL.GenBuffer();
        GL.BindBuffer(BufferTarget.ArrayBuffer, _vbo);
        GL.BufferData(BufferTarget.ArrayBuffer, Vertices.Count * sizeof(float), Vertices.ToArray(), BufferUsageHint.StaticDraw);

        _ebo = GL.GenBuffer();
        GL.BindBuffer(BufferTarget.ElementArrayBuffer, _ebo);
        GL.BufferData(BufferTarget.ElementArrayBuffer, Indices.Count * sizeof(uint), Indices.ToArray(), BufferUsageHint.StaticDraw);

        GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 5 * sizeof(float), 0);
        GL.EnableVertexAttribArray(0);
        GL.VertexAttribPointer(1, 3, VertexAttribPointerType.Float, false, 5 * sizeof(float), 3 * sizeof(float));
        GL.EnableVertexAttribArray(1);
    }

    private Matrix4 CalculateModelMatrix()
    {
        return Matrix4.CreateScale(Transformation.Scale) *
               Matrix4.CreateRotationX(MathHelper.DegreesToRadians(Transformation.Rotation.X)) *
               Matrix4.CreateRotationY(MathHelper.DegreesToRadians(Transformation.Rotation.Y)) *
               Matrix4.CreateRotationZ(MathHelper.DegreesToRadians(Transformation.Rotation.Z)) *
               Matrix4.CreateTranslation(Transformation.Position);
    }

    public void Draw()
    {
        GL.BindVertexArray(_vao);
        GL.Enable(EnableCap.DepthTest);

        Shader.Use();
        TextureObj.Use();

        Shader
            .SetInt("u_Texture", 0)
            .SetMat4("model", CalculateModelMatrix())
            .SetMat4("view", Camera.GetViewMatrix())
            .SetMat4("projection", Camera.GetProjectionMatrix());

        GL.BindBuffer(BufferTarget.ElementArrayBuffer, _ebo);
        GL.DrawElements(PrimitiveType.Triangles, Indices.Count, DrawElementsType.UnsignedInt, 0);
    }
    public void Dispose()
    {
        GL.DeleteBuffer(_vbo);
        GL.DeleteVertexArray(_vao);
        GL.DeleteBuffer(_ebo);
        TextureObj.Dispose();
    }
}

//public class Parte
//{
//    public List<Cara> Caras { get; } = [];
//    public Texture Texture { get; }
//    public Shader Shader { get; }

//    public Parte(Texture texture)
//    {
//        Texture = texture;
//        Shader = new Shader(Resources.Shaders.Objeto3DVert, Resources.Shaders.Objeto3DFrag);
//    }

//    public Parte(Shader shader, Texture texture)
//    {
//        Shader = shader;
//        Texture = texture;
//    }

//    public void AddCara(Cara cara)
//    {
//        Caras.Add(cara);
//    }

//    public void CentrarParte()
//    {
//        foreach (var cara in Caras)
//        {
//            cara.Centrar();
//        }
//    }
//}