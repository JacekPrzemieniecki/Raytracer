using System.Drawing;

namespace Raytracer.Shaders
{
    internal class PositionShader : Shader
    {
        public override Color Shade(Scene scene, RaycastHit hitInfo, int maxRecursiveRaycasts)
        {
            Vector3 position = hitInfo.Position;
            return Color.FromArgb((int) (0xff * (5 + position.z)), 0, 0); // (int) Math.Abs(0x01 * position.z));
        }
    }
}