using System.Drawing;
using System.Threading;
using Raytracer.Debugging;

namespace Raytracer
{
    class Triangle
    {
        public Mesh Mesh;
        public int V1Index;
        public int V2Index;
        public int V3Index;
        public bool IsSmooth = false;
        private Vector3 _edge1;
        private Vector3 _edge2;
        public Color Color;
        public Vector3 Normal;

        public Triangle(Mesh mesh, int v1Index, int v2Index, int v3Index, Color color, bool smooth = false)
        {
            Mesh = mesh;
            V1Index = v1Index;
            V2Index = v2Index;
            V3Index = v3Index;
            IsSmooth = smooth;
            Color = color;
            _edge1 = V2 - V1;
            _edge2 = V3 - V1;
            Vector3 crossProduct = Vector3.Cross(_edge2, _edge1);
            Normal = crossProduct.Normalized();
        }

        public bool RayCast(Ray ray, out RaycastHit hitInfo)
        {
#if DEBUG
            Interlocked.Increment(ref Counters.RayTriangleTests);
#endif
            hitInfo = new RaycastHit();
            if (Vector3.Dot(Normal, ray.Direction) > 0)
            {
#if DEBUG
                Interlocked.Increment(ref Counters.BackfaceCulls);
#endif
                return false;
            }
            Vector3 pVec = Vector3.Cross(ray.Direction, _edge2);
            float determinant = Vector3.Dot(_edge1, pVec);
            // ReSharper disable once CompareOfFloatsByEqualityOperator
            if (determinant == 0) return false;
            float invDeterminant = 1 / determinant;
            Vector3 tVec = ray.Origin - V1;
            hitInfo.U = Vector3.Dot(tVec, pVec) * invDeterminant;
            if (hitInfo.U < 0 || hitInfo.U > 1) return false;
            Vector3 qVec = Vector3.Cross(tVec, _edge1);
            hitInfo.V = Vector3.Dot(ray.Direction, qVec) * invDeterminant;
            if (hitInfo.V < 0 || hitInfo.V + hitInfo.U > 1) return false;
            hitInfo.Distance = Vector3.Dot(_edge2, qVec) * invDeterminant;
#if DEBUG
            if (hitInfo.Distance < 0.0001f) return false;
            Interlocked.Increment(ref Counters.RayHits);
            return true;
#else
            return hitInfo.Distance > 0;
#endif
        }

        public Vector3 SurfaceNormal(float u, float v)
        {
            if (!IsSmooth) return Normal;
            Vector3 v1Normal = Mesh.VertexNormals[V1Index];
            Vector3 v2Normal = Mesh.VertexNormals[V2Index];
            Vector3 v3Normal = Mesh.VertexNormals[V3Index];
            Vector3 normal = v1Normal * (1 - u - v) + v2Normal * u + v3Normal * v;
            return normal;
        }

        public Vector3 V1
        {
            get { return Mesh.Vertices[V1Index]; }
        }

        public Vector3 V2
        {
            get { return Mesh.Vertices[V2Index]; }
        }

        public Vector3 V3
        {
            get { return Mesh.Vertices[V3Index]; }
        }

    }
}
