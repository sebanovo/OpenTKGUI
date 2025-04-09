using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using System.Reflection;

namespace OpenTKGUI.Src.Utils;

public class Shader
{
    public int ID;
    public Shader(string vertexResourceName, string fragmentResourceName)
    {
        try
        {
            int vertexShader = GL.CreateShader(ShaderType.VertexShader);
            GL.ShaderSource(vertexShader, vertexResourceName);
            GL.CompileShader(vertexShader);

            int fragmentShader = GL.CreateShader(ShaderType.FragmentShader);
            GL.ShaderSource(fragmentShader, fragmentResourceName);
            GL.CompileShader(fragmentShader);

            ID = GL.CreateProgram();
            GL.AttachShader(ID, vertexShader);
            GL.AttachShader(ID, fragmentShader);
            GL.LinkProgram(ID);
            GL.DeleteShader(vertexShader);
            GL.DeleteShader(fragmentShader);

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

    public Shader SetInt(string name, int value)
    {
        GL.Uniform1(GL.GetUniformLocation(ID, name), value);
        return this;
    }

    public Shader SetFloat(string name, float value)
    {
        GL.Uniform1(GL.GetUniformLocation(ID, name), value);
        return this;
    }

    public Shader SetMat4(string name, Matrix4 value)
    {
        GL.UniformMatrix4(GL.GetUniformLocation(ID, name), true, ref value);
        return this;
    }

    public Shader SetVec3(string name, Vector3 value)
    {
        GL.Uniform3(GL.GetUniformLocation(ID, name), ref value);
        return this;
    }

    public Shader SetVec4(string name, Vector4 value)
    {
        GL.Uniform4(GL.GetUniformLocation(ID, name), ref value);
        return this;
    }
}
