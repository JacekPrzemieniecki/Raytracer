using Raytracer.Samplers;

namespace Raytracer.Shaders
{
    internal class ReflectedVectorShader : Shader
    {
        private ISampler _normalSampler;

        public ReflectedVectorShader(ISampler normalSampler)
        {
            _normalSampler = normalSampler;
        }

        public override Vector3 Shade(Scene scene, RaycastHit hitInfo, int maxRecursiveRaycasts)
        {
            Ray r = ReflectedRay(hitInfo, _normalSampler);
            return r.Direction * 0.5f + new Vector3(0.5f, 0.5f, 0.5f);
        }
    }
}