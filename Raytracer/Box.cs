#if DEBUG
using System.Threading;
using Raytracer.Debugging;

#endif

namespace Raytracer
{
    internal class Box
    {
        private readonly float _maxX;
        private readonly float _maxY;
        private readonly float _maxZ;
        private readonly float _minX;
        private readonly float _minY;
        private readonly float _minZ;

        public Box(float minX, float maxX, float minY, float maxY, float minZ, float maxZ)
        {
            _minX = minX;
            _maxX = maxX;
            _minY = minY;
            _maxY = maxY;
            _minZ = minZ;
            _maxZ = maxZ;
        }

        public bool Raycast(Ray ray, float maxDistance)
        {
#if DEBUG
            Interlocked.Increment(ref Counters.BoundingBoxChecks);
#endif
            float tMin;
            float tMax;
            float rayDirectionXInverse = 1 / ray.Direction.x;
            if (rayDirectionXInverse >= 0)
            {
                tMin = (_minX - ray.Origin.x) * rayDirectionXInverse;
                tMax = (_maxX - ray.Origin.x) * rayDirectionXInverse;
            }
            else
            {
                tMax = (_minX - ray.Origin.x) * rayDirectionXInverse;
                tMin = (_maxX - ray.Origin.x) * rayDirectionXInverse;
            }

            float tyMin;
            float tyMax;
            float rayDirectionYInverse = 1 / ray.Direction.y;
            if (rayDirectionYInverse >= 0)
            {
                tyMin = (_minY - ray.Origin.y) * rayDirectionYInverse;
                tyMax = (_maxY - ray.Origin.y) * rayDirectionYInverse;
            }
            else
            {
                tyMax = (_minY - ray.Origin.y) * rayDirectionYInverse;
                tyMin = (_maxY - ray.Origin.y) * rayDirectionYInverse;
            }

            if ((tMin > tyMax) || (tyMin > tMax))
            {
                return false;
            }

            if (tyMin > tMin)
            {
                tMin = tyMin;
            }
            if (tyMax < tMax)
            {
                tMax = tyMax;
            }

            float tzMin;
            float tzMax;
            float rayDirectionZInverse = 1 / ray.Direction.z;
            if (rayDirectionZInverse >= 0)
            {
                tzMin = (_minZ - ray.Origin.z) * rayDirectionZInverse;
                tzMax = (_maxZ - ray.Origin.z) * rayDirectionZInverse;
            }
            else
            {
                tzMax = (_minZ - ray.Origin.z) * rayDirectionZInverse;
                tzMin = (_maxZ - ray.Origin.z) * rayDirectionZInverse;
            }

            if (tMin > tzMax || tzMin > tMax)
            {
                return false;
            }
            if (tzMin > tMin)
            {
                tMin = tzMin;
            }
            if (tzMax < tMax)
            {
                tMax = tzMax;
            }
            if (tMax < tMin || tMax < 0 || tMin > maxDistance)
            {
                return false;
            }
            return true;
        }
    }
}