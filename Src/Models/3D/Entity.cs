using StbImageSharp;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTKGUI.Src.Utils;
using OpenTKGUI.Properties;

namespace OpenTKGUI.Src.Models._3D;

public class Entity
{
    private int _vao, _vbo, _ebo;
    private float[] _vertices = [];
    private uint[] _indices = [];
    private Shader _shader;
    private Texture _texture;
    private ArcRotateCamera _camera;

    public Entity(Resources.Config.ShapeData shapeData, string vertexCode, string fragmentCode, string textureImage, ArcRotateCamera camera)
    {
        _shader = new(vertexCode, fragmentCode);
        _texture = new(textureImage);
        _camera = camera;
        _vertices = CenterVerticesXYZ(shapeData.Vertices);
        _indices = shapeData.Indices;
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
        _vbo = GL.GenBuffer();
        GL.BindBuffer(BufferTarget.ArrayBuffer, _vbo);
        GL.BufferData(BufferTarget.ArrayBuffer, _vertices.Length * sizeof(float), _vertices, BufferUsageHint.StaticDraw);

        _vao = GL.GenVertexArray();
        GL.BindVertexArray(_vao);

        GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 5 * sizeof(float), 0);
        GL.EnableVertexAttribArray(0);

        GL.VertexAttribPointer(1, 3, VertexAttribPointerType.Float, false, 5 * sizeof(float), 3 * sizeof(float));
        GL.EnableVertexAttribArray(1);

        _ebo = GL.GenBuffer();
        GL.BindBuffer(BufferTarget.ElementArrayBuffer, _ebo);
        GL.BufferData(BufferTarget.ElementArrayBuffer, _indices.Length * sizeof(uint), _indices, BufferUsageHint.StaticDraw);
    }


    private void Bind()
    {
        _shader.Use();
        _texture.Use();
        GL.Enable(EnableCap.DepthTest);
        GL.BindVertexArray(_vao);

        _shader.SetInt("u_Texture", 0).
               SetMat4("model", Matrix4.Identity)
              .SetMat4("view", _camera.GetViewMatrix())
              .SetMat4("projection", _camera.GetProjectionMatrix());
    }

    public void Draw(float x, float y, float z)
    {
        Bind();

        float[] copiedVertices = [.. _vertices];
        for (int i = 0; i < copiedVertices.Length; i += 5)
        {
            copiedVertices[i] += x;
            copiedVertices[i + 1] += y;
            copiedVertices[i + 2] += z;
        }

        GL.BindBuffer(BufferTarget.ArrayBuffer, _vbo);
        GL.BufferData(BufferTarget.ArrayBuffer, copiedVertices.Length * sizeof(float), copiedVertices, BufferUsageHint.StaticDraw);
        GL.BindBuffer(BufferTarget.ElementArrayBuffer, _ebo);
        // GL.DrawArrays(PrimitiveType.Triangles, 0, _vertices.Length / 5);
        GL.DrawElements(PrimitiveType.Triangles, _indices.Length, DrawElementsType.UnsignedInt, 0);
    }

    public void Draw()
    {
        Bind();
        GL.DrawArrays(PrimitiveType.Triangles, 0, _vertices.Length / 5);
    }

    public void Dispose()
    {
        GL.DeleteBuffer(_vbo);
        GL.DeleteVertexArray(_vao);
        GL.DeleteBuffer(_ebo);
        _texture.Dispose();
    }
}
