﻿using System;
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
            Shader normal = new NormalShader();
            Shader diffuse = new DiffuseShader();
            Shader glossy = new GlossyShader(0.4f);
            Shader position = new PositionShader();
            Shader reflected = new ReflectedVectorShader();
            Meshes = new List<Mesh>
            { 
            new Cube(new Vector3(-3, -0.5f, -4), 1, normal),
            new TriangleSphere(new Vector3(0, 0, -7), 1.0, 10, 10, glossy, true),
            new Plane(new Vector3(0, -1, -5), Vector3.Left, Vector3.Forward, 100, diffuse)
            };
            LightSources = new List<LightSource>
            {
                new DirectionalLight(1.0f, Color.White, (new Vector3(0.4f, -3, -2.5f).Normalized()) )
            };
            Camera = new Camera(Vector3.Zero, 8.0f / 6.0f, (float)Math.PI * 50f / 180f);
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
                RaycastHit meshHit= mesh.Raycast(ray, closestHit.Distance);
                if (meshHit.Distance < closestHit.Distance)
                {
                    closestHit = meshHit;
                }
            }
            return closestHit;
        }

        public Color SampleColor(float viewportX, float viewportY, int maxRecursiveRaycasts)
        {
            Ray ray = Camera.ViewportPointToRay(viewportX, viewportY);
            return SampleColor(ray, maxRecursiveRaycasts);
        }

        public Color SampleColor(Ray ray, int maxRecursiveRaycasts )
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