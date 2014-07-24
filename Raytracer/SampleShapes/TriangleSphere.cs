using System;

namespace Raytracer.SampleShapes
{
    class TriangleSphere : Mesh
    {
        /// <summary>
        /// Triangle approximation of a sphere
        /// </summary>
        /// <param name="position">Center of the sphere</param>
        /// <param name="radius">Radius of the sphere</param>
        /// <param name="rings">Nummber of horizontal vertex rings approximating the sphere. Poles not included</param>
        /// <param name="segments">Number of segments per ring</param>
        public TriangleSphere(Vector3 position, double radius, int rings, int segments, Shader shader)
        {
            Position = position;
            Shader = shader;
            int polygonRings = rings - 1;
            TriangleCount = segments * rings * 2;
            int vertexCount = 2 + rings*segments;
            Vertices = new Vector3[vertexCount];
            int lastVertex = vertexCount - 1;
            int lastTriangleIndex = TriangleCount * 3 - 1;
            Triangles = new int[3 * TriangleCount];

            // build poles
            Vertices[0] = new Vector3(0, (float)radius, 0);
            Vertices[lastVertex] = new Vector3(0, (float) -radius, 0);

            BuildVertices(radius, segments, rings, segments);

            // Build pole triangles - connect pole with each edge on the first/last ring
            for (int triangle = 0; triangle < segments; triangle++)
            {
                int vertIndex = triangle * 3;
                Triangles[vertIndex] = 0;

                // North pole
                int edgeFirstVertex = triangle + 1;
                Triangles[vertIndex + 1] = edgeFirstVertex;
                Triangles[vertIndex + 2] = edgeFirstVertex + 1;

                // South pole
                Triangles[lastTriangleIndex - vertIndex] = lastVertex;
                Triangles[lastTriangleIndex - vertIndex - 1] = lastVertex - edgeFirstVertex - 1;
                Triangles[lastTriangleIndex - vertIndex - 2] = lastVertex - edgeFirstVertex;
            }
            // Connect last and first vertex on the ring
            Triangles[3 * segments - 1] = 1;
            Triangles[lastTriangleIndex - 3 * segments + 2] = lastVertex - 1;

            // Build rings
            int triangleIndex = segments;
            for (int ring = 0; ring < polygonRings; ring++)
            {
                int ringStart = ring*segments + 1;
                for (int vertex = 0; vertex < segments; vertex++)
                {
                    int v1 = ringStart + vertex;
                    int v2 = ringStart + (vertex + 1) % segments;
                    int v3 = v1 + segments;
                    int v4 = v2 + segments;
                    ConnectQuad(v1, v2, v3, v4, triangleIndex);
                    triangleIndex += 2;
                }
                
            }
            Init();
        }

        private void BuildVertices(double radius, int segments, int polygonRings, int verticesPerRing)
        {
            double verticalAngleStep = Math.PI / polygonRings;
            double horizontalAngleStep = 2*Math.PI / segments;
            const double piHalf = Math.PI / 2;
            for (int ring = 1; ring <= polygonRings; ring++)
            {
                double verticalAngle = piHalf - verticalAngleStep*ring;
                double y = radius * Math.Sin(verticalAngle);
                double cosVerticalAngle = Math.Cos(verticalAngle);
                for (int vertex = 0; vertex < verticesPerRing; vertex++)
                {
                    double horizontalAngle = horizontalAngleStep * vertex;
                    double x = radius * Math.Sin(horizontalAngle) * cosVerticalAngle;
                    double z = radius * Math.Cos(horizontalAngle) * cosVerticalAngle;
                    int vertIndex = (ring - 1)*verticesPerRing + vertex + 1;
                    Vertices[vertIndex] = new Vector3((float)x, (float)y, (float)z);
                }
            }
        }

        private void ConnectQuad(int v1, int v2, int v3, int v4, int triangleIndex)
        {
            int vertIndex = triangleIndex * 3;
            Triangles[vertIndex] = v1;
            Triangles[vertIndex + 1] = v2;
            Triangles[vertIndex + 2] = v3;
            Triangles[vertIndex + 3] = v2;
            Triangles[vertIndex + 4] = v4;
            Triangles[vertIndex + 5] = v3;
        }
    }
}
