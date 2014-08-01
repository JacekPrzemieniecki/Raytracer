using System.Drawing;

namespace Raytracer.LightSources
{
    class AmbientLight :LightSource
    {
        private float _intensity;
        private Color _color;

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
