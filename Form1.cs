using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using System.Diagnostics;
using System.Text.Json;
using OpenTK.Platform.Windows;
using OpenTKGUI.Utils;
using OpenTKGUI.Estructura;


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
            Objeto u = Modelo.CargarObjeto("./Objetos/U.json", _camera);
            Objeto piramide = Modelo.CargarObjeto("./Objetos/Piramide.json", _camera);
            u.Transformation.Position = new Vector3(-1.5f, 1.0f, 0.0f);

            u.Transformation.Position = new Vector3(-1.5f, 1.0f, 0.0f);
            piramide.Transformation.Position = new Vector3(1.0f, 1.0f, 0.0f);

            _escenario.Add(u);
            _escenario.Add(piramide);
        }

        private void glControl1_Load(object sender, EventArgs e)
        {
            _timer.Start();
            _sw.Start();
            _escenario = new Escenario(_camera);
            InicializarFormas();
        }

        private void glControl1_Paint(object? sender, PaintEventArgs e)
        {
            glControl1.MakeCurrent();
            // limpiar los buffers
            GL.ClearColor(backGroundColor);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            var u = _escenario.GetObjeto("U");
            var piramide = _escenario.GetObjeto("Piramide");
            //var cubo = _escenario.GetObjeto("Cubo");
            //var esfera = _escenario.GetObjeto("Esfera");
            //var cilindro = _escenario.GetObjeto("Cilindro");

            double totalSeconds = _sw.Elapsed.TotalSeconds;
            Vector3 rotacion = new Vector3(
                //(float)totalSeconds * 100,
                0.0f,
                0.0f,
                1.0f
            );
            Vector3 traslacion = new Vector3(
                0.0f,
                Convert.ToSingle(Math.Sin(totalSeconds)) / 100,
                0.0f
            );
            Vector3 escalacion = new Vector3(Convert.ToSingle(Math.Cos(totalSeconds)) / 100);

            u.GetParte("u1").Rotar(rotacion);
            //u.Rotar(rotacion);

            //u.Trasladar(traslacion);
            //u.GetParte("u2").Trasladar(traslacion);
            //piramide.GetParte("partePiramide").Trasladar(escalacion);

            //_escenario.Escalar(escalacion);
            //_escenario.Trasladar(traslacion);
            //_escenario.Rotar(rotacion);
            _escenario.Rotar(rotacion);
            _escenario.Draw();
           




            _escenario.DrawEjes();
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
                    string jsonContent = File.ReadAllText(fileName);
                    Objeto newObject = Modelo.CargarObjeto(fileName, _camera);
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

            foreach (var objeto in _escenario.Objetos)
            {
                if (saveFileDialog1.ShowDialog() != DialogResult.OK) return;
                string filePath = saveFileDialog1.FileName;
                ModeloObjeto combinedData = Modelo.GenerarModelo(objeto, filePath);

                var options = new JsonSerializerOptions { WriteIndented = true };
                File.WriteAllText(filePath, JsonSerializer.Serialize(combinedData, options));
            }
        }
    }
}
