using System;

namespace Raytracer.Samplers
{
    class InterpolatedNormalSampler : ISampler
    {
        private readonly float _twoRoughness;
        private readonly bool _isRough;
        private readonly Random _rng;

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
        
        public Vector3 Sample(Triangle triangle, float u, float v)
        {
            Vector3 normal = triangle.SurfaceNormal(u, v);
            if (!_isRough) return normal;
            return new Vector3(normal.x + (float)(_rng.NextDouble() - 0.5) * _twoRoughness,
                               normal.y + (float)(_rng.NextDouble() - 0.5) * _twoRoughness,
                               normal.z + (float)(_rng.NextDouble() - 0.5) * _twoRoughness).Normalized();
        }
    }
}
