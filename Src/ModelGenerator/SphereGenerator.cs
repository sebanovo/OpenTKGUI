using System;
using System.Text.Json;

namespace OpenTK.Src.ModelGenator;

// ejemplo de llamada
// SphereGenerator.GenerateSphereJson("Sphere.jsonc", 30, 20, 0.2f);
public class SphereGenerator
{
    public static void GenerateSphereJson(string filePath, int sectorCount = 20, int stackCount = 20, float radius = 1.0f)
    {
        var sphereData = new
        {
            Vertices = new List<float>(),    // x,y,z
            Normals = new List<float>(),     // nx,ny,nz
            TexCoords = new List<float>(),   // u,v
            Indices = new List<uint>()
        };

        float sectorStep = 2 * MathF.PI / sectorCount;
        float stackStep = MathF.PI / stackCount;
        float lengthInv = 1.0f / radius;

        for (int i = 0; i <= stackCount; ++i)
        {
            float stackAngle = MathF.PI / 2 - i * stackStep;
            float xy = radius * MathF.Cos(stackAngle);
            float z = radius * MathF.Sin(stackAngle);

            for (int j = 0; j <= sectorCount; ++j)
            {
                float sectorAngle = j * sectorStep;

                // Posición del vértice
                float x = xy * MathF.Cos(sectorAngle);
                float y = xy * MathF.Sin(sectorAngle);
                sphereData.Vertices.Add(x);
                sphereData.Vertices.Add(y);
                sphereData.Vertices.Add(z);

                // Normal (normalizada)
                sphereData.Normals.Add(x * lengthInv);
                sphereData.Normals.Add(y * lengthInv);
                sphereData.Normals.Add(z * lengthInv);

                // Coordenadas de textura
                sphereData.TexCoords.Add((float)j / sectorCount);
                sphereData.TexCoords.Add((float)i / stackCount);
            }
        }

        // Generar índices
        for (int i = 0; i < stackCount; ++i)
        {
            int k1 = i * (sectorCount + 1);
            int k2 = k1 + sectorCount + 1;

            for (int j = 0; j < sectorCount; ++j, ++k1, ++k2)
            {
                if (i != 0)
                {
                    sphereData.Indices.Add((uint)k1);
                    sphereData.Indices.Add((uint)k2);
                    sphereData.Indices.Add((uint)(k1 + 1));
                }

                if (i != (stackCount - 1))
                {
                    sphereData.Indices.Add((uint)(k1 + 1));
                    sphereData.Indices.Add((uint)k2);
                    sphereData.Indices.Add((uint)(k2 + 1));
                }
            }
        }

        var options = new JsonSerializerOptions { WriteIndented = true };
        var combinedData = new
        {
            Vertices = CombineVertexData(sphereData.Vertices, sphereData.TexCoords),
            Indices = sphereData.Indices
        };
        File.WriteAllText(filePath, JsonSerializer.Serialize(combinedData, options));
    }

    private static List<float> CombineVertexData(List<float> positions, List<float> texCoords)
    {
        var combined = new List<float>();
        int vertexCount = positions.Count / 3;

        for (int i = 0; i < vertexCount; i++)
        {
            // Añadir posición (x,y,z)
            combined.Add(positions[i * 3]);
            combined.Add(positions[i * 3 + 1]);
            combined.Add(positions[i * 3 + 2]);

            // Añadir coordenadas de textura (u,v)
            combined.Add(texCoords[i * 2]);
            combined.Add(texCoords[i * 2 + 1]);
        }

        return combined;
    }
}