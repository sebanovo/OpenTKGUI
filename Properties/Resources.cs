using System;
using System.Reflection;
using System.Text.Json;
using StbImageSharp;

namespace OpenTKGUI.Properties;

public class Resources
{
    public class Images
    {
        public static ImageResult Wood => LoadEmbeddedImage("OpenTKGUI.Resources.Images.Wood.jpg");
        public static ImageResult BlueMetal => LoadEmbeddedImage("OpenTKGUI.Resources.Images.BlueMetal.jpg");
        public static ImageResult Wall => LoadEmbeddedImage("OpenTKGUI.Resources.Images.Wall.png");
        public static ImageResult Bricks => LoadEmbeddedImage("OpenTKGUI.Resources.Images.Bricks.jpg");

        private static ImageResult LoadEmbeddedImage(string textureResourceName)
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
        public static string CrossHairVert => LoadEmbeddedShader("OpenTKGUI.Resources.Shaders.CrossHair.vert");
        public static string CrossHairFrag => LoadEmbeddedShader("OpenTKGUI.Resources.Shaders.CrossHair.frag");

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
            public float[] Vertices { get; set; } = [];
            public uint[] Indices { get; set; } = [];
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

            ShapeData? shape = JsonSerializer.Deserialize<ShapeData>(stream, options)
                ?? throw new InvalidDataException($"Invalid JSON format in {resourceName}");

            if (shape.Vertices == null || shape.Vertices.Length == 0)
            {
                throw new InvalidDataException($"No vertices found in {resourceName}");
            }

            shape.Indices ??= [];

            return shape;
        }
    }
}
