using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using Raytracer.SampleShapes;
using Raytracer.Shaders;

namespace Raytracer
{
    internal class Scene
    {
        public List<Mesh> Meshes;

        public Scene()
        {
            Shader shader = new TestShader();
            Meshes = new List<Mesh>
            { 
            new Cube(new Vector3(-3, -1, -7), 0.75f, shader),
            new TriangleSphere(new Vector3(0, 0, -5), 1.0, 10, 10, shader)
            };
            Camera = new Camera(8.0f / 6.0f, (float)Math.PI * 60f / 180f);
        }

        public Camera Camera { get; set; }
        public float Raycast(Ray ray, ref Color color, float maxDistance)
        {
            float distance = maxDistance;
            foreach (var mesh in Meshes)
            {
                if (!mesh.BoundingBox.Raycast(ray, distance))
                {
                    continue;
                }
                #if DEBUG
                Interlocked.Increment(ref Debugging.Counters.BoundingBoxHits);
                #endif
                float meshDistance = mesh.Raycast(ray, ref color, distance);
                if (meshDistance < distance)
                {
                    distance = meshDistance;
                }
            }
            return distance;
        }
    }
}