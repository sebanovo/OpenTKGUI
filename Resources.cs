using System;
using System.Reflection;
using System.Text.Json;
using StbImageSharp;

namespace OpenTKGUI;

public class Resources
{
    public class Images
    {
        public static string Wood => "OpenTKGUI.Resources.Images.Wood.jpg";
        public static string BlueMetal => "OpenTKGUI.Resources.Images.BlueMetal.jpg";
        public static string Wall => "OpenTKGUI.Resources.Images.Wall.png";
        public static string Bricks => "OpenTKGUI.Resources.Images.Bricks.jpg";

        public static ImageResult LoadEmbeddedImage(string textureResourceName)
        {
            Stream stream = Assembly.GetExecutingAssembly()?.GetManifestResourceStream(textureResourceName)
                            ?? throw new Exception($"Texture resource {textureResourceName} not found.");
            ImageResult image = ImageResult.FromStream(stream, ColorComponents.RedGreenBlueAlpha);
            return image;
        }
    }

    public static class Shaders
    {
        public static string Objeto3DVert => LoadEmbeddedShader("OpenTKGUI.Resources.Shaders.Objeto3D.vert");
        public static string Objeto3DFrag => LoadEmbeddedShader("OpenTKGUI.Resources.Shaders.Objeto3D.frag");
        public static string AxisVert => LoadEmbeddedShader("OpenTKGUI.Resources.Shaders.Axis.vert");
        public static string AxisFrag => LoadEmbeddedShader("OpenTKGUI.Resources.Shaders.Axis.frag");

        private static string LoadEmbeddedShader(string resourceName)
        {

            Stream stream = (Assembly.GetExecutingAssembly()?.GetManifestResourceStream(resourceName)) ??
                             throw new Exception($"Shader resource {resourceName} not found.");
            StreamReader reader = new(stream);
            return reader.ReadToEnd();
        }
    }
    public static class Config
    {
        public class ShapeData
        {
            public List<float> Vertices { get; set; } = [];
            public List<uint> Indices { get; set; } = [];
        }

        public static ShapeData U => LoadShapeConfig("OpenTKGUI.Resources.Config.U.jsonc");
        public static ShapeData Cube => LoadShapeConfig("OpenTKGUI.Resources.Config.Cube.jsonc");
        public static ShapeData Pyramid => LoadShapeConfig("OpenTKGUI.Resources.Config.Pyramid.jsonc");
        public static ShapeData Sphere => LoadShapeConfig("OpenTKGUI.Resources.Config.Sphere.jsonc");
        public static ShapeData Cylinder => LoadShapeConfig("OpenTKGUI.Resources.Config.Cylinder.jsonc");

        // Método de carga optimizado
        private static ShapeData LoadShapeConfig(string resourceName)
        {
            using Stream stream = Assembly.GetExecutingAssembly()?
                .GetManifestResourceStream(resourceName)
                ?? throw new FileNotFoundException($"Resource {resourceName} not found");

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                ReadCommentHandling = JsonCommentHandling.Skip
            };

            ShapeData? shape = (JsonSerializer.Deserialize<ShapeData>(stream, options)
                ?? throw new InvalidDataException($"Invalid JSON format in {resourceName}"))
                ?? throw new InvalidDataException($"Failed to deserialize {resourceName}");

            if (shape.Vertices.Count == 0 )
            {
                throw new InvalidDataException($"No vertices found in {resourceName}");

            }

            if (shape.Vertices.Count == 0)
            {
                throw new InvalidDataException($"No vertices found in {resourceName}");

            }
            return shape;
        }
    }
}
