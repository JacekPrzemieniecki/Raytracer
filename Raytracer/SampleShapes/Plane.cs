using System.Drawing;
using Raytracer.Shaders;

namespace Raytracer.SampleShapes
{
    internal class Plane : Mesh
    {
        private readonly Color _color = Color.Gray;

        public Plane(Vector3 position, Vector3 right, Vector3 forward, float edge, Shader shader)
        {
            Position = position;
            Shader = shader;
            float halfEdge = edge / 2;
            Vertices = new[]
            {
                (forward + right) * halfEdge,
                (forward - right) * halfEdge,
                (right - forward) * halfEdge,
                (-1 * forward - right) * halfEdge
            };
            Triangles = new[]
            {
                new Triangle(this, 0, 1, 2, _color),
                new Triangle(this, 1, 3, 2, _color)
            };
            Init();
        }
    }
}