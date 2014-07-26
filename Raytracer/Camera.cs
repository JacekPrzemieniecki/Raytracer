using System;
using System.Diagnostics;
using System.Globalization;

namespace Raytracer
{
    internal class Camera
    {
        public Vector3 Position { get; set; }
        private float AspectRatio { get; set; }

        private float FoV
        {
            set
            {
                _tanHalfFoV = (float) Math.Tan(value/2);
                Debug.Print(_tanHalfFoV.ToString(CultureInfo.InvariantCulture));
            }
        }

        private float _tanHalfFoV;

        public Camera(Vector3 position, float aspectRatio, float fov)
        {
            Position = position;
            AspectRatio = aspectRatio;
            FoV = fov;
        }

        public Ray ViewportPointToRay(float posX, float posY)
        {
            float dirY = posY*_tanHalfFoV;
            float dirX = posX*_tanHalfFoV*AspectRatio;
            var rayDirection = new Vector3(dirX, dirY, -1.0f);
            return new Ray(Position, rayDirection);
        }
    }
}