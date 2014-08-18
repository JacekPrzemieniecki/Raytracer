using Raytracer.Samplers;

namespace Raytracer.Shaders
{
    abstract class Shader
    {
        public abstract Vector3 Shade(Scene scene, RaycastHit hitInfo, int maxRecursiveRaycasts);

        protected static Ray ReflectedRay(RaycastHit hitInfo, TextureSampler normalSampler)
        {
            Vector3 normal = normalSampler.Sample(hitInfo);
            Vector3 incident = hitInfo.Ray.Direction;
            Vector3 reflected = incident - 2 * Vector3.Dot(incident, normal) * normal;
            return new Ray(hitInfo.Position, reflected);
        }
    }
}