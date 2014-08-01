namespace Raytracer.Shaders
{
    internal class PositionShader : Shader
    {
        public override Vector3 Shade(Scene scene, RaycastHit hitInfo, int maxRecursiveRaycasts)
        {
            return hitInfo.Position;
        }
    }
}