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
using System.Xml.Linq;
using System.Text.Json.Serialization;
using OpenTK.Platform.Windows;


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

        public void InicializarFormas()
        {
            // formas inicializasas
            Parte parteEsfera = new Parte(
                "parteEsfera",
                Resources.Config.Sphere.Vertices,
                Resources.Config.Sphere.Indices,
                Resources.Images.BlueMetal,
                _camera
             );

            Objeto esfera = new Objeto(
                "Esfera",
                parteEsfera
            );

            Parte parteU1 = new(
                "u1",
                Resources.Config.Cube.Vertices,
                Resources.Config.Cube.Indices,
                Resources.Images.Wood,
                _camera
             );

            parteU1.Transformation.Position = new Vector3(-0.4f, 0.0f, 0.0f);
            parteU1.Transformation.Scale = new Vector3(1.0f, 3.0f, 1.0f);

            Parte parteU2 = new(
                "u2",
                Resources.Config.Cube.Vertices,
                Resources.Config.Cube.Indices,
                Resources.Images.Wood,
                _camera
             );


            parteU2.Transformation.Position = new Vector3(0.4f, 0.0f, 0.0f);
            parteU2.Transformation.Scale = new Vector3(1.0f, 3.0f, 1.0f);


            Parte parteCubo = new(
                "cubo",
                Resources.Config.Cube.Vertices,
                Resources.Config.Cube.Indices,
                Resources.Images.Wood,
                _camera
            );

            parteCubo.Transformation.Position = new Vector3(0.0f, -0.4f, 0.0f);

            Objeto u = new();
            u.Name = "U";
            u.Add(parteU1);
            u.Add(parteU2);
            u.Add(parteCubo);

            Parte parteCilindro = new Parte(
                "parteCilindro",
                Resources.Config.Cylinder.Vertices,
                Resources.Config.Cylinder.Indices,
                Resources.Images.Wood,
                _camera
            );
            parteCilindro.Transformation.Position = new Vector3(0.0f, -0.5f, 0.0f);

            Objeto cilindro = new Objeto(
                "Cilindro",
                parteCilindro
            );

            Parte partePiramide = new Parte(
                "partePiramide",
                Resources.Config.Pyramid.Vertices,
                Resources.Config.Pyramid.Indices,
                Resources.Images.Wall,
                _camera
            );

            partePiramide.Transformation.Position = new Vector3(0.0f, 0.0f, 0.0f);

            Objeto piramide = new Objeto(
                "Piramide",
                partePiramide
             );


            _escenario = new Escenario(_camera);

            u.Transformation.Position = new Vector3(-1.5f, 1.0f, 0.0f);
            piramide.Transformation.Position = new Vector3(1.0f, 1.0f, 0.0f);

            _escenario.Add(u);
            _escenario.Add(piramide);
            //_escenario.Add(cubo);
            //_escenario.Add(esfera);
            //_escenario.Add(cilindro);
        }

        private void glControl1_Load(object sender, EventArgs e)
        {
            _timer.Start();
            _sw.Start();
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
            Vector3 rotation = new Vector3(
                0.0f, 
                (float)totalSeconds * 100,
                0.0f
            );
            Vector3 translation = new Vector3(
                Convert.ToSingle(Math.Cos(totalSeconds)),
                0.0f,
                0.0f
            );
            Vector3 scalation = new Vector3(Convert.ToSingle(Math.Cos(totalSeconds)) / 100);

            this.Text = scalation.X.ToString();
            //piramide.Draw();
            //u.Escalar(scalation);

            //u.GetParte("u1").Trasladar(translation);
            //u.GetParte("u2").Trasladar(translation);
            //piramide.GetParte("partePiramide").Trasladar(scalation);

            //_escenario.Escalar(scalation);
            //_escenario.Trasladar(translation);
            _escenario.Rotar(rotation);
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

        public class ModelDataObj
        {
            public string Name { get; set; }
            public Transformation1 Transformation { get; set; }
            public List<ModelParte> Partes { get; set; }
        }

        public class ModelParte
        {
            public string Name { get; set; }
            public string Texture { get; set; }
            public Transformation1 Transformation { get; set; }
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
                    var modelData = JsonSerializer.Deserialize<ModelDataObj>(jsonContent);
                    if (modelData == null) return;
                    Objeto newObject = new ();
                    newObject.Name = modelData.Name;
                    newObject.Transformation.Scale = new Vector3(
                        modelData.Transformation.Scale.X,
                        modelData.Transformation.Scale.Y,
                        modelData.Transformation.Scale.Z
                    );
                    newObject.Transformation.Position = new Vector3(
                        modelData.Transformation.Position.X,
                        modelData.Transformation.Position.Y,
                        modelData.Transformation.Position.Z
                    );
                    newObject.Transformation.Rotation = new Vector3(
                        modelData.Transformation.Rotation.X,
                        modelData.Transformation.Rotation.Y,
                        modelData.Transformation.Rotation.Z
                    );

                    foreach (var parte in modelData.Partes)
                    {
                        List<float> listaVertices = [];
                        foreach (var vertice in parte.Vertices)
                        {
                            listaVertices.Add(vertice.X);
                            listaVertices.Add(vertice.Y);
                            listaVertices.Add(vertice.Z);
                            listaVertices.Add(vertice.U);
                            listaVertices.Add(vertice.V);
                        }
                        Parte newParte = new Parte(parte.Name, listaVertices, parte.Indices, parte.Texture, _camera);

                        newParte.Transformation.Position = new Vector3(
                                parte.Transformation.Position.X,
                                parte.Transformation.Position.Y,
                                parte.Transformation.Position.Z
                        );

                        newParte.Transformation.Rotation = new Vector3(
                                parte.Transformation.Rotation.X,
                                parte.Transformation.Rotation.Y,
                                parte.Transformation.Rotation.Z
                        );

                        newParte.Transformation.Scale = new Vector3(
                                parte.Transformation.Scale.X,
                                parte.Transformation.Scale.Y,
                                parte.Transformation.Scale.Z
                        );
                        newObject.Add(newParte);
                    }
                    _escenario.Add(newObject);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al abrir el archivo: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public class Transformation1
        {
            public Punto Position { get; set; }
            public Punto Rotation { get; set; }
            public Punto Scale { get; set; }
        }

        public class Punto 
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
            if (_escenario.Objetos.Count == 0)
            {
                MessageBox.Show("No hay objetos para guardar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            foreach (var objeto in _escenario.Objetos)
            {
                if (saveFileDialog1.ShowDialog() != DialogResult.OK) return;
                string filePath = saveFileDialog1.FileName;
                var FileName = Path.GetFileNameWithoutExtension(filePath);
           
                List<ModelParte> listModelParte = [];
                foreach (var parte in objeto.Partes)
                {
                    List<Vertice> listaVertices = [];
                    for(int i = 0; i < parte.Vertices.Count; i += 5)
                    {
                        if (i + 4 >= parte.Vertices.Count) break;
                        listaVertices.Add(new Vertice()
                        {
                            X = parte.Vertices[i],
                            Y = parte.Vertices[i + 1],
                            Z = parte.Vertices[i + 2],
                            U = parte.Vertices[i + 3],
                            V = parte.Vertices[i + 4]
                        });
                    }

                    listModelParte.Add(
                        new ModelParte
                        {
                            Name = parte.Name,
                            Texture = parte.Texture,
                            Transformation = new Transformation1()
                            {
                                Position = new Punto()
                                {
                                    X = parte.Transformation.Position.X,
                                    Y = parte.Transformation.Position.Y,
                                    Z = parte.Transformation.Position.Z
                                },
                                Rotation = new Punto()
                                {
                                    X = parte.Transformation.Rotation.X,
                                    Y = parte.Transformation.Rotation.Y,
                                    Z = parte.Transformation.Rotation.Z
                                },
                                Scale = new Punto()
                                {
                                    X = parte.Transformation.Scale.X,
                                    Y = parte.Transformation.Scale.Y,
                                    Z = parte.Transformation.Scale.Z
                                }
                            },
                            Vertices = listaVertices,
                            Indices = parte.Indices
                        }
                     );
                }

                var combinedData = new ModelDataObj
                {
                    Name = FileName,
                    Transformation = new Transformation1
                    {
                        Position = new Punto()
                        {
                            X = objeto.Transformation.Position.X,
                            Y = objeto.Transformation.Position.Y,
                            Z = objeto.Transformation.Position.Z
                        },
                        Rotation = new Punto()
                        {
                            X = objeto.Transformation.Rotation.X,
                            Y = objeto.Transformation.Rotation.Y,
                            Z = objeto.Transformation.Rotation.Z
                        },
                        Scale = new Punto()
                        {
                            X = objeto.Transformation.Scale.X,
                            Y = objeto.Transformation.Scale.Y,
                            Z = objeto.Transformation.Scale.Z
                        }

                    },
                    Partes = listModelParte
                };

                var options = new JsonSerializerOptions { WriteIndented = true };
                File.WriteAllText(filePath, JsonSerializer.Serialize(combinedData, options));
            }
        }
    }
}
