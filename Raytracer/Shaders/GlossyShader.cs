using System.Drawing;

namespace Raytracer.Shaders
{
    internal class GlossyShader : Shader
    {
        private readonly float _oneMinRoughness;
        private readonly float _roughness;

        public GlossyShader(float roughness)
        {
            _roughness = roughness;
            _oneMinRoughness = 1 - roughness;
        }

        public override Color Shade(Scene scene, RaycastHit hitInfo, int maxRecursiveRaycasts)
        {
            if (maxRecursiveRaycasts == 0) return hitInfo.Triangle.Color;
            Ray ray = ReflectedRay(hitInfo);
            Color reflectedColor = scene.SampleColor(ray, maxRecursiveRaycasts - 1);
            Color triangleColor = hitInfo.Triangle.Color;
            Color returnColor =
                Color.FromArgb((int) (triangleColor.R * _roughness + reflectedColor.R * _oneMinRoughness),
                    (int) (triangleColor.G * _roughness + reflectedColor.G * _oneMinRoughness),
                    (int) (triangleColor.B * _roughness + reflectedColor.B * _oneMinRoughness));
            return returnColor;
        }
    }
}