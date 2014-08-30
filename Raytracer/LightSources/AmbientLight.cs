using System.Drawing;

namespace Raytracer.LightSources
{
    internal class AmbientLight : LightSource
    {
        private readonly Color _color;
        private readonly float _intensity;

        public AmbientLight(float intensity, Color color)
        {
            _intensity = intensity;
            _color = color;
        }


        public override float IntensityAt(Vector3 position, Vector3 surfaceNormal, Scene scene, out Color color)
        {
            color = _color;
            return _intensity;
        }
    }
}