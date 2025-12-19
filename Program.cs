using OpenTK.Mathematics;
using OpenTK.Windowing.Desktop;
using U.src;

namespace U;
class Program
{
    [STAThread]
    public static void Main(string[] args)
    {
        var nativeWindowSettings = new NativeWindowSettings()
        {
            ClientSize = new Vector2i(800, 600),
            Title = "U"
        };
        using Game game = new(GameWindowSettings.Default, nativeWindowSettings);
        game.Run();
    }
}
