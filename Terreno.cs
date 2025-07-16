using OpenTK.Mathematics;
using OpenTKGUI.Estructura;
using OpenTKGUI.Utils;

namespace OpenTKGUI;

using static OpenTKGUI.Resources;
public class Terreno
{
    private static float SIZE = 2;
    private static int VERTEX_COUNT = 10;
    private string _textura { get; set; }
    private ArcRotateCamera _camera;
    public Objeto model;

    public Terreno(string textura, ArcRotateCamera camera)
    {
        this._textura = textura;
        _camera = camera;
        model = generarObjeto();
    }

    public void Draw()
    {
        this.model.Draw();
    }

    private Objeto generarObjeto()
    {
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
        Parte p1 = new Parte(
            "Default", verticesList, indicesList,
            this._textura, this._camera, new Luz(Vector3.Zero, Vector3.Zero)
        );

        p1.Shader = new Shader(Shaders.TerrenoVert, Shaders.TerrenoFrag);
        Objeto o1 = new Objeto();
        o1.Name = "Terreno";
        o1.Add(p1);
        return o1;
    }
}
