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
        private Vector3 _position;

        public DirectionalLight(Vector3 position, float intensity, Color color, Vector3 direction)
        {
            _position = position;
            _intensity = intensity;
            _color = color;
            _direction = direction;
        }

        public override float IntensityAt(Vector3 position, Scene scene, out Color color)
        {
            color = Color.White;
            return 1;
        }
    }
}
