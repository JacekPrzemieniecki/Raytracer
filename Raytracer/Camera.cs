using System;
using System.Diagnostics;
using System.Globalization;

namespace Raytracer
{
    internal class Camera
    {
        private readonly Vector3 _position;

        private float _tanHalfFoV;

        public Camera(Vector3 position, float aspectRatio, float fov)
        {
            _position = position;
            AspectRatio = aspectRatio;
            FoV = fov;
        }

        private float AspectRatio { get; set; }

        private float FoV
        {
            set
            {
                _tanHalfFoV = (float) Math.Tan(value / 2);
                Debug.Print(_tanHalfFoV.ToString(CultureInfo.InvariantCulture));
            }
        }

        public Ray ViewportPointToRay(float posX, float posY)
        {
            float dirY = posY * _tanHalfFoV;
            float dirX = posX * _tanHalfFoV * AspectRatio;
            var rayDirection = new Vector3(dirX, dirY, -1.0f);
            return new Ray(_position, rayDirection);
        }
    }
}