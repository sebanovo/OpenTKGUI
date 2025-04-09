using System;
using System.Drawing;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTKGUI.Properties;
using OpenTKGUI.Src.Utils;

namespace OpenTKGUI.Src.Models._2D;

public class CrossHair
{
    public readonly Shader ShaderProgram;
    public FlyCamera _camera;
    public int VBO, VAO;
    private float _scaleX = 1;
    public float ScaleX
    {
        get => _scaleX;
        set
        {
            _scaleX = value;
            float[] temp = [.. Vertices];
            temp[0] *= _scaleX;
            temp[3] *= _scaleX;

            // Actualizar el VBO con las coordenadas ajustadas
            GL.BindBuffer(BufferTarget.ArrayBuffer, VBO);
            GL.BufferData(BufferTarget.ArrayBuffer, temp.Length * sizeof(float), temp, BufferUsageHint.StaticDraw);
        }
    }
    public float[] Vertices { get; set; }
    public float SizeX;
    public float SizeY;
    public CrossHair(FlyCamera camera, float sizeX, float sizeY)
    {
        SizeX = sizeX;
        SizeY = sizeY;
        _camera = camera;

        _scaleX = 1.0f;
        ShaderProgram = new Shader(Resources.Shaders.CrossHairVert, Resources.Shaders.CrossHairFrag);

        Vertices = [
            // x
            -0.02f * _scaleX, 0.0f, 0.0f,
            0.02f * _scaleX, 0.0f, 0.0f,

            // y
            0.0f, -0.02f, 0.0f,
            0.0f, 0.02f, 0.0f,
        ];
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
        GL.Disable(EnableCap.DepthTest);
        ShaderProgram.Use();
        GL.BindVertexArray(VAO);
        ScaleX = 1.0f / (SizeX / SizeY);
        ShaderProgram.SetVec3("u_Color", Vector3.One);
    }

    public void Draw()
    {
        Bind();
        GL.LineWidth(3.0f);
        GL.DrawArrays(PrimitiveType.Lines, 0, 2);
        GL.DrawArrays(PrimitiveType.Lines, 2, 2);
    }

    public void Dispose()
    {
        // crossHair
        GL.DeleteBuffer(VBO);
        GL.DeleteVertexArray(VAO);
    }
}
