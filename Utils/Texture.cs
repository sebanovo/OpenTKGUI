using System.Reflection;
using OpenTK.Graphics.OpenGL4;
using StbImageSharp;

namespace OpenTKGUI.Utils;

public class Texture
{
    public int Handle { get; private set; }
    public string Name { get; private set; }
    public Texture(string textureResourceName)
    {
        Name = textureResourceName;
        ImageResult imageResult = Resources.Images.LoadEmbeddedImage(textureResourceName);

        Handle = GL.GenTexture();
        GL.BindTexture(TextureTarget.Texture2D, Handle);
        StbImage.stbi__vertically_flip_on_load_global = 1;
        GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, imageResult.Width, imageResult.Height, 0, PixelFormat.Rgba, PixelType.UnsignedByte, imageResult.Data);
        GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);

        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.LinearMipmapLinear); // Para mipmaps
        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
    }

    public void Use(TextureUnit unit = TextureUnit.Texture0)
    {
        GL.ActiveTexture(unit);
        GL.BindTexture(TextureTarget.Texture2D, Handle);
    }

    public void Dispose()
    {
        GL.DeleteTexture(Handle);
    }
}
