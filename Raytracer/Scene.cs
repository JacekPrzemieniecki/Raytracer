using System.Collections.Generic;

namespace Raytracer
{
    internal class Scene
    {
        public List<Mesh> Meshes;

        public Scene()
        {
            Meshes = new List<Mesh> {new Mesh()};
            Camera = new Camera();
        }

        public Camera Camera { get; set; }
    }
}