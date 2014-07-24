using System.Drawing;

namespace Raytracer.Shaders
{
    class TestShader : Shader
    {
        public override Color Shade(Mesh mesh, RaycastHit hitInfo)
        {
            return Color.FromArgb((int)(0xFF * hitInfo.u), (int)(0xFF * hitInfo.v),
                    (int)(0xFF * (1 - hitInfo.u - hitInfo.v)));
        }
    }
}
