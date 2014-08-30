using System.Drawing;

namespace Raytracer.Samplers
{
    class SolidColorSampler : ISampler
    {
        private readonly Vector3 _color;

        public SolidColorSampler(Color color)
        {
            _color = Vector3.FromColor(color);
        }

        public Vector3 Sample(RaycastHit hitInfo)
        {
            return _color;
        }
    }
}
