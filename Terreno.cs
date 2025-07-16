using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using OpenTKGUI.Estructura;
using OpenTKGUI.Utils;

namespace OpenTKGUI;

using static OpenTKGUI.Resources;
public class Terreno : Parte
{
    private static float SIZE = 10;
    private static int VERTEX_COUNT = 10;
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
            this.Vertices = [];
            this.Indices = [];
            this.Vertices.AddRange(CenterVerticesXYZ([.. verticesList]));
            this.Indices.AddRange(indicesList);
            this.Name = "Terreno";
            this.Shader = new Shader(Shaders.TerrenoVert, Shaders.TerrenoFrag);
            this.Texture = "C:\\Users\\HP\\Documents\\Visual Studio 2022\\Projects\\C#\\OpenTKGUI\\Resources\\Images\\ThinMatrix\\Grass.png";
            this.TextureObj = TextureManager.LoadTexture(this.Texture);
            this._textureUnit = TextureManager.GetNextTextureUnit(this.Texture);
            this.Camera = camera;
            Load();
        }
    }
    override public void Draw(Matrix4? modelPadre = null)
    {
        modelPadre ??= Matrix4.Identity;
        Matrix4 finalModel = CalculateModelMatrix() * (Matrix4)modelPadre;
        GL.BindVertexArray(_vao);
        // GL.Enable(EnableCap.DepthTest);
        // GL.Enable(EnableCap.CullFace);
        // GL.CullFace(TriangleFace.Back);

        Shader.Use();
        // TextureObj.Use();
        TextureObj.Use(_textureUnit);
        Shader
            // .SetInt("u_Texture", 0)
            .SetInt("u_Texture", TextureManager.ConvertUnitToInt(_textureUnit))
            .SetMat4("model", finalModel)
            .SetMat4("view", Camera.GetViewMatrix())
            .SetMat4("projection", Camera.GetProjectionMatrix());

        GL.BindBuffer(BufferTarget.ElementArrayBuffer, _ebo);
        GL.DrawElements(PrimitiveType.Triangles, Indices.Count, DrawElementsType.UnsignedInt, 0);
    }
}
