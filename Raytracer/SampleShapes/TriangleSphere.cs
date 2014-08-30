using System;
using Raytracer.Samplers;
using Raytracer.Shaders;

namespace Raytracer.SampleShapes
{
    internal static class TriangleSphere
    {
        /// <summary>
        ///     Creates triangle approximation of a sphere
        /// </summary>
        /// <param name="position">Center of the sphere</param>
        /// <param name="radius">Radius of the sphere</param>
        /// <param name="rings">Nummber of horizontal vertex rings approximating the sphere. Poles not included</param>
        /// <param name="segments">Number of segments per ring</param>
        /// <param name="shader">Shader to use for the mesh</param>
        /// <param name="normalSampler">Normal sampler to use for the mesh</param>
        public static Mesh Create(Vector3 position, double radius, int rings, int segments, Shader shader,
            ISampler normalSampler)
        {
            Vector3[] vertices = BuildVertices(radius, segments, rings);
            Triangle[] triangles = BuildTriangles(segments, rings);

            return new Mesh(vertices, triangles, position, shader, normalSampler);
        }

        private static Vector3[] BuildVertices(double radius, int segments, int rings)
        {
            int vertexCount = 2 + rings * segments;
            var vertices = new Vector3[vertexCount];
            int lastVertex = vertexCount - 1;

            vertices[0] = new Vector3(0, (float) radius, 0);
            vertices[lastVertex] = new Vector3(0, (float) -radius, 0);
            double verticalAngleStep = Math.PI / (rings + 1);
            double horizontalAngleStep = 2 * Math.PI / segments;
            const double piHalf = Math.PI / 2;
            for (int ring = 1; ring <= rings; ring++)
            {
                double verticalAngle = piHalf - verticalAngleStep * ring;
                double y = radius * Math.Sin(verticalAngle);
                double cosVerticalAngle = Math.Cos(verticalAngle);
                for (int vertex = 0; vertex < segments; vertex++)
                {
                    double horizontalAngle = horizontalAngleStep * vertex;
                    double x = radius * Math.Sin(horizontalAngle) * cosVerticalAngle;
                    double z = radius * Math.Cos(horizontalAngle) * cosVerticalAngle;
                    int vertIndex = (ring - 1) * segments + vertex + 1;
                    vertices[vertIndex] = new Vector3((float) x, (float) y, (float) z);
                }
            }
            return vertices;
        }

        private static Triangle[] BuildTriangles(int segments, int rings)
        {
            var triangles = new Triangle[segments * rings * 2];
            int lastVertex = rings * segments + 1;
            int lastTriangleIndex = triangles.Length - 1;
            for (int triangle = 0; triangle < segments; triangle++)
            {
                int edgeFirstVertex = triangle + 1;
                triangles[triangle] = new Triangle(0, edgeFirstVertex + 1, edgeFirstVertex);

                // South pole
                int southEdgeFirstVertex = lastVertex - edgeFirstVertex;
                triangles[lastTriangleIndex - triangle] = new Triangle(southEdgeFirstVertex - 1,
                    southEdgeFirstVertex,
                    lastVertex);
            }
            // Connect last and first vertex on the ring
            triangles[segments - 1].V2Index = 1;
            triangles[lastTriangleIndex - segments + 1].V2Index = lastVertex - 1;

            // Build rings
            int triangleIndex = segments;
            for (int ring = 0; ring < rings - 1; ring++)
            {
                int ringStart = ring * segments + 1;
                for (int vertex = 0; vertex < segments; vertex++)
                {
                    int v1 = ringStart + vertex;
                    int v2 = ringStart + (vertex + 1) % segments;
                    int v3 = v1 + segments;
                    int v4 = v2 + segments;
                    triangles[triangleIndex] = new Triangle(v1, v2, v3);
                    triangles[triangleIndex + 1] = new Triangle(v3, v2, v4);
                    triangleIndex += 2;
                }
            }
            return triangles;
        }
    }
}