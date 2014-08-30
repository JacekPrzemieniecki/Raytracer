namespace Raytracer.Shaders
{
    internal class MixShader : Shader
    {
        private readonly float _coefficient;
        private readonly Shader _shader1;
        private readonly Shader _shader2;

        public MixShader(Shader shader1, Shader shader2, float coefficient)
        {
            _shader1 = shader1;
            _shader2 = shader2;
            _coefficient = coefficient;
        }

        public override Vector3 Shade(Scene scene, RaycastHit hitInfo, int maxRecursiveRaycasts)
        {
            return _shader1.Shade(scene, hitInfo, maxRecursiveRaycasts) * _coefficient +
                   _shader2.Shade(scene, hitInfo, maxRecursiveRaycasts) * (1 - _coefficient);
        }
    }
}