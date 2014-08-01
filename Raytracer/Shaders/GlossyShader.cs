namespace Raytracer.Shaders
{
    internal class GlossyShader : Shader
    {
        public override Vector3 Shade(Scene scene, RaycastHit hitInfo, int maxRecursiveRaycasts)
        {
            Vector3 triangleColor = Vector3.FromColor(hitInfo.Triangle.Color);
            if (maxRecursiveRaycasts == 0) return Vector3.FromColor(hitInfo.Triangle.Color);
            Ray ray = ReflectedRay(hitInfo);
            return scene.SampleColor(ray, maxRecursiveRaycasts - 1);
        }
    }
}