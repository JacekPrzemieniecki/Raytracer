using Raytracer.Samplers;

namespace Raytracer.Shaders
{
    internal class TestShader : Shader
    {
        private readonly ISampler _sampler;

        public TestShader(ISampler sampler)
        {
            _sampler = sampler;
        }

        public override Vector3 Shade(Scene scene, RaycastHit hitInfo, int maxRecursiveRaycasts)
        {
            return _sampler.Sample(hitInfo.Triangle, hitInfo.U, hitInfo.V) * 0.5f + new Vector3(0.5f, 0.5f, 0.5f);
        }
    }
}