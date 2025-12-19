using StbImageSharp;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using U.src.Utils;
using U.Src.Utils;
using System.Text.Json;

namespace U.Src.Models._3D;

public class Entity
{
    private int _vao;
    private int _vbo;
    private float[] _vertices = [];
    private Shader _shader;
    private Texture _texture;
    private Camera _camera;

    public Entity(float[] vertices, string vertexCode, string fragmentCode, ImageResult textureImage, Camera camera)
    {
        _shader = new(vertexCode, fragmentCode);
        _texture = new(textureImage);
        _camera = camera;
        _vertices = vertices;
    }

    private Vector3 CalculateCentroid()
    {
        float sumX = 0, sumY = 0, sumZ = 0;
        int vertexCount = _vertices.Length / 5;

        for (int i = 0; i < _vertices.Length; i += 5)
        {
            sumX += _vertices[i];
            sumY += _vertices[i + 1];
            sumZ += _vertices[i + 2];
        }

        return new Vector3(sumX / vertexCount, sumY / vertexCount, sumZ / vertexCount);
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
    }

    private void CenterVertices()
    {
        Vector3 centroid = CalculateCentroid();

        for (int i = 0; i < _vertices.Length; i += 5)
        {
            _vertices[i] -= centroid.X;
            _vertices[i + 1] -= centroid.Y + 0.15f;
            _vertices[i + 2] -= centroid.Z;
        }
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
        GL.DrawArrays(PrimitiveType.Triangles, 0, _vertices.Length / 5);
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
        // GL.DeleteBuffer(EBO);
        _texture.Dispose();
    }
}
