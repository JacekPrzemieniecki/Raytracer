using System;
using System.Drawing;

namespace Raytracer
{
    class Renderer
    {
        private int _pictureHeight;
        private int _pictureWidth;
        private Scene _scene;
        private const float renderedThreshold = 0.000001f;

        public void Render(Bitmap bmp, Scene scene, Action refreshAction, int passes, ref bool stopFlag)
        {
            _scene = scene;
            _pictureHeight = bmp.Height;
            _pictureWidth = bmp.Width;
            bool[][] renderedMask = new bool[_pictureWidth][];
            for (int i = 0; i < _pictureWidth; i++)
            {
                renderedMask[i] = new bool[_pictureHeight];
            }

            Random rng = new Random();
            for (int pass = 0; pass < passes; pass++)
                for (int y = 0; y < bmp.Size.Height; y++)
                {
                    for (int x = 0; x < bmp.Size.Width; x++)
                    {
                        if (renderedMask[x][y])
                        {
                            continue;
                        }

                        Vector3 pixelColor = RenderPixel(x, y, (float) rng.NextDouble(), (float) rng.NextDouble());
                        if (pass > 0)
                        {
                            Vector3 lastColor = Vector3.FromColor(bmp.GetPixel(x, y));
                            pixelColor = (lastColor * pass + pixelColor) / (pass + 1);
                            renderedMask[x][y] = (lastColor - pixelColor).LengthSquared() < renderedThreshold;
                        }
                        bmp.SetPixel(x, y, pixelColor.ToColor());
                    }
                    refreshAction();
                    if (stopFlag) return;
                }
        }

        private Vector3 RenderPixel(int screenX, int screenY, float dX, float dY)
        {
            float viewportX = (screenX + dX) / _pictureWidth * 2 - 1;
            // Screen Y is pointed down
            float viewportY = 1 - (screenY + dY) / _pictureHeight * 2;
            return _scene.SampleColor(viewportX, viewportY, 4);
        }
    }
}