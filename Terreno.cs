using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTKGUI.Estructura;
using OpenTKGUI.Utils;

namespace OpenTKGUI;

using static OpenTKGUI.Resources;
public class Terreno : Parte
{
    private static float SIZE = 100;
    private static int VERTEX_COUNT = 1090;
    private string _textura { get; set; }
    private ArcRotateCamera _camera;

    public Terreno(ArcRotateCamera camera) : base()
    {
        _camera = camera;

        float height = 0;
        Vector3 normal = new(0, 1, 0);

        List<float> verticesList = [];
        for (int cZ = 0; cZ < VERTEX_COUNT; cZ++)
        {
            for (int cX = 0; cX < VERTEX_COUNT; cX++)
            {
                verticesList.Add(cX / ((float)VERTEX_COUNT - 1) * SIZE);
                verticesList.Add(height);
                verticesList.Add(cZ / ((float)VERTEX_COUNT - 1) * SIZE);

                verticesList.Add(cX / ((float)VERTEX_COUNT - 1));
                verticesList.Add(cZ / ((float)VERTEX_COUNT - 1));
                verticesList.Add(normal.X);
                verticesList.Add(normal.Y);
                verticesList.Add(normal.Z);
            }
        }

        List<uint> indicesList = [];
        for (uint gz = 0; gz < VERTEX_COUNT - 1; gz++)
        {
            for (uint gx = 0; gx < VERTEX_COUNT - 1; gx++)
            {
                uint topLeft = (uint)(gz * VERTEX_COUNT) + gx;
                uint topRight = topLeft + 1;
                uint bottomLeft = (uint)((gz + 1) * VERTEX_COUNT) + gx;
                uint bottomRight = bottomLeft + 1;
                indicesList.Add(topLeft);
                indicesList.Add(bottomLeft);
                indicesList.Add(topRight);
                indicesList.Add(topRight);
                indicesList.Add(bottomLeft);
                indicesList.Add(bottomRight);
            }
        }
        this.Vertices = [];
        this.Indices = [];
        this.Vertices.AddRange(CenterVerticesXYZ([.. verticesList]));
        this.Indices.AddRange(indicesList);
        this.Name = "Terreno";
        this.Shader = new Shader(Shaders.TerrenoVert, Shaders.TerrenoFrag);
        // this.Texture = "C:\\Users\\HP\\Documents\\Visual Studio 2022\\Projects\\C#\\OpenTKGUI\\Resources\\Images\\ThinMatrix\\Grass.png";
        // this.Texture = "C:\\Users\\HP\\Documents\\Visual Studio 2022\\Projects\\C#\\OpenTKGUI\\Resources\\Images\\Terreno\\grassy2.png";
        // (this.TextureObj, _) = TextureManager.LoadTexture(this.Texture);
        // this._textureUnit = TextureManager.GetNextTextureUnit(this.Texture);

        this.Texture = "C:\\Users\\HP\\Documents\\Visual Studio 2022\\Projects\\C#\\OpenTKGUI\\Resources\\Images\\Terreno\\Grass.png";
        (texture1, isTransparency) = TextureManager.LoadTexture(this.Texture);
        this.t1 = TextureManager.GetNextTextureUnit(this.Texture);
        this.Texture = "C:\\Users\\HP\\Documents\\Visual Studio 2022\\Projects\\C#\\OpenTKGUI\\Resources\\Images\\Terreno\\mud.png";
        (texture2, isTransparency) = TextureManager.LoadTexture(this.Texture);
        this.t2 = TextureManager.GetNextTextureUnit(this.Texture);
        this.Texture = "C:\\Users\\HP\\Documents\\Visual Studio 2022\\Projects\\C#\\OpenTKGUI\\Resources\\Images\\Terreno\\grassFlowers.png";
        (texture3, isTransparency) = TextureManager.LoadTexture(this.Texture);
        this.t3 = TextureManager.GetNextTextureUnit(this.Texture);
        this.Texture = "C:\\Users\\HP\\Documents\\Visual Studio 2022\\Projects\\C#\\OpenTKGUI\\Resources\\Images\\Terreno\\path.png";
        (texture4, isTransparency) = TextureManager.LoadTexture(this.Texture);
        this.t4 = TextureManager.GetNextTextureUnit(this.Texture);
        this.Texture = "C:\\Users\\HP\\Documents\\Visual Studio 2022\\Projects\\C#\\OpenTKGUI\\Resources\\Images\\Terreno\\blendMap.png";
        (texture5, isTransparency) = TextureManager.LoadTexture(this.Texture);
        this.t5 = TextureManager.GetNextTextureUnit(this.Texture);
        this.Camera = camera;
        Load();
    }
    Texture texture1;
    Texture texture2;
    Texture texture3;
    Texture texture4;
    Texture texture5;

    TextureUnit t1;
    TextureUnit t2;
    TextureUnit t3;
    TextureUnit t4;
    TextureUnit t5;
    override public void Draw(Matrix4? modelPadre = null)
    {
        modelPadre ??= Matrix4.Identity;
        Matrix4 finalModel = CalculateModelMatrix() * (Matrix4)modelPadre;
        GL.BindVertexArray(_vao);
        GL.Enable(EnableCap.DepthTest);
        GL.Enable(EnableCap.CullFace);
        GL.CullFace(TriangleFace.Back);

        Shader.Use();
        // TextureObj.Use();
        texture1.Use(t1);
        texture2.Use(t2);
        texture3.Use(t3);
        texture4.Use(t4);
        texture5.Use(t5);

        Shader
            // .SetInt("u_Texture", 0)
            .SetInt("u_BackgroundTexture", TextureManager.ConvertUnitToInt(t1))
            .SetInt("u_RTexture", TextureManager.ConvertUnitToInt(t2))
            .SetInt("u_GTexture", TextureManager.ConvertUnitToInt(t3))
            .SetInt("u_BTexture", TextureManager.ConvertUnitToInt(t4))
            .SetInt("u_BlendMap", TextureManager.ConvertUnitToInt(t5))
            .SetMat4("model", finalModel)
            .SetMat4("view", Camera.GetViewMatrix())
            .SetMat4("projection", Camera.GetProjectionMatrix());

        GL.BindBuffer(BufferTarget.ElementArrayBuffer, _ebo);
        GL.DrawElements(PrimitiveType.Triangles, Indices.Count, DrawElementsType.UnsignedInt, 0);
    }
}
