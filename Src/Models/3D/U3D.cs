using U.src.Utils;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using U.Src.Utils;

namespace U.Src.Models._3D;

public class U3D
{
    public readonly Shader ShaderProgram;
    public readonly Texture TextureImage;
    public int VBO, VAO;
    public readonly float[] Vertices = [
        // Posiciones        // Coordenadas de textura
        -0.2f, -0.2f, -0.2f,  0.0f, 0.0f, // Cara trasera
        0.2f, -0.2f, -0.2f,  1.0f, 0.0f,
        0.2f,  0.2f, -0.2f,  1.0f, 1.0f,
        0.2f,  0.2f, -0.2f,  1.0f, 1.0f,
        -0.2f,  0.2f, -0.2f,  0.0f, 1.0f,
        -0.2f, -0.2f, -0.2f,  0.0f, 0.0f,

        -0.2f, -0.2f,  0.2f,  0.0f, 0.0f, // Cara frontal
        0.2f, -0.2f,  0.2f,  1.0f, 0.0f,
        0.2f,  0.2f,  0.2f,  1.0f, 1.0f,
        0.2f,  0.2f,  0.2f,  1.0f, 1.0f,
        -0.2f,  0.2f,  0.2f,  0.0f, 1.0f,
        -0.2f, -0.2f,  0.2f,  0.0f, 0.0f,

        -0.2f,  0.2f,  0.2f,  1.0f, 0.0f, // Cara izquierda
        -0.2f,  0.2f, -0.2f,  1.0f, 1.0f,
        -0.2f, -0.2f, -0.2f,  0.0f, 1.0f,
        -0.2f, -0.2f, -0.2f,  0.0f, 1.0f,
        -0.2f, -0.2f,  0.2f,  0.0f, 0.0f,
        -0.2f,  0.2f,  0.2f,  1.0f, 0.0f,

        0.2f,  0.2f,  0.2f,  1.0f, 0.0f, // Cara derecha
        0.2f,  0.2f, -0.2f,  1.0f, 1.0f,
        0.2f, -0.2f, -0.2f,  0.0f, 1.0f,
        0.2f, -0.2f, -0.2f,  0.0f, 1.0f,
        0.2f, -0.2f,  0.2f,  0.0f, 0.0f,
        0.2f,  0.2f,  0.2f,  1.0f, 0.0f,

        -0.2f, -0.2f, -0.2f,  0.0f, 1.0f, // Cara inferior
        0.2f, -0.2f, -0.2f,  1.0f, 1.0f,
        0.2f, -0.2f,  0.2f,  1.0f, 0.0f,
        0.2f, -0.2f,  0.2f,  1.0f, 0.0f,
        -0.2f, -0.2f,  0.2f,  0.0f, 0.0f,
        -0.2f, -0.2f, -0.2f,  0.0f, 1.0f,

        -0.2f,  0.2f, -0.2f,  0.0f, 1.0f, // Cara superior
        0.2f,  0.2f, -0.2f,  1.0f, 1.0f,
        0.2f,  0.2f,  0.2f,  1.0f, 0.0f,
        0.2f,  0.2f,  0.2f,  1.0f, 0.0f,
        -0.2f,  0.2f,  0.2f,  0.0f, 0.0f,
        -0.2f,  0.2f, -0.2f,  0.0f, 1.0f,

        // palo1
        -0.6f, -0.2f, -0.2f,  0.0f, 0.0f,  // Cara trasera
        -0.2f, -0.2f, -0.2f,  1.0f, 0.0f,
        -0.2f,  1.0f, -0.2f,  1.0f, 1.0f,
        -0.2f,  1.0f, -0.2f,  1.0f, 1.0f,
        -0.6f,  1.0f, -0.2f,  0.0f, 1.0f,
        -0.6f, -0.2f, -0.2f,  0.0f, 0.0f,

        -0.6f, -0.2f,  0.2f,  0.0f, 0.0f, // Cara frontal
        -0.2f, -0.2f,  0.2f,  1.0f, 0.0f,
        -0.2f,  1.0f,  0.2f,  1.0f, 1.0f,
        -0.2f,  1.0f,  0.2f,  1.0f, 1.0f,
        -0.6f,  1.0f,  0.2f,  0.0f, 1.0f,
        -0.6f, -0.2f,  0.2f,  0.0f, 0.0f,

        -0.6f,  1.0f,  0.2f,  0.0f, 1.0f,  // Cara izquierda
        -0.6f,  1.0f, -0.2f,  1.0f, 1.0f,
        -0.6f, -0.2f, -0.2f,  1.0f, 0.0f,
        -0.6f, -0.2f, -0.2f,  1.0f, 0.0f,
        -0.6f, -0.2f,  0.2f,  0.0f, 0.0f,
        -0.6f,  1.0f,  0.2f,  0.0f, 1.0f,

        -0.2f,  1.0f,  0.2f,  0.0f, 1.0f,  // Cara derecha
        -0.2f,  1.0f, -0.2f,  1.0f, 1.0f,
        -0.2f, -0.2f, -0.2f,  1.0f, 0.0f,
        -0.2f, -0.2f, -0.2f,  1.0f, 0.0f,
        -0.2f, -0.2f,  0.2f,  0.0f, 0.0f,
        -0.2f,  1.0f,  0.2f,  0.0f, 1.0f,

        -0.6f, -0.2f, -0.2f,  0.0f, 1.0f, // Cara inferior
        -0.2f, -0.2f, -0.2f,  1.0f, 1.0f,
        -0.2f, -0.2f,  0.2f,  1.0f, 0.0f,
        -0.2f, -0.2f,  0.2f,  1.0f, 0.0f,
        -0.6f, -0.2f,  0.2f,  0.0f, 0.0f,
        -0.6f, -0.2f, -0.2f,  0.0f, 1.0f,

        -0.6f,  1.0f, -0.2f,  0.0f, 1.0f, // Cara superior
        -0.2f,  1.0f, -0.2f,  1.0f, 1.0f,
        -0.2f,  1.0f,  0.2f,  1.0f, 0.0f,
        -0.2f,  1.0f,  0.2f,  1.0f, 0.0f,
        -0.6f,  1.0f,  0.2f,  0.0f, 0.0f,
        -0.6f,  1.0f, -0.2f,  0.0f, 1.0f,

        // palo2
        0.2f, -0.2f, -0.2f,  0.0f, 0.0f,  // Cara trasera
        0.6f, -0.2f, -0.2f,  1.0f, 0.0f,
        0.6f,  1.0f, -0.2f,  1.0f, 1.0f,
        0.6f,  1.0f, -0.2f,  1.0f, 1.0f,
        0.2f,  1.0f, -0.2f,  0.0f, 1.0f,
        0.2f, -0.2f, -0.2f,  0.0f, 0.0f,

        0.2f, -0.2f,  0.2f,  0.0f, 0.0f, // Cara frontal
        0.6f, -0.2f,  0.2f,  1.0f, 0.0f,
        0.6f,  1.0f,  0.2f,  1.0f, 1.0f,
        0.6f,  1.0f,  0.2f,  1.0f, 1.0f,
        0.2f,  1.0f,  0.2f,  0.0f, 1.0f,
        0.2f, -0.2f,  0.2f,  0.0f, 0.0f,

        0.2f,  1.0f,  0.2f,  0.0f, 1.0f,  // Cara izquierda
        0.2f,  1.0f, -0.2f,  1.0f, 1.0f,
        0.2f, -0.2f, -0.2f,  1.0f, 0.0f,
        0.2f, -0.2f, -0.2f,  1.0f, 0.0f,
        0.2f, -0.2f,  0.2f,  0.0f, 0.0f,
        0.2f,  1.0f,  0.2f,  0.0f, 1.0f,

        0.6f,  1.0f,  0.2f,  0.0f, 1.0f,  // Cara derecha
        0.6f,  1.0f, -0.2f,  1.0f, 1.0f,
        0.6f, -0.2f, -0.2f,  1.0f, 0.0f,
        0.6f, -0.2f, -0.2f,  1.0f, 0.0f,
        0.6f, -0.2f,  0.2f,  0.0f, 0.0f,
        0.6f,  1.0f,  0.2f,  0.0f, 1.0f,

        0.2f, -0.2f, -0.2f,  0.0f, 1.0f, // Cara inferior
        0.6f, -0.2f, -0.2f,  1.0f, 1.0f,
        0.6f, -0.2f,  0.2f,  1.0f, 0.0f,
        0.6f, -0.2f,  0.2f,  1.0f, 0.0f,
        0.2f, -0.2f,  0.2f,  0.0f, 0.0f,
        0.2f, -0.2f, -0.2f,  0.0f, 1.0f,

        0.2f,  1.0f, -0.2f,  0.0f, 1.0f, // Cara superior
        0.6f,  1.0f, -0.2f,  1.0f, 1.0f,
        0.6f,  1.0f,  0.2f,  1.0f, 0.0f,
        0.6f,  1.0f,  0.2f,  1.0f, 0.0f,
        0.2f,  1.0f,  0.2f,  0.0f, 0.0f,
        0.2f,  1.0f, -0.2f,  0.0f, 1.0f,
    ];

