namespace Raytracer.Shaders
{
    internal class NormalShader : Shader
    {
        public override Vector3 Shade(Scene scene, RaycastHit hitInfo, int maxRecursiveRaycasts)
        {
            return hitInfo.Triangle.SurfaceNormal(hitInfo.U, hitInfo.V);
        }
    }
}