using OpenTKGUI.Estructura;
using OpenTKGUI.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Mathematics;
using System.Globalization;

namespace OpenTKGUI
{
    class OBJLoader
    {
        public static Objeto CargarObjeto(string path, ArcRotateCamera camera)
        {
            string readText = File.ReadAllText(path);
            List<Vector3> vertices = new(500000);
            List<Vector2> textures = new(500000); List<Vector3> normales = new(500000);
            List<float> verticesArray = new(500000 * 5);
            List<uint> indicesArray = new(500000);

            StringReader sr = new StringReader(readText);
            while (true)
            {
                string? line = sr.ReadLine();
                if (line == null)
                {
                    break;
                }
                else if (line.StartsWith("v "))
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
                    texture.Y = 1.0f - float.Parse(lineaActual[2], CultureInfo.InvariantCulture);
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
                    string[] lineActual = line.Split(" ");
                    string[] v1 = lineActual[1].Split("/");
                    string[] v2 = lineActual[2].Split("/");
                    string[] v3 = lineActual[3].Split("/");

                    Vector3 vertice;
                    Vector2 textura;
                    int indiceVertice;
                    int indiceTextura;

                    // v1
                    indiceVertice = int.Parse(v1[0]) - 1;
                    indiceTextura = int.Parse(v1[1]) - 1;
                    vertice = vertices[indiceVertice];
                    textura = textures[indiceTextura];
                    verticesArray.Add(vertice.X);
                    verticesArray.Add(vertice.Y);
                    verticesArray.Add(vertice.Z);
                    verticesArray.Add(textura.X);
                    verticesArray.Add(textura.Y);

                    // indicesArray.Add((uint)index++);

                    // v2
                    indiceVertice = int.Parse(v2[0]) - 1;
                    indiceTextura = int.Parse(v2[1]) - 1;
                    vertice = vertices[indiceVertice];
                    textura = textures[indiceTextura];
                    verticesArray.Add(vertice.X);
                    verticesArray.Add(vertice.Y);
                    verticesArray.Add(vertice.Z);
                    verticesArray.Add(textura.X);
                    verticesArray.Add(textura.Y);

                    // indicesArray.Add((uint)index++);

                    // v3
                    indiceVertice = int.Parse(v3[0]) - 1;
                    indiceTextura = int.Parse(v3[1]) - 1;
                    vertice = vertices[indiceVertice];
                    textura = textures[indiceTextura];
                    verticesArray.Add(vertice.X);
                    verticesArray.Add(vertice.Y);
                    verticesArray.Add(vertice.Z);
                    verticesArray.Add(textura.X);
                    verticesArray.Add(textura.Y);

                    // indicesArray.Add((uint)index++);
                }
            }
            sr.Close();
            uint j = 0;
            for (uint i = 0; i < verticesArray.Count; i += 5)
            {
                indicesArray.Add(j);
                j++;
            }
            int k = 0;
            for (int i = 0; i < verticesArray.Count; i += 5)
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
            Parte newParte = new Parte("Default", verticesArray, indicesArray, "OpenTKGUI.Resources.Images.Minecraft.Zombie.png", camera);
            newParte.Transformation.Position = new Vector3(0.0f, 0.0f, 0.0f);
            newObjeto.Add(newParte);
            return newObjeto;
        }
    }
}
