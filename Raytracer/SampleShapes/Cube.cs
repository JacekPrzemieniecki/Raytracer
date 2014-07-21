namespace Raytracer.SampleShapes
{
    class Cube : Mesh
    {
        public Cube(Vector3 position, float edge)
        {
            Position = position;
            float h = edge / 2;
            Vertices = new[]
            {
                new Vector3(-h, h, -h),
                new Vector3(h, h, -h),
                new Vector3(h, h, h),
                new Vector3(-h, h, h),
                new Vector3(-h, -h, -h),
                new Vector3(h, -h, -h),
                new Vector3(h, -h, h),
                new Vector3(-h, -h, h)       
            };
            Triangles = new[]
            {
                0, 1, 4,
                1, 2, 5,
                2, 3, 6,
                3, 0, 7,
                4, 1, 5,
                5, 2, 6,
                6, 3, 7,
                7, 0, 4
            };
            TriangleCount = Triangles.Length / 3;
            Init();
        }
    }
}
