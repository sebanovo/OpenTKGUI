using System.Drawing.Imaging;
using System.Reflection;
using OpenTK.Graphics.OpenGL4;
using StbImageSharp;
using PixelFormat = OpenTK.Graphics.OpenGL4.PixelFormat;

namespace OpenTKGUI.Utils;

public class Texture(int ID)
{
    public int ID { get; private set; } = ID;

    public static Texture LoadFromResource(string resourcePath)
    {
        StbImage.stbi_set_flip_vertically_on_load(1);
        ImageResult image = Resources.Images.LoadEmbeddedImage(resourcePath);

        int id = GL.GenTexture();
        GL.ActiveTexture(TextureUnit.Texture0);
        GL.BindTexture(TextureTarget.Texture2D, id);
        GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, image.Width, image.Height, 0, PixelFormat.Rgba, PixelType.UnsignedByte, image.Data);
        GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);

        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.LinearMipmapLinear); // Para mipmaps
        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
        return new Texture(id);
    }

    public void Use(TextureUnit unit = TextureUnit.Texture0)
    {
        GL.ActiveTexture(unit);
        GL.BindTexture(TextureTarget.Texture2D, ID);
    }

    public void Dispose()
    {
        GL.DeleteTexture(ID);
    }
}
