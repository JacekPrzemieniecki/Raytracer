using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Raytracer
{
    class Renderer
    {
        private Scene scene;
        public void Render(Bitmap bmp, Scene scene)
        {
            this.scene = scene;
            for (int y = 0; y < bmp.Size.Height; y++)
            {
                for (int x = 0; x < bmp.Size.Width; x++)
                {
                    bmp.SetPixel(x, y, RenderPixel(x, y));
                }
            }
        }

        private Color RenderPixel(int x, int y)
        {
            return Color.Red;
        }
    }
}
