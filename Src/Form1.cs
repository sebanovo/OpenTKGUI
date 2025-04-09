using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using OpenTKGUI.Src.Models._2D;
using OpenTKGUI.Src.Models._3D;
using OpenTKGUI.Src.Utils;
using OpenTKGUI.Properties;
using System.Diagnostics;
using OpenTK.Windowing.Common;
using OpenTKGUI.Src.Models._3D.Composite;
using OpenTKGUI.Src.Models._3D.Factories;


namespace OpenTKGUI
{
    public partial class Form1 : Form
    {
        Escenario _escenario;
        Axis _axis;
        ArcRotateCamera _camera;
        System.Windows.Forms.Timer _timer;
        Stopwatch _sw;

        Color4 backGroundColor = new(0.2f, 0.3f, 0.3f, 1.0f);
        float _x, _y, _z;
        public Form1()
        {
            InitializeComponent();
            glControl1.MouseWheel += glControl1_MouseWheel;
            _timer = new();
            _timer.Interval = 16; // 60 FPS
            _timer.Tick += (s, e) => glControl1.Invalidate();

            _sw = new();

            //_camera = new Camera(Vector3.UnitZ * 3, glControl1.ClientSize.Width / (float)glControl1.ClientSize.Height);
            //_camera = new TrallBackCamera(Vector3.UnitZ * 3, glControl1.ClientSize.Width / (float)glControl1.ClientSize.Height);
            _camera = new ArcRotateCamera(Vector3.Zero, 2.0f, glControl1.ClientSize.Width / (float)glControl1.ClientSize.Height);
        }


        private void glControl1_Resize(object sender, EventArgs e)
        {
            if (glControl1.ClientSize.Width > 0 && glControl1.ClientSize.Height > 0)
            {
                GL.Viewport(0, 0, glControl1.ClientSize.Width, glControl1.ClientSize.Height);
                _camera.AspectRatio = glControl1.ClientSize.Width / (float)glControl1.ClientSize.Height;
            }
        }

        private void glControl1_Load(object sender, EventArgs e)
        {
            _timer.Start();
            _sw.Start();

            // Iniciatialize  Shapes
            var U = EntityFactory.CreateFromShapeData(
                "U",
                Resources.Config.U,
                Resources.Images.Wood,
                _camera
            );

            var Cubo = EntityFactory.CreateFromShapeData(
               "Cubo",
               Resources.Config.Cube,
               Resources.Images.Bricks,
               _camera
           );

            var Piramide = EntityFactory.CreateFromShapeData(
               "Piramide",
               Resources.Config.Pyramid,
               Resources.Images.Wall,
               _camera
           );
            var Ejes = new Axis(_camera);

            _escenario = new Escenario();
            _escenario.Ejes = Ejes;
            _escenario.AddObjeto(U);
            _escenario.AddObjeto(Cubo);
            _escenario.AddObjeto(Piramide);
        }

        private void glControl1_Paint(object? sender, PaintEventArgs e)
        {
            glControl1.MakeCurrent();
            // limpiar los buffers
            GL.ClearColor(backGroundColor);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            var u = _escenario.GetObjeto("U");
            var cubo = _escenario.GetObjeto("Cubo");
            var piramide = _escenario.GetObjeto("Piramide");
            var axis = _escenario.Ejes;

            if (
                u == null ||
                cubo == null ||
                piramide == null ||
                axis == null 
            ) return;

            u.Position = new Vector3(_x, _y, _z);
            u.Rotation = new Vector3((float)_sw.Elapsed.TotalSeconds * 100, 0.0f, 0.0f);
            u.Draw();

            cubo.Position = new Vector3(-1.0f, 0.0f, 0.0f);
            cubo.Rotation = new Vector3(0.0f, (float)_sw.Elapsed.TotalSeconds * 100, 0.0f);
            cubo.Draw();

            piramide.Position = new Vector3(1.0f, 0.0f, 0.0f);
            piramide.Rotation = new Vector3(0.0f, 0.0f, (float)_sw.Elapsed.TotalSeconds * 100);
            piramide.Draw();

            axis.Draw();

            glControl1.SwapBuffers();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                Close();
            }
            if (keyData == Keys.D1)
            {
                Console.WriteLine("Fill");
                GL.PolygonMode(TriangleFace.FrontAndBack, PolygonMode.Fill);
            }
            if (keyData == Keys.D2)
            {
                Console.WriteLine("Point");
                GL.PointSize(10);
                GL.PolygonMode(TriangleFace.FrontAndBack, PolygonMode.Point);
            }
            if (keyData == Keys.D3)
            {
                Console.WriteLine("Line");
                GL.PolygonMode(TriangleFace.FrontAndBack, PolygonMode.Line);
            }
            if (keyData == Keys.Up)
            {
                _y += 1.0f;
            }
            if (keyData == Keys.Down)
            {
                _y -= 1.0f;
            }
            if (keyData == Keys.Left)
            {
                _x -= 1.0f;
            }
            if (keyData == Keys.Right)
            {
                _x += 1.0f;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);

            _escenario.Dispose();
            _timer.Stop();
            _sw.Stop();
        }
        protected void glControl1_MouseWheel(object sender, MouseEventArgs e)
        {
            _camera.ProcessMouseWheel(e.Delta);
            base.OnMouseWheel(e);
        }

        private void glControl1_MouseDown(object sender, MouseEventArgs e)
        {
            _camera.ProcessMouseDown(new Vector2(e.X, e.Y), e.Button);
            base.OnMouseDown(e);
        }

        private void glControl1_MouseUp(object sender, MouseEventArgs e)
        {
            _camera.ProcessMouseUp();
            base.OnMouseUp(e);
        }

        private void glControl1_MouseMove(object sender, MouseEventArgs e)
        {
            _camera.ProcessMouseMove(new Vector2(e.X, e.Y));
            base.OnMouseMove(e);
        }

    }
}
