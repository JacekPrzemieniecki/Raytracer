namespace Raytracer.Shaders
{
    internal class TestShader : Shader
    {
        public override Vector3 Shade(Scene scene, RaycastHit hitInfo, int maxRecursiveRaycasts)
        {
            return new Vector3(hitInfo.U, hitInfo.V, 1 - hitInfo.U - hitInfo.V);
        }
    }
}