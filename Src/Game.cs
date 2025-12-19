using System.Diagnostics;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTK.Graphics.OpenGL4;

using U.Src.Models._2D;
using U.Src.Models._3D;
using U.Src.Utils;

#pragma warning disable CS8618

namespace U.src
{
    public class Game(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings)
                : GameWindow(gameWindowSettings, nativeWindowSettings)
    {
        // Shapes
        U3D _u;
        CrossHair _crossHair;
        Axis _axis;
        // Camera
        Camera _camera;
        Vector2 _lastPosition;
        Color4 backGroundColor = new(0.2f, 0.3f, 0.3f, 1.0f);
        Stopwatch _timer;
        bool _firstMove = true;

        protected override void OnLoad()
        {
            base.OnLoad();
            _timer = new Stopwatch();
            _timer.Start();

            WindowState = WindowState.Maximized;
            _camera = new Camera(Vector3.UnitZ * 3, Size.X / (float)Size.Y);
            CursorState = CursorState.Grabbed;

            // Iniciatialize  Shapes
            _u = new();
            _crossHair = new();
            _axis = new();

            // Load Shapes
            _u.Load();
            _axis.Load();
            _crossHair.Load();
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            // limpiar los buffers
            GL.ClearColor(backGroundColor);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            // Draw U

            _u.Bind();
            _u.ShaderProgram.SetInt("texture1", 0)
                            .SetMat4("transform", Matrix4.Identity)
                            .SetMat4("model", Matrix4.Identity)
                            .SetMat4("view", _camera.GetViewMatrix())
                            .SetMat4("projection", _camera.GetProjectionMatrix());
            _u.Draw();

            // Draw Axis (xyz)
            _axis.Bind();
            _axis.ShaderProgram.SetMat4("model", Matrix4.Identity)
                               .SetMat4("view", _camera.GetViewMatrix())
                               .SetMat4("projection", _camera.GetProjectionMatrix());
            _axis.Draw();

            // Draw Cross Hair 
            _crossHair.Bind();
            _crossHair.ScaleX = 1.0f / (Size.X / (float)Size.Y);
            _crossHair.ShaderProgram.SetVec3("u_Color", new Vector3(1.0f, 1.0f, 1.0f));
            _crossHair.Draw();

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

            _u.Dispose();
            _axis.Dispose();
            _crossHair.Dispose();

            _timer.Stop();
        }
    }
}
