using System.Drawing;

namespace Raytracer.Shaders
{
    class NormalShader : Shader
    {
        public override Color Shade(Scene scene, RaycastHit hitInfo)
        {
            Vector3 normal = hitInfo.Triangle.SurfaceNormal(hitInfo.U, hitInfo.V);
            return Color.FromArgb(0x7F + (int) (normal.x * 0x7F), 0x7F + (int) (normal.y * 0x7F), 0x7F + (int) (normal.z * 0x7F));
        }
    }
}
