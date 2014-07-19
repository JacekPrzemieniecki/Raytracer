﻿using System.Drawing;

namespace Raytracer
{
    internal class Mesh
    {
        public int[] Triangles;
        public Vector3[] Vertices;
        public int TriangleCount { get; protected set; }

        public Mesh(Vector3[] vertices, int[] triangles)
        {
            Vertices = vertices;
            Triangles = triangles;
            TriangleCount = Triangles.Length / 3;
        }

        protected Mesh()
        {
        }

        /// <summary>
        /// Casts a ray against mesh
        /// </summary>
        /// <param name="ray">Ray to be cast</param>
        /// <param name="color">Color of the point where ray hit</param>
        /// <param name="maxDistance">Maximum distance to trace ray</param>
        /// <returns>Distance along the ray the hit was found</returns>
        public float Raycast(Ray ray, ref Color color, float maxDistance)
        {
            var closestHit = new RaycastHit { t = maxDistance };
            for (int i = 0; i < TriangleCount; i++)
            {
                RaycastHit hitInfo;
                if (RaycastTriangle(i, ray, out hitInfo) && (closestHit.t > hitInfo.t))
                {
                    closestHit = hitInfo;
                }
            }
// ReSharper disable once CompareOfFloatsByEqualityOperator
            if (closestHit.t < maxDistance)
            {
                color = Color.Red;
            }
            return closestHit.t;
        }

        private bool RaycastTriangle(int triangleId, Ray ray, out RaycastHit hitInfo)
        {
            hitInfo = new RaycastHit();
            int triangleOffset = triangleId * 3;
            Vector3 vert0 = Vertices[Triangles[triangleOffset]];
            Vector3 vert1 = Vertices[Triangles[triangleOffset + 1]];
            Vector3 vert2 = Vertices[Triangles[triangleOffset + 2]];
            Vector3 edge1 = vert1 - vert0;
            Vector3 edge2 = vert2 - vert0;
            Vector3 pVec = Vector3.Cross(ray.Direction, edge2);
            float determinant = Vector3.Dot(edge1, pVec);
// ReSharper disable once CompareOfFloatsByEqualityOperator
            if (determinant == 0) return false;
            float invDeterminant = 1 / determinant;
            Vector3 tVec = ray.Origin - vert0;
            hitInfo.u = Vector3.Dot(tVec, pVec) * invDeterminant;
            if (hitInfo.u < 0 || hitInfo.u > 1) return false;
            Vector3 qVec = Vector3.Cross(tVec, edge1);
            hitInfo.v = Vector3.Dot(ray.Direction, qVec) * invDeterminant;
            if (hitInfo.v < 0 || hitInfo.v + hitInfo.u > 1) return false;
            hitInfo.t = Vector3.Dot(edge2, qVec) * invDeterminant;
            return hitInfo.t > 0;
        }
    }
}