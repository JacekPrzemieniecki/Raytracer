using System;
using System.Collections.Generic;

namespace Raytracer
{
    internal class Scene
    {
        public List<Mesh> Meshes;

        public Scene()
        {
            Meshes = new List<Mesh> {new Mesh()};
            Camera = new Camera(8.0f/6.0f, (float)Math.PI * 60f/180f);
        }

        public Camera Camera { get; set; }
    }
}