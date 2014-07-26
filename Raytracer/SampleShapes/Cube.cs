using Raytracer.Shaders;

namespace Raytracer.SampleShapes
{
    class Cube : Mesh
    {
        public Cube(Vector3 position, float edge, Shader shader)
        {
            Position = position;
            Shader = shader;
            IsSmoothShaded = false;
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
                0, 1, 2,
                2, 3, 0,
                0, 3, 7,
                0, 7, 4,
                1, 0, 4,
                1, 4, 5,
                2, 1, 5,
                2, 5, 6,
                3, 2, 6,
                3, 6, 7,
                4, 6, 5,
                4, 7, 6
            };
            Init();
        }
    }
}
