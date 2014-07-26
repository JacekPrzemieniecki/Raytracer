using System.Drawing;

namespace Raytracer.Shaders
{
    class ReflectedVectorShader : Shader
    {
        public override Color Shade(Scene scene, RaycastHit hitInfo, int maxRecursiveRaycasts)
        {
            Ray r = ReflectedRay(hitInfo);
            int v = (int) (r.Direction.y * 0x7F + 0x7F);
            return Color.FromArgb(v, v, v);
        }
    }
}
