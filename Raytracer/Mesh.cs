using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raytracer
{
    class Mesh
    {
        public Vector3[] vertices;
        public int[] triangles;

        public Mesh()
        {
            vertices = new Vector3[] {new Vector3(0.0f, 0.0f, 0.0f), new Vector3(1.0f, 0.0f, 0.0f), new Vector3(0.0f, 1.0f, 0.0f)};
            triangles = new int[] {0, 1, 2};
        }
    }
}
