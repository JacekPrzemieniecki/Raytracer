using System.Threading;
using Raytracer.Debugging;

namespace Raytracer
{
    public class Box
    {
        public float MaxX;
        public float MaxY;
        public float MaxZ;
        public float MinX;
        public float MinY;
        public float MinZ;

        public Box(float minX, float maxX, float minY, float maxY, float minZ, float maxZ)
        {
            MinX = minX;
            MaxX = maxX;
            MinY = minY;
            MaxY = maxY;
            MinZ = minZ;
            MaxZ = maxZ;
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
                tMin = (MinX - ray.Origin.x) * rayDirectionXInverse;
                tMax = (MaxX - ray.Origin.x) * rayDirectionXInverse;
            }
            else
            {
                tMax = (MinX - ray.Origin.x) * rayDirectionXInverse;
                tMin = (MaxX - ray.Origin.x) * rayDirectionXInverse;
            }

            float tyMin;
            float tyMax;
            float rayDirectionYInverse = 1 / ray.Direction.y;
            if (rayDirectionYInverse >= 0)
            {
                tyMin = (MinY - ray.Origin.y) * rayDirectionYInverse;
                tyMax = (MaxY - ray.Origin.y) * rayDirectionYInverse;
            }
            else
            {
                tyMax = (MinY - ray.Origin.y) * rayDirectionYInverse;
                tyMin = (MaxY - ray.Origin.y) * rayDirectionYInverse;
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
                tzMin = (MinZ - ray.Origin.z) * rayDirectionZInverse;
                tzMax = (MaxZ - ray.Origin.z) * rayDirectionZInverse;
            }
            else
            {
                tzMax = (MinZ - ray.Origin.z) * rayDirectionZInverse;
                tzMin = (MaxZ - ray.Origin.z) * rayDirectionZInverse;
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