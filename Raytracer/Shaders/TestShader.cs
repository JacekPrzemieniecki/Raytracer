using Raytracer.Samplers;

namespace Raytracer.Shaders
{
    internal class TestShader : Shader
    {
        private TextureSampler _sampler;
        public TestShader(TextureSampler sampler)
        {
            _sampler = sampler;
        }

        public override Vector3 Shade(Scene scene, RaycastHit hitInfo, int maxRecursiveRaycasts)
        {
            return _sampler.Sample(hitInfo) * 0.5f + new Vector3(0.5f, 0.5f, 0.5f);
        }
    }
}