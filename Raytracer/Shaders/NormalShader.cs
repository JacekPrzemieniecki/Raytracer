using System.Drawing;

namespace Raytracer.Shaders
{
    class NormalShader : Shader
    {
        public override Color Shade(Scene scene, Mesh mesh, RaycastHit hitInfo)
        {
            Vector3 normal = mesh.SurfaceNormal(hitInfo);
            return Color.FromArgb(0x7F + (int) (normal.x * 0x7F), 0x7F + (int) (normal.y * 0x7F), 0x7F + (int) (normal.z * 0x7F));
        }
    }
}
