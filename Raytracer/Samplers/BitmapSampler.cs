using System.Drawing;

namespace Raytracer.Samplers
{
    class BitmapSampler : ISampler
    {
        private readonly Bitmap _bmp;

        public BitmapSampler(Bitmap bmp)
        {
            _bmp = bmp;
        }

        public Vector3 Sample(RaycastHit hitInfo)
        {
            Vector2 position = hitInfo.Triangle.UVCoordinates(hitInfo.U, hitInfo.V);
            Color color = _bmp.GetPixel((int) (position.x * _bmp.Width + 0.5), (int) (position.y * _bmp.Height + 0.5));
            return Vector3.FromColor(color);
        }
    }
}
