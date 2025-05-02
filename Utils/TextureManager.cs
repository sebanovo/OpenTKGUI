using OpenTK.Graphics.OpenGL4;

namespace OpenTKGUI.Utils;
class TextureManager
{
    private static readonly Dictionary<string, Texture> _texturas = new Dictionary<string, Texture>();
    private static int _nextUnit = 0;

    public static Texture LoadTexture(string textureName)
    {
        if (!_texturas.TryGetValue(textureName, out var texture))
        {
            texture = new Texture(textureName);
            _texturas.Add(textureName, texture);
        }
        return texture;
    }

    public static TextureUnit GetNextTextureUnit()
    {
        var unit = TextureUnit.Texture0 + _nextUnit;
        _nextUnit = (_nextUnit + 1) % 16;
        return unit;
    }

    public static int ConvertUnitToInt(TextureUnit textureUnit)
    {
        return (int)textureUnit - (int)TextureUnit.Texture0;
    }
}