    public U3D()
    {
        ShaderProgram = new Shader("U.Resources.Shaders.uShape.vert", "U.Resources.Shaders.uShape.frag");
        TextureImage = new("U.Resources.Images.container.jpg");
    }

    public void Load()
    {
        VBO = GL.GenBuffer();
        GL.BindBuffer(BufferTarget.ArrayBuffer, VBO);
        GL.BufferData(BufferTarget.ArrayBuffer, Vertices.Length * sizeof(float), Vertices, BufferUsageHint.StaticDraw);

        VAO = GL.GenVertexArray();
        GL.BindVertexArray(VAO);

        GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 5 * sizeof(float), 0);
        GL.EnableVertexAttribArray(0);

        GL.VertexAttribPointer(1, 3, VertexAttribPointerType.Float, false, 5 * sizeof(float), 3 * sizeof(float));
        GL.EnableVertexAttribArray(1);
    }

    public void Bind()
    {
        ShaderProgram.Use();
        GL.Enable(EnableCap.DepthTest);
        GL.BindVertexArray(VAO);
        TextureImage.Use(TextureUnit.Texture0);
    }


    public void Draw()
    {
        GL.DrawArrays(PrimitiveType.Triangles, 0, Vertices.Length / 5);
    }

    public void Dispose()
    {
        GL.DeleteBuffer(VBO);
        GL.DeleteVertexArray(VAO);
        // GL.DeleteBuffer(EBO);
        TextureImage.Dispose();
    }
}
