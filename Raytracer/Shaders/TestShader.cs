using System.Drawing;

namespace Raytracer.Shaders
{
    class TestShader : Shader
    {
        public override Color Shade(Scene scene, RaycastHit hitInfo, int maxRecursiveRaycasts)
        {
            return Color.FromArgb((int)(0xFF * hitInfo.U), (int)(0xFF * hitInfo.V),
                    (int)(0xFF * (1 - hitInfo.U - hitInfo.V)));
        }
    }
}
