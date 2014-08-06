using System;

namespace Raytracer.Shaders
{
    class MixShader : Shader
    {
        private readonly Shader _shader1;
        private Shader _shader2;
        private readonly float _coefficient;

        public MixShader(Shader shader1, Shader shader2, float coefficient)
        {
            _shader1 = shader1;
            _shader2 = shader2;
            this._coefficient = coefficient;
        }

        public override Vector3 Shade(Scene scene, RaycastHit hitInfo, int maxRecursiveRaycasts)
        {
            return _shader1.Shade(scene, hitInfo, maxRecursiveRaycasts) * _coefficient +
                   _shader2.Shade(scene, hitInfo, maxRecursiveRaycasts) * (1 - _coefficient);
        }
    }
}
