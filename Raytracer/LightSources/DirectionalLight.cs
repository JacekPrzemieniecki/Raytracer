using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raytracer.LightSources
{
    class DirectionalLight : LightSource
    {
        private float _intensity;
        private Color _color;
        private Vector3 _direction;
        private Vector3 _reverseDirection;

        public DirectionalLight(float intensity, Color color, Vector3 direction)
        {
            _intensity = intensity;
            _color = color;
            _direction = direction;
            _reverseDirection = Vector3.Zero - direction;
        }

        public override float IntensityAt(Vector3 position, Vector3 surfaceNormal, Scene scene, out Color color)
        {
            color = Color.White;
            return Vector3.Dot(surfaceNormal, _reverseDirection) * _intensity;
        }
    }
}
