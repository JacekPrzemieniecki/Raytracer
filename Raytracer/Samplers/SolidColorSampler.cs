using System.Drawing;

namespace Raytracer.Samplers
{
    class SolidColorSampler : TextureSampler
    {
        private readonly Vector3 _color;

        public SolidColorSampler(Color color)
        {
            _color = Vector3.FromColor(color);
        }

        public override Vector3 Sample(RaycastHit hitInfo)
        {
            return _color;
        }
    }
}
