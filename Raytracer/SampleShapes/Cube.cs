using Raytracer.Samplers;
using Raytracer.Shaders;

namespace Raytracer.SampleShapes
{
    internal static class Cube
    {
        public static Mesh Create(Vector3 position, Quaternion rotation, float edge, Shader shader, ISampler normalSampler)
        {
            float h = edge / 2;
            var vertices = new[]
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
            var triangles = new[]
            {
                new Triangle(0, 1, 2),
                new Triangle(2, 3, 0),
                new Triangle(0, 3, 7),
                new Triangle(0, 7, 4),
                new Triangle(1, 0, 4),
                new Triangle(1, 4, 5),
                new Triangle(2, 1, 5),
                new Triangle(2, 5, 6),
                new Triangle(3, 2, 6),
                new Triangle(3, 6, 7),
                new Triangle(4, 6, 5),
                new Triangle(4, 7, 6)
            };

            return new Mesh(vertices, triangles, position, rotation, shader, normalSampler);
        }
    }
}