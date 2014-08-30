using System.Drawing;

namespace Raytracer.Samplers
{
    internal class SolidColorSampler : ISampler
    {
        private readonly Vector3 _color;

        public SolidColorSampler(Color color)
        {
            _color = Vector3.FromColor(color);
        }

        public Vector3 Sample(Triangle triangle, float u, float v)
        {
            return _color;
        }
    }
}