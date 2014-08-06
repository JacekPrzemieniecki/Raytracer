using Raytracer.Samplers;

namespace Raytracer.Shaders
{
    internal class GlossyShader : Shader
    {
        private TextureSampler _normalSampler;

        public GlossyShader(TextureSampler normalSampler)
        {
            _normalSampler = normalSampler;
        }

        public override Vector3 Shade(Scene scene, RaycastHit hitInfo, int maxRecursiveRaycasts)
        {
            if (maxRecursiveRaycasts == 0) return new Vector3(0, 0, 0);
            Ray ray = ReflectedRay(hitInfo, _normalSampler);
            return scene.SampleColor(ray, maxRecursiveRaycasts - 1);
        }
    }
}