using System;
using System.Reflection;
using System.Text.Json;
using StbImageSharp;

namespace U.Properties;

public class Resources
{
    public class Images
    {
        public static ImageResult wood
        {
            get
            {
                return LoadEmbeddedImage("U.Resources.Images.wood.jpg");
            }
        }

        public static ImageResult blueMetal
        {
            get
            {
                return LoadEmbeddedImage("U.Resources.Images.blueMetal.jpg");
            }
        }

        public static ImageResult wall
        {
            get
            {
                return LoadEmbeddedImage("U.Resources.Images.wall.png");
            }
        }

        public static ImageResult bricks
        {
            get
            {
                return LoadEmbeddedImage("U.Resources.Images.bricks.jpg");
            }
        }

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
        public static string uShapeVert
        {
            get
            {
                return LoadEmbeddedShader("U.Resources.Shaders.uShape.vert");
            }
        }

        public static string uShapeFrag
        {
            get
            {
                return LoadEmbeddedShader("U.Resources.Shaders.uShape.frag");
            }
        }

        public static string axisVert
        {
            get
            {
                return LoadEmbeddedShader("U.Resources.Shaders.axis.vert");
            }
        }

        public static string axisFrag
        {
            get
            {
                return LoadEmbeddedShader("U.Resources.Shaders.axis.frag");
            }
        }

        public static string crossHairVert
        {
            get
            {
                return LoadEmbeddedShader("U.Resources.Shaders.crossHair.vert");
            }
        }

        public static string crossHairFrag
        {
            get
            {
                return LoadEmbeddedShader("U.Resources.Shaders.crossHair.frag");
            }
        }

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
        public static float[] uShape
        {
            get
            {
                return LoadEmbeddedConfig("U.Resources.Config.U.jsonc");
            }
        }

        public static float[] cube
        {
            get
            {
                return LoadEmbeddedConfig("U.Resources.Config.Cube.jsonc");
            }
        }

        public static float[] pyramid
        {
            get
            {
                return LoadEmbeddedConfig("U.Resources.Config.Pyramid.jsonc");
            }
        }

        private class Json
        {
            public float[]? Vertices { get; set; }
        }

        private static float[] LoadEmbeddedConfig(string resourceName)
        {
            Stream stream = (Assembly.GetExecutingAssembly()?.GetManifestResourceStream(resourceName)) ??
                             throw new Exception($"Json resource {resourceName} not found.");
            StreamReader reader = new(stream);

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                ReadCommentHandling = JsonCommentHandling.Skip
            };

            Json? shape = JsonSerializer.Deserialize<Json>(reader.ReadToEnd(), options)
            ?? throw new Exception("No se pudo cargar deserializar el archivo: " + resourceName);

            if (shape.Vertices == null)
            {
                throw new Exception("No se puedo cargar los vertices de: " + resourceName);
            }
            return [.. shape.Vertices];
        }
    }
}
