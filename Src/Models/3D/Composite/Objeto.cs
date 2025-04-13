using System;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTKGUI.Src.Utils;

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

    public void Draw()
    {
        foreach (var parte in Partes)
        {
            parte.Draw();
        }
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

//public class Objeto : IDisposable
//{
//    private int _vao;
//    private readonly List<int> _vbos = [];
//    private readonly List<int> _ebos = [];
//    public string Nombre { get; set; }
//    public List<Parte> Partes { get; } = [];
//    public ArcRotateCamera Camera { get; }
//    public Vector3 Position { get; set; } = Vector3.Zero;
//    public Vector3 Rotation { get; set; } = Vector3.Zero;
//    public Vector3 Scale { get; set; } = Vector3.One;
//    public Objeto(string nombre, ArcRotateCamera camera)
//    {
//        Nombre = nombre;
//        Camera = camera;
//    }

//    public void Load()
//    {
//        _vao = GL.GenVertexArray();
//        GL.BindVertexArray(_vao);

//        foreach (var parte in Partes)
//        {
//            var vertices = parte.Caras.SelectMany(c => c.Vertices.SelectMany(v => v.ToArray())).ToArray();
//            var indices = parte.Caras.SelectMany(c => c.Indices).ToArray();

//            // Generar y almacenar VBO
//            int vbo = GL.GenBuffer();
//            _vbos.Add(vbo);
//            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);
//            GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);

//            // Generar y almacenar EBO
//            int ebo = GL.GenBuffer();
//            _ebos.Add(ebo);
//            GL.BindBuffer(BufferTarget.ElementArrayBuffer, ebo);
//            GL.BufferData(BufferTarget.ElementArrayBuffer, indices.Length * sizeof(uint), indices, BufferUsageHint.StaticDraw);

//            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 5 * sizeof(float), 0);
//            GL.EnableVertexAttribArray(0);
//            GL.VertexAttribPointer(1, 2, VertexAttribPointerType.Float, false, 5 * sizeof(float), 3 * sizeof(float));
//            GL.EnableVertexAttribArray(1);
//        }
//    }

//    private Matrix4 CalculateModelMatrix()
//    {
//        return Matrix4.CreateScale(Scale) *
//               Matrix4.CreateRotationX(MathHelper.DegreesToRadians(Rotation.X)) *
//               Matrix4.CreateRotationY(MathHelper.DegreesToRadians(Rotation.Y)) *
//               Matrix4.CreateRotationZ(MathHelper.DegreesToRadians(Rotation.Z)) *
//               Matrix4.CreateTranslation(Position);
//    }

//    public void Draw()
//    {
//        GL.BindVertexArray(_vao);
//        GL.Enable(EnableCap.DepthTest);

//        for (int i = 0; i < Partes.Count; i++)
//        {
//            var parte = Partes[i];
//            parte.Shader.Use();
//            parte.Texture.Use();

//            parte.Shader
//                .SetInt("u_Texture", 0)
//                .SetMat4("model", CalculateModelMatrix())
//                .SetMat4("view", Camera.GetViewMatrix())
//                .SetMat4("projection", Camera.GetProjectionMatrix());

//            GL.BindBuffer(BufferTarget.ElementArrayBuffer, _ebos[i]);
//            GL.DrawElements(PrimitiveType.Triangles,
//                           Partes[i].Caras.Sum(c => c.Indices.Length),
//                           DrawElementsType.UnsignedInt, 0);
//        }
//    }

//    public void Dispose()
//    {
//        GL.DeleteVertexArray(_vao);

//        foreach (int vbo in _vbos)
//        {
//            GL.DeleteBuffer(vbo);
//        }

//        foreach (int ebo in _ebos)
//        {
//            GL.DeleteBuffer(ebo);
//        }

//        foreach (var parte in Partes)
//        {
//            parte.Texture.Dispose();
//        }

//        _vbos.Clear();
//        _ebos.Clear();
//        Partes.Clear();

//        GC.SuppressFinalize(this);
//    }
//}
