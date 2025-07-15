namespace OpenTKGUI.Estructura;

public class Vertice
{
    public float X { get; set; }
    public float Y { get; set; }
    public float Z { get; set; }
    public float U { get; set; }
    public float V { get; set; }
    public float NX { get; set; }
    public float NY { get; set; }
    public float NZ { get; set; }

    public Vertice() { }
    public Vertice(float x, float y, float z, float u, float v, float nx, float ny, float nz)
    {
        X = x;
        Y = y;
        Z = z;
        U = u;
        V = v;
        NX = nx;
        NY = ny;
        NZ = nz;
    }

    public float[] ToArray() => [X, Y, Z, U, V, NX, NY, NZ];
}