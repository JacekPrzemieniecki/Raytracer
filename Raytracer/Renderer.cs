using System.Drawing;

namespace Raytracer
{
    internal class Renderer
    {
        private Scene _scene;
        private int _pictureWidth;
        private int _pictureHeight;

        public void Render(Bitmap bmp, Scene scene)
        {
            _scene = scene;
            _pictureHeight = bmp.Height;
            _pictureWidth = bmp.Width;
            for (int y = 0; y < bmp.Size.Height; y++)
            {
                for (int x = 0; x < bmp.Size.Width; x++)
                {
                    bmp.SetPixel(x, y, RenderPixel(x, y));
                }
            }
        }

        private Color RenderPixel(int screenX, int screenY)
        {
            float viewportX = (screenX + 0.5f) / _pictureWidth * 2 - 1;
            // Screen Y is pointed down
            float viewportY = 1 - (screenY + 0.5f) / _pictureHeight * 2;
            return _scene.SampleColor(viewportX, viewportY, 4);
        }
    }
}