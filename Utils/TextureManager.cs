using OpenTK.Graphics.OpenGL4;

namespace OpenTKGUI.Utils;

class TextureManager
{
    private static readonly Dictionary<string, (Texture, bool)> _texturas = new Dictionary<string, (Texture, bool)>();
    private static readonly Dictionary<string, TextureUnit> _texturaUnidades = new();
    private static int _nextUnit = 0;

    public static (Texture, bool) LoadTexture(string textureName)
    {
        if (!_texturas.TryGetValue(textureName, out var texture))
        {
            texture = Texture.LoadFromResource(textureName);
            _texturas[textureName] = texture;
        }
        return texture;
    }

    public static TextureUnit GetNextTextureUnit(string textureName)
    {
        if (_texturaUnidades.TryGetValue(textureName, out var unit))
        {
            return unit;
        }

        if (_nextUnit >= 16) // o usar GL.GetInteger(GL_MAX_TEXTURE_IMAGE_UNITS)
        {
            throw new Exception("Se excedió el número máximo de unidades de textura.");
        }

        unit = TextureUnit.Texture0 + _nextUnit;
        _texturaUnidades[textureName] = unit;
        _nextUnit++;
        return unit;
    }

    public static int ConvertUnitToInt(TextureUnit textureUnit)
    {
        return (int)textureUnit - (int)TextureUnit.Texture0;
    }
}