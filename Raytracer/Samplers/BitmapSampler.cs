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

        public Vector3 Sample(Triangle triangle, float u, float v)
        {

            Vector2 position = triangle.UVCoordinates(u, v);
            Color color = _bmp.GetPixel((int)(position.x * _bmp.Width + 0.5), (int)(position.y * _bmp.Height + 0.5));
            return Vector3.FromColor(color);
        }
    }
}
