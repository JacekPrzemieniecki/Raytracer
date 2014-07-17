using System;

namespace Raytracer
{
    internal class Mesh
    {
        public int[] Triangles;
        public Vector3[] Vertices;
        public int TriangleCount { get; private set; }

        public Mesh(Vector3[] vertices, int[] triangles)
        {
            Vertices = vertices;
            Triangles = triangles;
            TriangleCount = Triangles.Length / 3;
        }

        public Mesh()
            : this(new[] {
                new Vector3(0.0f, 0.0f, -5.0f), 
                new Vector3(3.0f, 3.0f, -5.0f), 
                new Vector3(0.0f, 3.0f, -5.0f),
                new Vector3(-1.0f, 0.0f, -3.0f),
                new Vector3(2.0f, 0.0f, -3.0f),
                new Vector3(1.75f, 1.0f, -3.0f)  
                },
                new[] { 0, 1, 2, 3, 4, 5 }
                )
        {
        }
    }
}