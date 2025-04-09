using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenTKGUI.Src.Utils
{
    public class ArcRotateCamera
    {
        // Parámetros de la órbita
        private float _alpha;  // Ángulo horizontal (radianes)
        private float _beta;   // Ángulo vertical (radianes)
        private float _radius; // Distancia al target

        // Punto de interés
        public Vector3 Target { get; set; }

        // Orientación
        public Vector3 Up { get; set; } = Vector3.UnitY;

        // Parámetros de proyección
        public float FieldOfView { get; set; } = MathHelper.PiOver2;
        public float AspectRatio { get; set; }
        public float NearPlane { get; set; } = 0.1f;
        public float FarPlane { get; set; } = 100f;

        // Control de cámara
        public float RotationSpeed { get; set; } = 0.005f;
        public float PanSpeed { get; set; } = 0.01f;
        public float ZoomSpeed { get; set; } = 0.001f;

        // Estado del control
        private bool _isRotating = false;
        private bool _isPanning = false;
        private Vector2 _lastMousePos;

        public ArcRotateCamera(Vector3 target, float radius, float aspectRatio)
        {
            Target = target;
            _radius = radius;
            AspectRatio = aspectRatio;

            // Posición inicial mirando desde el frente del eje z
            _alpha = MathHelper.PiOver2;
            _beta = MathHelper.PiOver2;
        }

        public Matrix4 GetViewMatrix()
        {
            return Matrix4.LookAt(CalculatePosition(), Target, Up);
        }

        public Matrix4 GetProjectionMatrix()
        {
            return Matrix4.CreatePerspectiveFieldOfView(FieldOfView, AspectRatio, NearPlane, FarPlane);
        }

        public Vector3 CalculatePosition()
        {
            return new Vector3(
               _radius * (float)Math.Cos(_alpha) * (float)Math.Sin(_beta),
               _radius * (float)Math.Cos(_beta),
               _radius * (float)Math.Sin(_alpha) * (float)Math.Sin(_beta)
           ) + Target;
        }

        public void ProcessMouseWheel(float delta)
        {
            _radius -= delta * ZoomSpeed;
            _radius = Math.Max(0.1f, Math.Min(_radius, FarPlane * 0.9f));
        }

        public void ProcessMouseDown(Vector2 mousePos, MouseButtons button)
        {
            _lastMousePos = new Vector2(-mousePos.X, mousePos.Y);
            _isRotating = (button == MouseButtons.Left);
            _isPanning = (button == MouseButtons.Middle);
        }

        public void ProcessMouseUp()
        {
            _isRotating = false;
            _isPanning = false;
        }

        public void ProcessMouseMove(Vector2 mousePos)
        {
            var Aux = new Vector2(-mousePos.X, mousePos.Y);
            var delta = Aux - _lastMousePos;
            _lastMousePos = Aux;

            if (_isRotating)
            {
                Rotate(delta);
            }
            else if (_isPanning)
            {
                Pan(delta);
            }
        }

        private void Rotate(Vector2 delta)
        {
            _alpha -= delta.X * RotationSpeed;
            _beta = MathHelper.Clamp(
                _beta - delta.Y * RotationSpeed,
                0.1f,
                MathHelper.Pi - 0.1f);
        }

        private void Pan(Vector2 delta)
        {
            Vector3 right = Vector3.Normalize(Vector3.Cross(Up, CalculatePosition() - Target));
            Vector3 actualUp = Vector3.Normalize(Vector3.Cross(CalculatePosition() - Target, right));

            Target += -right * delta.X * PanSpeed + actualUp * delta.Y * PanSpeed;
        }


        public Vector3 Position => CalculatePosition();

        public Vector3 Direction => Vector3.Normalize(Target - Position);
        public float Radius
        {
            get => _radius;
            set => _radius = MathHelper.Clamp(value, 0.1f, FarPlane * 0.9f);
        }
    }

}
