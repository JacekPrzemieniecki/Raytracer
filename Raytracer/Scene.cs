using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using Raytracer.LightSources;
using Raytracer.SampleShapes;
using Raytracer.Shaders;

namespace Raytracer
{
    internal class Scene
    {
        public List<Mesh> Meshes;
        public List<LightSource> LightSources;

        public Scene()
        {
            Shader shader = new TestShader();
            Meshes = new List<Mesh>
            { 
            new Cube(new Vector3(-3, -1, -7), 0.75f, shader),
            new TriangleSphere(new Vector3(0, 0, -5), 1.0, 10, 10, new DiffuseShader())
            };
            LightSources = new List<LightSource>
            {
                new DirectionalLight(1.0f, Color.White, (new Vector3(0.4f, -3, -2.5f).Normalized()) )
            };
            Camera = new Camera(8.0f / 6.0f, (float)Math.PI * 60f / 180f);
        }

        public Camera Camera { get; set; }
        public RaycastHit Raycast(Ray ray, float maxDistance)
        {
            var closestHit = new RaycastHit {Distance = maxDistance};
            foreach (var mesh in Meshes)
            {
                if (!mesh.BoundingBox.Raycast(ray, closestHit.Distance))
                {
                    continue;
                }
                #if DEBUG
                Interlocked.Increment(ref Debugging.Counters.BoundingBoxHits);
                #endif
                RaycastHit meshHit= mesh.Raycast(this, ray, closestHit.Distance);
                if (meshHit.Distance < closestHit.Distance)
                {
                    closestHit = meshHit;
                }
            }
            return closestHit;
        }

        public Color SampleColor(float viewportX, float viewportY)
        {
            Ray ray = Camera.ViewportPointToRay(viewportX, viewportY);
            RaycastHit raycastHit = Raycast(ray, float.MaxValue);
            if (raycastHit.Mesh == null)
            {
                return Color.White;
            }
            return raycastHit.Mesh.SampleColor(this, raycastHit);
        }
    }
}