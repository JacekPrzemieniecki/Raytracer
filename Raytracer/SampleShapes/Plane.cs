using Raytracer.Shaders;

namespace Raytracer.SampleShapes
{
    internal static class Plane
    {
        public static Mesh Create(Vector3 position, Vector3 forward, Vector3 right, float edge, Shader shader)
        {
            float halfEdge = edge / 2;
            var vertices = new[]
            {
                (forward + right) * halfEdge,
                (forward - right) * halfEdge,
                (right - forward) * halfEdge,
                (-1 * forward - right) * halfEdge
            };
            var triangles = new[]
            {
                new Triangle(0, 1, 2),
                new Triangle(1, 3, 2)
            };

            return new Mesh(vertices, triangles, position, shader);
        }
    }
}