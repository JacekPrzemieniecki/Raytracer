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
                new Vector3(0.0f, 0.0f, -5.0f), 
                new Vector3(3.0f, 3.0f, -5.0f), 
                new Vector3(0.0f, 3.0f, -5.0f),
                new Vector3(3.0f, -3.0f, -5.0f),
                new Vector3(0.25f, -0.5f, -5.0f),
                new Vector3(0.45f, 0.45f, -5.0f)  
            };
            Triangles = new[] {0, 1, 2};
            TriangleCount = Triangles.Length/3;
        }
    }
}