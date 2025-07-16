using System.Reflection;

namespace OpenTKGUI;

public class Resources
{
    public static class Shaders
    {
        public static string Objeto3DVert => LoadEmbeddedShader("OpenTKGUI.Resources.Shaders.Objeto3D.vert");
        public static string Objeto3DFrag => LoadEmbeddedShader("OpenTKGUI.Resources.Shaders.Objeto3D.frag");
        public static string AxisVert => LoadEmbeddedShader("OpenTKGUI.Resources.Shaders.Axis.vert");
        public static string AxisFrag => LoadEmbeddedShader("OpenTKGUI.Resources.Shaders.Axis.frag");
        public static string TerrenoVert => LoadEmbeddedShader("OpenTKGUI.Resources.Shaders.Terreno.vert");
        public static string TerrenoFrag => LoadEmbeddedShader("OpenTKGUI.Resources.Shaders.Terreno.frag");

        private static string LoadEmbeddedShader(string resourceName)
        {

            Stream stream = (Assembly.GetExecutingAssembly()?.GetManifestResourceStream(resourceName)) ??
                             throw new Exception($"Shader resource {resourceName} not found.");
            StreamReader reader = new(stream);
            return reader.ReadToEnd();
        }
    }
}
