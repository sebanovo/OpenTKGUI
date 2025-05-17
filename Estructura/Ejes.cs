using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTKGUI.Utils;

namespace OpenTKGUI.Estructura;

public class Ejes 
{
    public readonly Shader ShaderProgram;
    public int VBO, VAO;
    public readonly float[] Vertices = [
        // ejes x
        10.0f, 0.0f, 0.0f,
        -10.0f, 0.0f, 0.0f,

        // ejes y
        0.0f, 10.0f, 0.0f,
        0.0f, -10.0f, 0.0f,

        // ejes z
        0.0f, 0.0f, 10.0f,
        0.0f, 0.0f, -10.0f
    ];
    ArcRotateCamera _camera;

    public Ejes() { }
    public Ejes(ArcRotateCamera camera)
    {
        _camera = camera;
        ShaderProgram = new Shader(Resources.Shaders.AxisVert, Resources.Shaders.AxisFrag);
    }

    public void Load()
    {
        VBO = GL.GenBuffer();
        GL.BindBuffer(BufferTarget.ArrayBuffer, VBO);
        GL.BufferData(BufferTarget.ArrayBuffer, Vertices.Length * sizeof(float), Vertices, BufferUsageHint.StaticDraw);

        VAO = GL.GenVertexArray();
        GL.BindVertexArray(VAO);

        GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
        GL.EnableVertexAttribArray(0);
    }

    public void Bind()
    {
        ShaderProgram.Use();
        GL.BindVertexArray(VAO);
        ShaderProgram
                .SetMat4("model", Matrix4.Identity)
                .SetMat4("view", _camera.GetViewMatrix())
                .SetMat4("projection", _camera.GetProjectionMatrix());
    }

    public void Draw()
    {
        Bind();
        GL.LineWidth(3.0f);
        ShaderProgram.SetVec3("u_Color", new Vector3(1.0f, 0.0f, 0.0f));
        GL.DrawArrays(PrimitiveType.Lines, 0, 2);
        ShaderProgram.SetVec3("u_Color", new Vector3(0.0f, 1.0f, 0.0f));
        GL.DrawArrays(PrimitiveType.Lines, 2, 2);
        ShaderProgram.SetVec3("u_Color", new Vector3(0.0f, 0.0f, 1.0f));
        GL.DrawArrays(PrimitiveType.Lines, 4, 2);
    }

    public void Dispose()
    {
        GL.DeleteBuffer(VBO);
        GL.DeleteVertexArray(VAO);
    }

}
