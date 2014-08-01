using System;
using System.Drawing;

namespace Raytracer.LightSources
{
    internal class PointLight : LightSource
    {
        private Vector3 _position;
        private float _intensity;
        private Color _color;
        private float _rangeSquared;

        public PointLight(Vector3 position, float intensity, float range, Color color)
        {
            _position = position;
            _intensity = intensity;
            _color = color;
            _rangeSquared = range * range;
        }

        public override float IntensityAt(Vector3 position, Vector3 surfaceNormal, Scene scene, out Color color)
        {
            color = _color;
            Vector3 direction = position - _position;
            float distance = direction.Length();
            Ray ray = new Ray(_position, direction);
            bool visible = scene.IsVisible(ray, distance);
            if (!visible) return 0;
            float intensity = _intensity * _rangeSquared / (_rangeSquared + distance * distance);
            float normalAdjustedIntensity = intensity * Vector3.Dot(surfaceNormal, Vector3.Zero - direction.Normalized());
            return Math.Max(normalAdjustedIntensity, 0);
        }
    }
}