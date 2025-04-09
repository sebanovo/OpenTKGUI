using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace OpenTKGUI.Src.Utils
{
    public class OrbitCamera
    {
        // Propiedades de la cámara
        private Vector3 _position;
        private Vector3 _target = Vector3.Zero;
        private Vector3 _up = Vector3.UnitY;
        private float _distance;

        // Propiedades de proyección (las que ya tenías)
        private float _fov = MathHelper.PiOver2;
        public float AspectRatio { get; set; }

        // Parámetros de control
        private float _zoomSpeed = 0.001f;
        private float _rotationSpeed = 0.005f;
        private float _panSpeed = 0.01f;
        private bool _isRotating = false;
        private bool _isPanning = false;
        private Vector2 _lastMousePos;

        public OrbitCamera(Vector3 position, float aspectRatio)
        {
            _position = position;
            _distance = (position - _target).Length;
            AspectRatio = aspectRatio;
        }

        // Métodos que ya usabas (se mantienen igual)
        public Matrix4 GetViewMatrix() => Matrix4.LookAt(_position, _target, _up);

        public Matrix4 GetProjectionMatrix() =>
            Matrix4.CreatePerspectiveFieldOfView(_fov, AspectRatio, 0.01f, 100f);

        // Propiedad para acceder a la posición (por si la necesitas)
        public Vector3 Position => _position;

        // Métodos de control (nuevos)
        public void ProcessMouseWheel(float delta)
        {
            _distance -= delta * _zoomSpeed;
            _distance = Math.Max(0.1f, _distance);
            UpdateCameraPosition();
        }

        // public void ProcessMouseDown(Vector2 mousePos, MouseButtons button)
        // {
        //     _lastMousePos = mousePos;h
        //     _isRotating = button == MouseButtons.Left;
        //     _isPanning = button == MouseButtons.Middle;
        // }

        public void ProcessMouseUp()
        {
            _isRotating = false;
            _isPanning = false;
        }

        public void ProcessMouseMove(Vector2 mousePos)
        {
            var delta = mousePos - _lastMousePos;
            _lastMousePos = mousePos;

            if (_isRotating)
            {
                Orbit(delta);
            }
            else if (_isPanning)
            {
                Pan(delta);
            }
        }

        private void Orbit(Vector2 delta)
        {
            Vector3 right = Vector3.Normalize(Vector3.Cross(_up, _position - _target));

            // Rotación vertical con matrices
            float verticalAngle = -delta.Y * _rotationSpeed;
            Matrix4 verticalRot = Matrix4.CreateFromAxisAngle(right, verticalAngle);

            // Aplicar rotación
            Vector3 newPos = _target + Vector3.TransformVector(_position - _target, verticalRot);

            // Prevenir flipping
            Vector3 newDirection = Vector3.Normalize(newPos - _target);
            if (Math.Abs(Vector3.Dot(newDirection, _up)) < 0.99f)
            {
                _position = newPos;
                _up = Vector3.TransformVector(_up, verticalRot);
            }

            // Rotación horizontal
            float horizontalAngle = -delta.X * _rotationSpeed;
            Matrix4 horizontalRot = Matrix4.CreateFromAxisAngle(_up, horizontalAngle);
            _position = _target + Vector3.TransformVector(_position - _target, horizontalRot);

            UpdateCameraPosition();
        }

        private void Pan(Vector2 delta)
        {
            Vector3 right = Vector3.Normalize(Vector3.Cross(_up, _position - _target));
            Vector3 actualUp = Vector3.Normalize(Vector3.Cross(_position - _target, right));

            _target += -right * delta.X * _panSpeed + actualUp * delta.Y * _panSpeed;
            UpdateCameraPosition();
        }

        private void UpdateCameraPosition()
        {
            // Mantiene la distancia constante después de rotar/mover
            Vector3 direction = Vector3.Normalize(_position - _target);
            _position = _target + direction * _distance;
        }
    }
}
