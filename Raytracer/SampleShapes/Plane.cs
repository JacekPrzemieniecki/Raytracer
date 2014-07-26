using System.Drawing;
using Raytracer.Shaders;

namespace Raytracer.SampleShapes
{
    class Plane : Mesh
    {
        private Color _color = Color.Gray;
        public Plane(Vector3 position, Vector3 left, Vector3 forward, float edge, Shader shader)
        {
            Position = position;
            Shader = shader;
            float halfEdge = edge / 2;
            Vertices = new[]
            {
                (forward - left) * halfEdge,
                (forward + left) * halfEdge,
                (-1 * forward - left) * halfEdge,
                (left - forward) * halfEdge
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
