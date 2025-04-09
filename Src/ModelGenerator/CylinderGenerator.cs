using System;
using System.Text.Json;

namespace OpenTKGUI.Src.ModelGenator;

// uso: 
//   CylinderGenerator.GenerateCylinderJson(
//             filePath: "Cylinder.jsonc",
//             sectorCount: 36,
//             radius: 0.2f,
//             height: 1.0f
//         );
public static class CylinderGenerator
{
    public static void GenerateCylinderJson(string filePath, int sectorCount = 36, float radius = 1.0f, float height = 2.0f)
    {
        var vertices = BuildVertices(sectorCount, radius, height);
        var indices = BuildIndices(sectorCount);

        var vertexData = new List<float>();
        var texCoords = new List<float>();

        // Separar coordenadas de posición y textura (si es necesario)
        // Esto depende de cómo quieras estructurar tus datos finales
        for (int i = 0; i < vertices.Count; i += 5)
        {
            vertexData.Add(vertices[i]);     // x
            vertexData.Add(vertices[i + 1]);   // y
            vertexData.Add(vertices[i + 2]);   // z
            texCoords.Add(vertices[i + 3]);    // u
            texCoords.Add(vertices[i + 4]);    // v
        }

        var combinedVertices = CombineVertexData(vertexData, texCoords);

        var result = new
        {
            vertices = combinedVertices,
            indices = indices
        };

        var options = new JsonSerializerOptions
        {
            WriteIndented = true,
            NumberHandling = System.Text.Json.Serialization.JsonNumberHandling.AllowNamedFloatingPointLiterals
        };

        string json = JsonSerializer.Serialize(result, options);
        File.WriteAllText(filePath, json);
    }

    private static List<float> CombineVertexData(List<float> positions, List<float> texCoords)
    {
        List<float> combined = new List<float>();
        int vertexCount = positions.Count / 3;

        for (int i = 0; i < vertexCount; i++)
        {
            // Posición (x, y, z)
            combined.Add(positions[i * 3]);
            combined.Add(positions[i * 3 + 1]);
            combined.Add(positions[i * 3 + 2]);

            // Coordenadas de textura (u, v)
            combined.Add(texCoords[i * 2]);
            combined.Add(texCoords[i * 2 + 1]);
        }

        return combined;
    }

    private static List<float> BuildVertices(int sectorCount, float radius, float height)
    {
        List<float> vertices = new List<float>();
        List<float> unitVertices = GetUnitCircleVertices(sectorCount);

        // Lado del cilindro
        for (int i = 0; i < 2; ++i)
        {
            float h = -height / 2.0f + i * height;
            float t = 1.0f - i;

            for (int j = 0, k = 0; j <= sectorCount; ++j, k += 3)
            {
                float ux = unitVertices[k];
                float uy = unitVertices[k + 1];

                vertices.Add(ux * radius);           // x
                vertices.Add(uy * radius);           // y
                vertices.Add(h);                     // z
                vertices.Add((float)j / sectorCount);// u
                vertices.Add(t);                     // v
            }
        }

        // Bases del cilindro
        for (int i = 0; i < 2; ++i)
        {
            float h = -height / 2.0f + i * height;

            // Centro
            vertices.Add(0); vertices.Add(0); vertices.Add(h);
            vertices.Add(0.5f); vertices.Add(0.5f);

            for (int j = 0, k = 0; j < sectorCount; ++j, k += 3)
            {
                float ux = unitVertices[k];
                float uy = unitVertices[k + 1];

                vertices.Add(ux * radius);            // x
                vertices.Add(uy * radius);            // y
                vertices.Add(h);                      // z
                vertices.Add(-ux * 0.5f + 0.5f);      // u
                vertices.Add(-uy * 0.5f + 0.5f);      // v
            }
        }

        return vertices;
    }

    private static List<int> BuildIndices(int sectorCount)
    {
        List<int> indices = new List<int>();
        int k1 = 0;
        int k2 = sectorCount + 1;

        // Lados
        for (int i = 0; i < sectorCount; ++i, ++k1, ++k2)
        {
            indices.Add(k1);
            indices.Add(k1 + 1);
            indices.Add(k2);

            indices.Add(k2);
            indices.Add(k1 + 1);
            indices.Add(k2 + 1);
        }

        // Bases
        int baseCenterIndex = (sectorCount + 1) * 2;
        int topCenterIndex = baseCenterIndex + sectorCount + 1;

        // Base inferior
        for (int i = 0, k = baseCenterIndex + 1; i < sectorCount; ++i, ++k)
        {
            indices.Add(baseCenterIndex);
            indices.Add(i < sectorCount - 1 ? k + 1 : baseCenterIndex + 1);
            indices.Add(k);
        }

        // Base superior
        for (int i = 0, k = topCenterIndex + 1; i < sectorCount; ++i, ++k)
        {
            indices.Add(topCenterIndex);
            indices.Add(k);
            indices.Add(i < sectorCount - 1 ? k + 1 : topCenterIndex + 1);
        }

        return indices;
    }

    private static List<float> GetUnitCircleVertices(int sectorCount)
    {
        const float PI = 3.1415926f;
        float sectorStep = 2 * PI / sectorCount;

        List<float> unitCircleVertices = new List<float>();
        for (int i = 0; i <= sectorCount; ++i)
        {
            float sectorAngle = i * sectorStep;
            unitCircleVertices.Add((float)Math.Cos(sectorAngle)); // x
            unitCircleVertices.Add((float)Math.Sin(sectorAngle)); // y
            unitCircleVertices.Add(0);                            // z
        }
        return unitCircleVertices;
    }
}