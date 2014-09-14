using System.Collections.Generic;
using Raytracer.LightSources;

#if DEBUG
using System.Threading;
using Raytracer.Debugging;
#endif

namespace Raytracer
{
    internal class Scene
    {
        public readonly List<LightSource> LightSources;
        private readonly Camera _camera;
        private readonly List<Mesh> _meshes;

        public Scene(List<Mesh> meshes, List<LightSource> lightSources, Camera camera)
        {
            _meshes = meshes;
            LightSources = lightSources;
            _camera = camera;
        }

        public bool IsVisible(Vector3 from, Vector3 to)
        {
            Vector3 direction = to - from;
            float distance = direction.Length();
            var ray = new Ray(from, direction);
            return IsVisible(ray, distance);
        }

        public bool IsVisible(Ray ray, float distance)
        {
            float dist = distance - 0.001f;
            foreach (Mesh mesh in _meshes)
            {
#if DEBUG
                Interlocked.Increment(ref Counters.BoundingBoxHits);
#endif
                RaycastHit meshHit = null;
                bool hitFound = mesh.Raycast(ray, dist, ref meshHit);
                if (hitFound)
                {
                    return false;
                }
            }
            return true;
        }

        public bool Raycast(Ray ray, float maxDistance, ref RaycastHit hitInfo)
        {
            float closestHitDistance = maxDistance;
            bool hitFound = false;
            foreach (Mesh mesh in _meshes)
            {
#if DEBUG
                Interlocked.Increment(ref Counters.BoundingBoxHits);
#endif
                RaycastHit meshHit = null;
                bool hit = mesh.Raycast(ray, closestHitDistance, ref meshHit);
                if (hit)
                {
                    hitInfo = meshHit;
                    closestHitDistance = meshHit.Distance;
                    hitFound = true;
                }
            }
            return hitFound;
        }

        public Vector3 SampleColor(float viewportX, float viewportY, int maxRecursiveRaycasts)
        {
            Ray ray = _camera.ViewportPointToRay(viewportX, viewportY);
            return SampleColor(ray, maxRecursiveRaycasts);
        }

        public Vector3 SampleColor(Ray ray, int maxRecursiveRaycasts)
        {
            RaycastHit raycastHit = null;
            bool hitFound = Raycast(ray, float.MaxValue, ref raycastHit);
            if (!hitFound)
            {
                return new Vector3(1, 1, 1);
            }
            return raycastHit.Mesh.SampleColor(this, raycastHit, maxRecursiveRaycasts);
        }
    }
}