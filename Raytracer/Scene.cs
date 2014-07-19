using System;
using System.Collections.Generic;
using Raytracer.SampleShapes;

namespace Raytracer
{
    internal class Scene
    {
        public List<Mesh> Meshes;

        public Scene()
        {
            Meshes = new List<Mesh>
            { 
            new Cube(new Vector3(-3, -1, -7), 0.75f),
            new SampleShapes.TriangleSphere(new Vector3(0, 0, -5), 1.0, 10, 10)};
            Camera = new Camera(8.0f / 6.0f, (float)Math.PI * 60f / 180f);
        }

        public Camera Camera { get; set; }
    }
}