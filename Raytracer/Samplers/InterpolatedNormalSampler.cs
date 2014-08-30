using System;

namespace Raytracer.Samplers
{
    class InterpolatedNormalSampler : ISampler
    {
        private float _twoRoughness;
        private bool _isRough;
        private Random _rng;

        public InterpolatedNormalSampler()
        {
            _isRough = false;
        }

        public InterpolatedNormalSampler(float roughness)
        {
            _twoRoughness = roughness * 2;
            _isRough = true;
            _rng = new Random();
        }

        public Vector3 Sample(RaycastHit hitInfo)
        {
            Vector3 normal = hitInfo.Triangle.SurfaceNormal(hitInfo.U, hitInfo.V);
            if (!_isRough) return normal;
            return new Vector3(normal.x + (float)(_rng.NextDouble() - 0.5) * _twoRoughness, 
                               normal.y  + (float)(_rng.NextDouble() - 0.5) * _twoRoughness, 
                               normal.z  + (float)(_rng.NextDouble() - 0.5) * _twoRoughness).Normalized();
        }
    }
}
