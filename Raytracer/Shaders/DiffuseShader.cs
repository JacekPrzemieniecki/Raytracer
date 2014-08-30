using System.Drawing;
using Raytracer.LightSources;
using Raytracer.Samplers;

namespace Raytracer.Shaders
{
    internal class DiffuseShader : Shader
    {
        private readonly ISampler _textureSampler;

        public DiffuseShader(ISampler textureSampler)
        {
            _textureSampler = textureSampler;
        }

        public override Vector3 Shade(Scene scene, RaycastHit hitInfo, int maxRecursiveRaycasts)
        {
            float totalIntensity = 0;
            foreach (LightSource lightSource in scene.LightSources)
            {
                Color c;
                totalIntensity += lightSource.IntensityAt(hitInfo.Position, hitInfo.Normal, scene, out c);
            }
            Vector3 diffuse = _textureSampler.Sample(hitInfo.Triangle, hitInfo.U, hitInfo.V);
            return diffuse * totalIntensity;
        }
    }
}