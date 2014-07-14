namespace Raytracer
{
    internal class Mesh
    {
        public int[] Triangles;
        public Vector3[] Vertices;
        public int TriangleCount { get; private set; }

        public Mesh()
        {
            Vertices = new[] {
                new Vector3(-0.5f, -0.3f, 1.0f), 
                new Vector3(0.1f, 0.0f, 1.0f), 
                new Vector3(0.0f, 0.15f, 1.0f),
                new Vector3(0.3f, 0.3f, 1.0f),
                new Vector3(0.25f, -0.5f, 1),
                new Vector3(0.45f, 0.45f, 1)  
            };
            Triangles = new[] {0, 1, 2, 3, 4, 5};
            TriangleCount = Triangles.Length/3;
        }
    }
}