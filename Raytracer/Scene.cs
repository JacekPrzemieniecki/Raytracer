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
            Shader normal = new NormalShader();
            Shader diffuse = new DiffuseShader();
            //Shader glossy = new GlossyShader(0.4f);
            //Shader position = new PositionShader();
            //Shader reflected = new ReflectedVectorShader();
            _meshes = new List<Mesh>
            {
                new Cube(new Vector3(-3, -1, -7), 0.75f, normal),
                new TriangleSphere(new Vector3(0, 0, -5), 1.0, 10, 10, diffuse, true),
                //new Plane(new Vector3(0, -1, -5), Vector3.Left, Vector3.Forward, 100, diffuse)
            };
            LightSources = new List<LightSource>
            {
                new DirectionalLight(1.0f, Color.White, (new Vector3(0.4f, -3, -2.5f).Normalized()))
            };
            _camera = new Camera(Vector3.Zero, 8.0f / 6.0f, (float) Math.PI * 60f / 180f);
        }

        private RaycastHit Raycast(Ray ray, float maxDistance)
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

        public Color SampleColor(float viewportX, float viewportY, int maxRecursiveRaycasts)
        {
            Ray ray = _camera.ViewportPointToRay(viewportX, viewportY);
            return SampleColor(ray, maxRecursiveRaycasts);
        }

        public Color SampleColor(Ray ray, int maxRecursiveRaycasts)
        {
            RaycastHit raycastHit = Raycast(ray, float.MaxValue);
            if (raycastHit.Mesh == null)
            {
                return Color.White;
            }
            return raycastHit.Mesh.SampleColor(this, raycastHit, maxRecursiveRaycasts - 1);
        }
    }
}