using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raytracer
{
    class Scene
    {
        public List<Mesh> meshes;

        public Scene()
        {
            meshes = new List<Mesh>();
            meshes.Add(new Mesh());
        }
    }
}
