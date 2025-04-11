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
using System.Text.Json;
using System.Windows.Forms;
using static OpenTKGUI.Properties.Resources.Config;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Security.Cryptography;


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

            var Esfera = EntityFactory.CreateFromShapeData(
                "Esfera",
                Resources.Config.Sphere,
                Resources.Images.BlueMetal,
                _camera
            );

            var Cilindro = EntityFactory.CreateFromShapeData(
                "Cilindro",
                Resources.Config.Cylinder,
                Resources.Images.Wood,
                _camera
            );

            _escenario = new()
            {
                Ejes = Ejes
            };
            //_escenario.AddObjeto(U);
            //_escenario.AddObjeto(Cubo);
            //_escenario.AddObjeto(Piramide);
            //_escenario.AddObjeto(Esfera);
            //_escenario.AddObjeto(Cilindro);
        }

        private void glControl1_Paint(object? sender, PaintEventArgs e)
        {
            glControl1.MakeCurrent();
            // limpiar los buffers
            GL.ClearColor(backGroundColor);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            //var u = _escenario.GetObjeto("U");
            //var cubo = _escenario.GetObjeto("Cubo");
            //var piramide = _escenario.GetObjeto("Piramide");
            //var esfera = _escenario.GetObjeto("Esfera");
            //var cilindro = _escenario.GetObjeto("Cilindro");

            //if (
            //    u == null ||
            //    cubo == null ||
            //    piramide == null ||
            //    axis == null ||
            //    esfera == null ||
            //    cilindro == null
            //) return;

            //u.Position = new Vector3(_x, _y, _z);
            //u.Rotation = new Vector3((float)_sw.Elapsed.TotalSeconds * 100, 0.0f, 0.0f);
            //u.Draw();

            //cubo.Position = new Vector3(-1.0f, 0.0f, 0.0f);
            //cubo.Rotation = new Vector3(0.0f, (float)_sw.Elapsed.TotalSeconds * 100, 0.0f);
            //cubo.Draw();

            //piramide.Position = new Vector3(1.0f, 0.0f, 0.0f);
            //piramide.Rotation = new Vector3(0.0f, 0.0f, (float)_sw.Elapsed.TotalSeconds * 100);
            //piramide.Draw();

            //esfera.Position = new Vector3(_x, _y, _z);
            //esfera.Draw();

            //cilindro.Position = new Vector3(_x, _y, _z);
            //cilindro.Rotation = new Vector3(100.0f, 0.0f, 0.0f);
            //cilindro.Draw();

            var axis = _escenario.Ejes;
            axis.Draw();
            _escenario.Render();
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

        public class ModelData
        {
            public string Nombre { get; set; }
            public Position Position { get; set; }
            public string Texture { get; set; }
            public List<Vertice> Vertices { get; set; }
            public List<uint> Indices { get; set; }
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
                    var modelData = JsonSerializer.Deserialize<ModelData>(jsonContent);
                    if (modelData == null) return; 

                    var objeto = new Objeto3D(modelData.Nombre, _camera);
                    objeto.Position = new Vector3
                    {
                        X = modelData.Position.X,
                        Y = modelData.Position.Y,
                        Z = modelData.Position.Z
                    };

                    var shader = new Shader(Resources.Shaders.Objeto3DVert, Resources.Shaders.Objeto3DFrag);
                    var texture = new Texture(modelData.Texture);
                    var parte = new Parte(shader, texture);
                    var cara = new Cara(modelData.Vertices, [..modelData.Indices]);
                    cara.Centrar();

                    parte.AddCara(cara);
                    objeto.Partes.Add(parte);
                    objeto.Load();

                    // Añadir al escenario
                    _escenario.AddObjeto(objeto);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al abrir el archivo: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public class Position 
        {
            public float X { get; set; }
            public float Y { get; set; }
            public float Z { get; set; }
        }

        private void guardarComoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Guardar el Archivo Json
            saveFileDialog1.Filter = "JSON files (*.json)|*.json";
            saveFileDialog1.Title = "Guardar modelo en JSON";
            saveFileDialog1.DefaultExt = "json";
            saveFileDialog1.AddExtension = true;
            if(_escenario.Objetos.Count == 0)
            {
                MessageBox.Show("No hay objetos para guardar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            foreach (var objeto in _escenario.Objetos)
            {
                if(saveFileDialog1.ShowDialog() != DialogResult.OK) return;
                string filePath = saveFileDialog1.FileName;
                var FileName = Path.GetFileNameWithoutExtension(filePath);
                List<Vertice> vertices = [];
                List<uint> indices = [];
                foreach (var parte in objeto.Partes)
                {
                    foreach (var cara in parte.Caras)
                    {
                        foreach(var vertice in cara.Vertices)
                        {
                            vertices.Add(new Vertice()
                            {
                                X = vertice.X,
                                Y = vertice.Y,
                                Z = vertice.Z,
                                U = vertice.U,
                                V = vertice.V
                            });
                        }
                    }
                    indices.AddRange(parte.Caras.SelectMany(c => c.Indices));
                }

                var combinedData = new ModelData
                {
                    Nombre = FileName,
                    Position = new Position 
                    {
                        X = objeto.Position.X,
                        Y = objeto.Position.Y,
                        Z = objeto.Position.Z
                    },
                    Texture = objeto.Partes[0].Texture.Name,
                    Vertices = vertices,
                    Indices = indices
                };

                var options = new JsonSerializerOptions { WriteIndented = true };
                File.WriteAllText(filePath, JsonSerializer.Serialize(combinedData, options));
            }
        }
    }
}
