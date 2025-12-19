using System.Diagnostics;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTK.Graphics.OpenGL4;

using U.Src.Models._2D;
using U.Src.Models._3D;
using U.Src.Utils;
using U.Properties;
using U.Src.Models._3D.Factories;
using U.Src.Models._3D.Composite;

#pragma warning disable CS8618

namespace U.src
{
    public class Game : GameWindow
    {
        Escenario _escenario;
        // Camera
        FlyCamera _camera;
        Vector2 _lastPosition;
        Color4 backGroundColor = new(0.2f, 0.3f, 0.3f, 1.0f);
        Stopwatch _timer;
        bool _firstMove = true;
        float _x = 0.0f;
        float _y = 0.0f;
        float _z = 0.0f;

        public Game(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings) : base(gameWindowSettings, nativeWindowSettings)
        {
            _timer = new Stopwatch();
            WindowState = WindowState.Maximized;
            CursorState = CursorState.Grabbed;
            _camera = new FlyCamera(Vector3.UnitZ * 3, Size.X / (float)Size.Y);
        }

        protected override void OnLoad()
        {
            base.OnLoad();
            _timer.Start();
            _escenario = new Escenario();

            Initialize3DObjects();
        }
        private void Initialize3DObjects()
        {
            // Configuración de objetos usando la fábrica
            var u = EntityFactory.CreateFromShapeData(
                "FormaU",
                Resources.Config.Cylinder,
                Resources.Images.Wood,
                _camera
            );

            var cube = EntityFactory.CreateFromShapeData(
                "Cubo",
                Resources.Config.Cube,
                Resources.Images.Bricks,
                _camera
            );

            var pyramid = EntityFactory.CreateFromShapeData(
                "Piramide",
                Resources.Config.Pyramid,
                Resources.Images.Wall,
                _camera
            );

            var sphere = EntityFactory.CreateFromShapeData(
                "Esfera",
                Resources.Config.Sphere,
                Resources.Images.BlueMetal,
                _camera
            );

            var axis = new Axis(_camera);
            var crossHair = new CrossHair(_camera, Size.X, Size.Y);

            _escenario.Ejes = axis;
            _escenario.CrossHair = crossHair;
            // Agregado al escenario
            _escenario.AddObjeto(u);
            _escenario.AddObjeto(cube);
            _escenario.AddObjeto(pyramid);
            _escenario.AddObjeto(sphere);
        }


        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            // limpiar los buffers
            GL.ClearColor(backGroundColor);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            var u = _escenario.GetObjeto("FormaU");
            var cubo = _escenario.GetObjeto("Cubo");
            var piramide = _escenario.GetObjeto("Piramide");
            var sphere = _escenario.GetObjeto("Esfera");
            var axis = _escenario.Ejes;
            var crossHair = _escenario.CrossHair;

            if (
                u == null ||
                cubo == null ||
                piramide == null ||
                sphere == null ||
                axis == null ||
                crossHair == null
            ) return;

            u.Position = new Vector3(_x, _y, _z);
            u.Rotation = new Vector3((float)_timer.Elapsed.TotalSeconds * 100, 0.0f, 0.0f);
            u.Draw();

            cubo.Position = new Vector3(-1.0f, 0.0f, 0.0f);
            cubo.Rotation = new Vector3(0.0f, (float)_timer.Elapsed.TotalSeconds * 100, 0.0f);
            cubo.Draw();

            piramide.Position = new Vector3(1.0f, 0.0f, 0.0f);
            piramide.Rotation = new Vector3(0.0f, 0.0f, (float)_timer.Elapsed.TotalSeconds * 100);
            piramide.Draw();

            sphere.Position = new Vector3(0.0f, 0.0f, -1.0f);
            sphere.Rotation = new Vector3(0.0f, 0.0f, (float)_timer.Elapsed.TotalSeconds * 100);
            sphere.Draw();

            axis.Draw();
            crossHair.Draw();
            base.SwapBuffers();
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);

            if (!IsFocused) return;

            var input = KeyboardState;

            // Cerrar la ventana al presionar Escape
            if (KeyboardState.IsKeyDown(Keys.Escape))
            {
                Close();
            }
            else if (KeyboardState.IsKeyPressed(Keys.D1))
            {
                GL.PolygonMode(TriangleFace.FrontAndBack, PolygonMode.Fill);
            }
            else if (KeyboardState.IsKeyPressed(Keys.D2))
            {
                GL.PointSize(20.0f);
                GL.PolygonMode(TriangleFace.FrontAndBack, PolygonMode.Point);
            }
            else if (KeyboardState.IsKeyPressed(Keys.D3))
            {
                GL.LineWidth(10.0f);
                GL.PolygonMode(TriangleFace.FrontAndBack, PolygonMode.Line);
            }
            else if (KeyboardState.IsKeyPressed(Keys.Left))
            {
                _x -= 1.0f;
            }
            else if (KeyboardState.IsKeyPressed(Keys.Right))
            {
                _x += 1.0f;
            }
            else if (KeyboardState.IsKeyPressed(Keys.Down))
            {
                _y -= 1.0f;
            }
            else if (KeyboardState.IsKeyPressed(Keys.Up))
            {
                _y += 1.0f;
            }
            else if (KeyboardState.IsKeyPressed(Keys.H))
            {
                _z -= 1.0f;
            }
            else if (KeyboardState.IsKeyPressed(Keys.Y))
            {
                _z += 1.0f;
            }

            const float cameraSpeed = 1.5f;
            const float sensitivity = 0.2f;
            if (input.IsKeyDown(Keys.W))
            {
                _camera.Position += _camera.Front * cameraSpeed * (float)e.Time;
            }
            if (input.IsKeyDown(Keys.S))
            {
                _camera.Position -= _camera.Front * cameraSpeed * (float)e.Time;
            }
            if (input.IsKeyDown(Keys.A))
            {
                _camera.Position -= _camera.Right * cameraSpeed * (float)e.Time;
            }
            if (input.IsKeyDown(Keys.D))
            {
                _camera.Position += _camera.Right * cameraSpeed * (float)e.Time;
            }
            if (input.IsKeyDown(Keys.Space))
            {
                _camera.Position += _camera.Up * cameraSpeed * (float)e.Time;
            }
            if (input.IsKeyDown(Keys.LeftShift))
            {
                _camera.Position -= _camera.Up * cameraSpeed * (float)e.Time;
            }
            if (input.IsKeyDown(Keys.Space))
            {
                _camera.Position += _camera.Up * cameraSpeed * (float)e.Time;
            }
            if (input.IsKeyDown(Keys.LeftShift))
            {
                _camera.Position -= _camera.Up * cameraSpeed * (float)e.Time;
            }

            var mouse = MouseState;

            if (_firstMove)
            {
                _lastPosition = new Vector2(mouse.X, mouse.Y);
                _firstMove = false;
            }

            else
            {
                var deltaX = mouse.X - _lastPosition.X;
                var deltaY = mouse.Y - _lastPosition.Y;
                _lastPosition = new Vector2(mouse.X, mouse.Y);

                _camera.Yaw += deltaX * sensitivity;
                _camera.Pitch -= deltaY * sensitivity;
            }
        }

        protected override void OnResize(ResizeEventArgs e)
        {
            base.OnResize(e);
            GL.Viewport(0, 0, Size.X, Size.Y);
        }

        protected override void OnUnload()
        {
            base.OnUnload();
            _escenario.Dispose();
            _timer.Stop();
        }
    }
}
