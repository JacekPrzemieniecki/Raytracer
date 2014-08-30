namespace Raytracer.Shaders
{
    internal class ReflectedVectorShader : Shader
    {
        public override Vector3 Shade(Scene scene, RaycastHit hitInfo, int maxRecursiveRaycasts)
        {
            Ray r = ReflectedRay(hitInfo);
            return r.Direction * 0.5f + new Vector3(0.5f, 0.5f, 0.5f);
        }
    }
}