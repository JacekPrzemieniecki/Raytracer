using System;
using System.Diagnostics;
using System.Globalization;

namespace Raytracer
{
    internal class Camera
    {
        public float AspectRatio { get; set; }

        private float _fov;
        public float FoV
        {
            get { return _fov; }
            set
            {
                tanHalfFoV = (float) Math.Tan(value/2);
                _fov = value;
                Debug.Print(tanHalfFoV.ToString(CultureInfo.InvariantCulture));
            }
        }

        private float tanHalfFoV;

        public Camera(float aspectRatio, float fov)
        {
            AspectRatio = aspectRatio;
            FoV = fov;
        }

        public Ray ViewportPointToRay(float posX, float posY)
        {
            float dirY = posY*tanHalfFoV;
            float dirX = posX*tanHalfFoV*AspectRatio;
            Vector3 rayDirection = new Vector3(dirX, dirY, -1.0f);
            return new Ray(Vector3.Zero, rayDirection);
        }
    }
}