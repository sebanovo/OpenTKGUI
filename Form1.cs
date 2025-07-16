using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using System.Diagnostics;
using System.Text.Json;
using OpenTKGUI.Utils;
using OpenTKGUI.Estructura;
using OpenTKGUI.Animation;


namespace OpenTKGUI
{
    public partial class Form1 : Form
    {
        Escenario _escenario;
        Libreto _libreto;
        ArcRotateCamera _camera;
        Luz _luz = new Luz(new Vector3(0, 100, 0), new Vector3(1, 1, 1));
        System.Windows.Forms.Timer _timer;
        Stopwatch _sw;
        Color4 backGroundColor = new(0.2f, 0.3f, 0.3f, 1.0f);

        public Form1()
        {
            InitializeComponent();
            glControl1.MouseWheel += glControl1_MouseWheel;
            _timer = new();
            _timer.Interval = 16; // 60 FPS
            _timer.Tick += (s, e) => glControl1.Invalidate();

            _sw = new();

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
        public void InicializarFormas()
        {
            _escenario = new Escenario(_camera);
            Objeto f1 = JSONLoader.CargarObjeto("./Modelos/JSON/Formula1.json", _camera, _luz);
            Objeto f2 = OBJLoader.CargarObjeto("./Modelos/OBJ/grass.obj", _camera, _luz);
            _escenario.Add(f1);
            _escenario.Add(f2);
        }

        public void InicializarLibreto()
        {
            // var auto = _escenario.Get("Auto");
            // Accion animacion = new Accion();

            // // llantas rotacion
            // // parte 1
            // animacion.Add(new Rotar(auto.Get("Auto.RuedaDelanteraIzquierda"),
            //     new Vector3(0.0f, 135, 0.0f),
            //     1f,
            //     8, 10));
            // animacion.Add(new Rotar(auto.Get("Auto.ParteRuedaDelanteraIzquierda"),
            //     new Vector3(0.0f, 135, 0.0f),
            //     1f,
            //     8, 10));
            // animacion.Add(new Rotar(auto.Get("Auto.RuedaDelanteraIzquierda"),
            //     new Vector3(0.0f, 90, 0.0f),
            //     1f,
            //     10, 12));
            // animacion.Add(new Rotar(auto.Get("Auto.ParteRuedaDelanteraIzquierda"),
            //     new Vector3(0.0f, 90, 0.0f),
            //     1f,
            //     10, 12));
            // animacion.Add(new Rotar(auto.Get("Auto.RuedaDelanteraDerecha"),
            //     new Vector3(0.0f, 135, 0.0f),
            //     1f,
            //     8, 10));
            // animacion.Add(new Rotar(auto.Get("Auto.ParteRuedaDelanteraDerecha"),
            //     new Vector3(0.0f, 135, 0.0f),
            //     1f,
            //     8, 10));
            // animacion.Add(new Rotar(auto.Get("Auto.RuedaDelanteraDerecha"),
            //     new Vector3(0.0f, 90, 0.0f),
            //     1f,
            //     10, 12));
            // animacion.Add(new Rotar(auto.Get("Auto.ParteRuedaDelanteraDerecha"),
            //     new Vector3(0.0f, 90, 0.0f),
            //     1f,
            //     10, 12));

            // // parte 2
            // animacion.Add(new Rotar(auto.Get("Auto.RuedaDelanteraIzquierda"),
            //     new Vector3(0.0f, 135, 0.0f),
            //     1f,
            //     30, 32));
            // animacion.Add(new Rotar(auto.Get("Auto.ParteRuedaDelanteraIzquierda"),
            //     new Vector3(0.0f, 135, 0.0f),
            //     1f,
            //     30, 32));
            // animacion.Add(new Rotar(auto.Get("Auto.RuedaDelanteraIzquierda"),
            //     new Vector3(0.0f, 90, 0.0f),
            //     1f,
            //     32, 34));
            // animacion.Add(new Rotar(auto.Get("Auto.ParteRuedaDelanteraIzquierda"),
            //     new Vector3(0.0f, 90, 0.0f),
            //     1f,
            //     32, 34));
            // animacion.Add(new Rotar(auto.Get("Auto.RuedaDelanteraDerecha"),
            //     new Vector3(0.0f, 135, 0.0f),
            //     1f,
            //     30, 32));
            // animacion.Add(new Rotar(auto.Get("Auto.ParteRuedaDelanteraDerecha"),
            //     new Vector3(0.0f, 135, 0.0f),
            //     1f,
            //     30, 32));
            // animacion.Add(new Rotar(auto.Get("Auto.RuedaDelanteraDerecha"),
            //     new Vector3(0.0f, 90, 0.0f),
            //     1f,
            //     32, 34));
            // animacion.Add(new Rotar(auto.Get("Auto.ParteRuedaDelanteraDerecha"),
            //     new Vector3(0.0f, 90, 0.0f),
            //     1f,
            //     32, 34));

            // // auto rotacion
            // animacion.Add(new Rotar(auto, new Vector3(0, 90, 0), 0.55f, 8, 12));
            // animacion.Add(new Rotar(auto, new Vector3(0, 270, 0), 0.55f, 30, 34));

            // // auto traslacion
            // animacion.Add(new Trasladar(auto, new Vector3(5.0f, 0.5f, -2.0f), 0.01f, 1, 8));
            // animacion.Add(new Trasladar(auto, new Vector3(3.0f, 0.5f, -4.0f), 0.01f, 8, 12));
            // animacion.Add(new Trasladar(auto, new Vector3(-3.0f, 0.5f, -4.0f), 0.01f, 12, 30));
            // animacion.Add(new Trasladar(auto, new Vector3(-5.0f, 0.5f, 2.0f), 0.01f, 30, 34));
            // animacion.Add(new Trasladar(auto, new Vector3(-5.0f, 0.5f, 0.0f), 0.01f, 34, 42));

            // _libreto = new Libreto(new Escena(animacion));
        }


        private void glControl1_Load(object sender, EventArgs e)
        {
            _timer.Start();
            InicializarFormas();
            InicializarLibreto();
        }

        private void glControl1_Paint(object? sender, PaintEventArgs e)
        {
            glControl1.MakeCurrent();
            // limpiar los buffers
            GL.ClearColor(backGroundColor);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            //long tiempo = (long)_sw.Elapsed.TotalSeconds;
            //labelTiempo.Text = "Tiempo: " + tiempo.ToString();
            //_libreto.Play(tiempo);
            //if (_sw.Elapsed.TotalSeconds > 42)
            //{
            //    _sw.Reset();

            _escenario.Draw();
            _escenario.DrawEjes();
            _escenario.DrawTerreno();
            glControl1.SwapBuffers();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                Close();
            }
            if (keyData == Keys.D1) // Fill
            {
                GL.PolygonMode(TriangleFace.FrontAndBack, PolygonMode.Fill);
            }
            if (keyData == Keys.D2) // Point
            {
                GL.PointSize(10);
                GL.PolygonMode(TriangleFace.FrontAndBack, PolygonMode.Point);
            }
            if (keyData == Keys.D3) // Line
            {
                GL.PolygonMode(TriangleFace.FrontAndBack, PolygonMode.Line);
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

        private void abrirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "JSON files (*.json)|*.json";
            openFileDialog1.Title = "Abrir model en JSON";
            openFileDialog1.Multiselect = true;

            if (openFileDialog1.ShowDialog() != DialogResult.OK) return;
            try
            {
                foreach (var fileName in openFileDialog1.FileNames)
                {
                    Objeto newObject = JSONLoader.CargarObjeto(fileName, _camera, _luz);
                    _escenario.Add(newObject);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al abrir el archivo: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void guardarComoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Guardar el Archivo Json
            saveFileDialog1.Filter = "JSON files (*.json)|*.json";
            saveFileDialog1.Title = "Guardar modelo en JSON";
            saveFileDialog1.DefaultExt = "json";
            saveFileDialog1.AddExtension = true;
            if (_escenario.Objetos.Count == 0)
            {
                MessageBox.Show("No hay objetos para guardar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            foreach (var objeto in _escenario.Objetos.Values)
            {
                if (saveFileDialog1.ShowDialog() != DialogResult.OK) return;
                string filePath = saveFileDialog1.FileName;
                ModeloObjeto combinedData = JSONLoader.GenerarModelo((Objeto)objeto, filePath);

                var options = new JsonSerializerOptions { WriteIndented = true };
                File.WriteAllText(filePath, JsonSerializer.Serialize(combinedData, options));
            }
        }

        private void PlayAnimationButton_Click(object sender, EventArgs e)
        {
            _sw.Start();
        }

        private void buttonResetAnimation_Click(object sender, EventArgs e)
        {
            var auto1 = _escenario.Get("Auto");
            Objeto auto2 = JSONLoader.CargarObjeto("./Objetos/Auto.json", _camera, _luz);
            auto1.Transformation.Position = auto2.Transformation.Position;
            auto1.Transformation.Scale = auto2.Transformation.Scale;
            auto1.Transformation.Rotation = auto2.Transformation.Rotation;
            auto1.Transformation.Position = new Vector3(5.0f, 0.5f, 0.0f);
            _sw.Reset();
        }
    }
}
