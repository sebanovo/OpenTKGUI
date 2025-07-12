using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;

namespace OpenTKGUI.Utils;

public class Shader
{
    public int ID;
    private readonly Dictionary<string, int> _ubicacionesUniforms;
    public Shader(string vertexResourceName, string fragmentResourceName)
    {
        try
        {
            int vertexShader = GL.CreateShader(ShaderType.VertexShader);
            GL.ShaderSource(vertexShader, vertexResourceName);
            CompileShader(vertexShader);

            int fragmentShader = GL.CreateShader(ShaderType.FragmentShader);
            GL.ShaderSource(fragmentShader, fragmentResourceName);
            CompileShader(fragmentShader);

            ID = GL.CreateProgram();
            GL.AttachShader(ID, vertexShader);
            GL.AttachShader(ID, fragmentShader);
            LinkProgram(ID);

            GL.DetachShader(ID, vertexShader);
            GL.DetachShader(ID, fragmentShader);
            GL.DeleteShader(vertexShader);
            GL.DeleteShader(fragmentShader);

            GL.GetProgram(ID, GetProgramParameterName.ActiveUniforms, out var numberOfUniforms);
            _ubicacionesUniforms = new Dictionary<string, int>();

            for (int index = 0; index < numberOfUniforms; index++)
            {
                var clave = GL.GetActiveUniform(ID, index, out _, out _);
                var ubicacion = GL.GetUniformLocation(ID, clave);
                _ubicacionesUniforms.Add(clave, ubicacion);
            }
        }
        catch (Exception e)
        {
            Console.Write(e.Message);
        }
    }

    public void Use()
    {
        GL.UseProgram(ID);
    }

    public static void CompileShader(int shader)
    {
        GL.CompileShader(shader);
        GL.GetShader(shader, ShaderParameter.CompileStatus, out var code);
        if (code != (int)All.True)
        {
            var infoLog = GL.GetShaderInfoLog(shader);
            throw new Exception($"Error ocurrido mientras se compilaba el shader: ({shader}).\n{infoLog}");
        }
    }
    private static void LinkProgram(int program)
    {
        GL.LinkProgram(program);
        GL.GetProgram(program, GetProgramParameterName.LinkStatus, out var code);
        if (code != (int)All.True)
        {
            throw new Exception($"Error ocurrido mientras se vinculaba el programa ({program})");
        }
    }

    public Shader SetInt(string name, int value)
    {
        GL.Uniform1(_ubicacionesUniforms[name], value);
        return this;
    }

    public Shader SetFloat(string name, float value)
    {
        GL.Uniform1(_ubicacionesUniforms[name], value);
        return this;
    }

    public Shader SetMat4(string name, Matrix4 value)
    {
        GL.UniformMatrix4(_ubicacionesUniforms[name], false, ref value);
        return this;
    }

    public Shader SetVec3(string name, Vector3 value)
    {
        GL.Uniform3(_ubicacionesUniforms[name], ref value);
        return this;
    }

    public Shader SetVec4(string name, Vector4 value)
    {
        GL.Uniform4(_ubicacionesUniforms[name], ref value);
        return this;
    }
}
