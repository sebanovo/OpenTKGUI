using OpenTKGUI.Estructura;
using OpenTKGUI.Utils;
using OpenTK.Mathematics;
using System.Globalization;

namespace OpenTKGUI
{
    class OBJLoader
    {
        public static Objeto CargarObjeto(string path, ArcRotateCamera camera, Luz luz)
        {
            string readText = File.ReadAllText(path);
            List<Vector3> vertices = new(500000);
            List<Vector2> textures = new(500000); List<Vector3> normales = new(500000);
            List<float> verticesArray = new(500000 * 5);
            List<uint> indicesArray = new(500000);

            StringReader sr = new StringReader(readText);
            string? line;
            while ((line = sr.ReadLine()) != null)
            {
                if (line.StartsWith("v "))
                {
                    string[] lineaActual = line.Split(" ");
                    Vector3 vertice;
                    vertice.X = float.Parse(lineaActual[1], CultureInfo.InvariantCulture);
                    vertice.Y = float.Parse(lineaActual[2], CultureInfo.InvariantCulture);
                    vertice.Z = float.Parse(lineaActual[3], CultureInfo.InvariantCulture);
                    vertices.Add(vertice);
                }
                else if (line.StartsWith("vt "))
                {
                    string[] lineaActual = line.Split(" ");
                    Vector2 texture;
                    texture.X = float.Parse(lineaActual[1], CultureInfo.InvariantCulture);
                    texture.Y = float.Parse(lineaActual[2], CultureInfo.InvariantCulture);
                    textures.Add(texture);
                }
                else if (line.StartsWith("vn "))
                {
                    string[] lineaActual = line.Split(" ");
                    Vector3 normal;
                    normal.X = float.Parse(lineaActual[1], CultureInfo.InvariantCulture);
                    normal.Y = float.Parse(lineaActual[2], CultureInfo.InvariantCulture);
                    normal.Z = float.Parse(lineaActual[3], CultureInfo.InvariantCulture);
                    normales.Add(normal);
                }
                else if (line.StartsWith("f "))
                {
                    string[] lineaActual = line.Split(" ", StringSplitOptions.RemoveEmptyEntries);
                    List<uint> faceIndices = [];
                    for (int i = 1; i < lineaActual.Length; i++)
                    {
                        string[] partesVertex = lineaActual[i].Split("/");
                        int indiceVertice = int.Parse(partesVertex[0]) - 1;

                        int indiceTextura = (partesVertex.Length > 1 && partesVertex[1] != "")
                            ? int.Parse(partesVertex[1]) - 1
                            : -1;

                        int indiceNormal = int.Parse(partesVertex[2]) - 1;

                        Vector3 vertice = vertices[indiceVertice];
                        Vector2 textura = (indiceTextura >= 0) ? textures[indiceTextura] : Vector2.Zero;
                        Vector3 normal = (indiceNormal >= 0) ? normales[indiceNormal] : Vector3.UnitZ;

                        verticesArray.Add(vertice.X);
                        verticesArray.Add(vertice.Y);
                        verticesArray.Add(vertice.Z);
                        verticesArray.Add(textura.X);
                        verticesArray.Add(textura.Y);

                        verticesArray.Add(normal.X);
                        verticesArray.Add(normal.Y);
                        verticesArray.Add(normal.Z);

                        uint index = (uint)(verticesArray.Count / 8 - 1);
                        faceIndices.Add(index);
                    }
                    for (int i = 1; i < faceIndices.Count - 1; i++)
                    {
                        indicesArray.Add(faceIndices[0]);
                        indicesArray.Add(faceIndices[i]);
                        indicesArray.Add(faceIndices[i + 1]);
                    }
                }
            }
            sr.Close();
            int k = 0;
            for (int i = 0; i < verticesArray.Count; i += 8)
            {
                k++;
                Console.WriteLine("{" + $"{verticesArray[i]}, {verticesArray[i + 1]}, {verticesArray[i + 2]}, {verticesArray[i + 3]}, {verticesArray[i + 4]}" + "}");
            }
            for (int i = 0; i < indicesArray.Count; i += 3)
            {
                Console.WriteLine("{" + $"{indicesArray[i]}, {indicesArray[i + 1]}, {indicesArray[i + 2]}" + "}");
            }
            Console.WriteLine(k);
            Console.WriteLine(verticesArray.Count);
            Console.WriteLine(indicesArray.Count);

            // Crear el Objeto
            Objeto newObjeto = new()
            {
                Name = "Stall"
            };
            Parte newParte = new Parte("Default", verticesArray, indicesArray, "C:\\Users\\HP\\Documents\\Visual Studio 2022\\Projects\\C#\\OpenTKGUI\\Resources\\Images\\Gray.jpg", camera, luz);
            newParte.Transformation.Position = new Vector3(0.0f, 0.0f, 0.0f);
            newObjeto.Add(newParte);
            // newObjeto.Transformation.Scale = new Vector3(0.1f / 20);
            return newObjeto;
        }
    }
}
