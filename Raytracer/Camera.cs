using System;

namespace Raytracer
{
    internal class Camera
    {
        private readonly Vector3 _position;
        private readonly Quaternion _rotation;
        private float _tanHalfFoV;

        public Camera(Vector3 position, Quaternion rotation, float aspectRatio, float fov)
        {
            _position = position;
            _rotation = rotation.Normalized();
            AspectRatio = aspectRatio;
            FoV = fov;
        }

        private float AspectRatio { get; set; }

        private float FoV
        {
            set { _tanHalfFoV = (float) Math.Tan(value / 2); }
        }

        public Ray ViewportPointToRay(float posX, float posY)
        {
            float dirY = posY * _tanHalfFoV;
            float dirX = posX * _tanHalfFoV * AspectRatio;
            var rayDirection = new Vector3(dirX, dirY, 1.0f);
            Vector3 rotatedDirection = rayDirection.RotatedBy(_rotation);
            return new Ray(_position, rotatedDirection);
        }
    }
}