using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using Raytracer.Debugging;
using Raytracer.LightSources;
using Raytracer.SampleShapes;
using Raytracer.Shaders;

namespace Raytracer
{
    internal class Scene
    {
        public readonly List<LightSource> LightSources;
        private readonly Camera _camera;
        private readonly List<Mesh> _meshes;

        public Scene()
        {
            Vector3 cameraPosition = new Vector3(0, 3, 5);
            _camera = new Camera(cameraPosition, 8.0f / 6.0f, (float)Math.PI * 60f / 180f);

            //Shader normal = new NormalShader();
            Shader diffuse = new DiffuseShader();
            Shader glossy = new GlossyShader();
            //Shader position = new PositionShader();
            //Shader reflected = new ReflectedVectorShader();

            Mesh sphere = new TriangleSphere(new Vector3(3, 2, -6), 2.0, 16, 20, diffuse, true);
            Mesh cube = new Cube(new Vector3(-2, 1, -5), 2, diffuse);
            Mesh backWall = new Plane(new Vector3(0, 0, -10), Vector3.Right, Vector3.Down, 100, diffuse);
            Mesh floor = new Plane(new Vector3(0, 0, 0), Vector3.Right, Vector3.Forward, 100, glossy);
            Mesh leftWall = new Plane(new Vector3(-5, 0, 0), Vector3.Forward, Vector3.Up, 100, glossy);
            Mesh rightWall = new Plane(new Vector3(5, 0, 0), Vector3.Back, Vector3.Up, 100, glossy);

            _meshes = new List<Mesh>
            {
                cube,
                sphere,
                backWall,
                floor,
                leftWall,
                rightWall
            };

            LightSource cameraLightSource = new PointLight(cameraPosition, 3, 10, Color.White);
            LightSources = new List<LightSource>
            {
                //new PointLight(new Vector3(-4.5f, 0.5f, -9.5f), 2.0f, 5, Color.White),
                //new PointLight(new Vector3(4.5f, 0.5f, -9.5f), 2.0f, 5, Color.White),
                new PointLight(new Vector3(-4.5f, 7.5f, -9.5f), 2.0f, 5, Color.White),
                new PointLight(new Vector3(4.5f, 7.5f, -9.5f), 2.0f, 5, Color.White),
                cameraLightSource,
                //new PointLight(new Vector3(0, 1, -3.9f), 3, Color.White ),
            };
        }

        public bool IsVisible(Vector3 from, Vector3 to)
        {
            Vector3 direction = to - from;
            float distance = direction.Length();
            Ray ray = new Ray(from, direction);
            return IsVisible(ray, distance);
        }

        public bool IsVisible(Ray ray, float distance)
        {
            float dist = distance - 0.001f;
            foreach (Mesh mesh in _meshes)
            {
                if (!mesh.BoundingBox.Raycast(ray, dist))
                {
                    continue;
                }
#if DEBUG
                Interlocked.Increment(ref Counters.BoundingBoxHits);
#endif
                RaycastHit meshHit = mesh.Raycast(ray, dist);
                if (meshHit.Distance < dist)
                {
                    return false;
                }
            }
            return true;
        }

        public RaycastHit Raycast(Ray ray, float maxDistance)
        {
            var closestHit = new RaycastHit {Distance = maxDistance};
            foreach (Mesh mesh in _meshes)
            {
                if (!mesh.BoundingBox.Raycast(ray, closestHit.Distance))
                {
                    continue;
                }
#if DEBUG
                Interlocked.Increment(ref Counters.BoundingBoxHits);
#endif
                RaycastHit meshHit = mesh.Raycast(ray, closestHit.Distance);
                if (meshHit.Distance < closestHit.Distance)
                {
                    closestHit = meshHit;
                }
            }
            return closestHit;
        }

        public Vector3 SampleColor(float viewportX, float viewportY, int maxRecursiveRaycasts)
        {
            Ray ray = _camera.ViewportPointToRay(viewportX, viewportY);
            return SampleColor(ray, maxRecursiveRaycasts);
        }

        public Vector3 SampleColor(Ray ray, int maxRecursiveRaycasts)
        {
            RaycastHit raycastHit = Raycast(ray, float.MaxValue);
            if (raycastHit.Mesh == null)
            {
                return new Vector3(1, 1, 1);
            }
            return raycastHit.Mesh.SampleColor(this, raycastHit, maxRecursiveRaycasts - 1);
        }
    }
}