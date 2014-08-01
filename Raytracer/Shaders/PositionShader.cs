using System;
using System.Drawing;

namespace Raytracer.Shaders
{
    internal class PositionShader : Shader
    {
        public override Color Shade(Scene scene, RaycastHit hitInfo, int maxRecursiveRaycasts)
        {
            Vector3 position = hitInfo.Position;
            return Color.FromArgb((int) Math.Abs(0xff * position.y)% 0xff, 0, 0);
        }
    }
}