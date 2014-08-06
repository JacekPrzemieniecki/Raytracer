using System.Drawing;
using Raytracer.Shaders;

namespace Raytracer.SampleShapes
{
    internal class Cube : Mesh
    {
        private Color _color = Color.White;
        public Cube(Vector3 position, float edge, Shader shader)
        {
            Position = position;
            Shader = shader;
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
                new Triangle(this, 0, 1, 2),
                new Triangle(this, 2, 3, 0),
                new Triangle(this, 0, 3, 7),
                new Triangle(this, 0, 7, 4),
                new Triangle(this, 1, 0, 4),
                new Triangle(this, 1, 4, 5),
                new Triangle(this, 2, 1, 5),
                new Triangle(this, 2, 5, 6),
                new Triangle(this, 3, 2, 6),
                new Triangle(this, 3, 6, 7),
                new Triangle(this, 4, 6, 5),
                new Triangle(this, 4, 7, 6)
            };
            Init();
        }
    }
}