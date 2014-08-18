using System;
using System.Drawing;

namespace Raytracer.LightSources
{
    internal class DirectionalLight : LightSource
    {
        private readonly Color _color;
        private readonly float _intensity;
        private readonly Vector3 _reverseDirection;

        public DirectionalLight(float intensity, Color color, Vector3 direction)
        {
            _intensity = intensity;
            _color = color;
            _reverseDirection = Vector3.Zero - direction;
        }

        public override float IntensityAt(Vector3 position, Vector3 surfaceNormal, Scene scene, out Color color)
        {
            color = _color;
            RaycastHit hit = null;
            bool hitFound = scene.Raycast(new Ray(position, _reverseDirection), float.MaxValue, ref hit);
            return hitFound ? 0 : Math.Max(Vector3.Dot(surfaceNormal, _reverseDirection) * _intensity, 0);
        }
    }
}