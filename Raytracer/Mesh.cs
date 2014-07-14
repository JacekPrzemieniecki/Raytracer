namespace Raytracer
{
    internal class Mesh
    {
        public int[] Triangles;
        public Vector3[] Vertices;

        public Mesh()
        {
            Vertices = new[]
            {new Vector3(50.0f, 300.0f, 1.0f), new Vector3(100.0f, 0.0f, 1.0f), new Vector3(0.0f, 150.0f, 1.0f)};
            Triangles = new[] {0, 1, 2};
        }
    }
}