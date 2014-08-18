using System.Threading;
using Raytracer.Debugging;

namespace Raytracer
{
    internal class Triangle
    {
        public Vector3 Normal;
        private Vector3 _edge1;
        private Vector3 _edge2;
        private Mesh _mesh;
        public int V1Index;
        public int V2Index;
        public int V3Index;
        private int UV1Index;
        private int UV2Index;
        private int UV3Index;

        public Triangle(int v1Index, int v2Index, int v3Index)
        {
            V1Index = v1Index;
            V2Index = v2Index;
            V3Index = v3Index;
        }

        public Triangle(int v1Index, int v2Index, int v3Index,
                        int uv1Index, int uv2Index, int uv3Index)
        {
            V1Index = v1Index;
            V2Index = v2Index;
            V3Index = v3Index;

            UV1Index = uv1Index;
            UV2Index = uv2Index;
            UV3Index = uv3Index;
        }

        public void Init(Mesh mesh)
        {
            _mesh = mesh;
            _edge1 = V2 - V1;
            _edge2 = V3 - V1;
            Vector3 crossProduct = Vector3.Cross(_edge2, _edge1);
            Normal = crossProduct.Normalized();
        }

        public Vector3 V1
        {
            get { return _mesh.Vertices[V1Index]; }
        }

        public Vector3 V2
        {
            get { return _mesh.Vertices[V2Index]; }
        }

        public Vector3 V3
        {
            get { return _mesh.Vertices[V3Index]; }
        }

        private Vector2 Uv1
        {
            get { return _mesh.UVs[UV1Index]; }
        }

        private Vector2 Uv2
        {
            get { return _mesh.UVs[UV2Index]; }
        }

        private Vector2 Uv3
        {
            get { return _mesh.UVs[UV3Index]; }
        }

        public Vector2 UVCoordinates(float u, float v)
        {
            return Uv1 * (1 - u - v) + Uv2 * u + Uv3 * v;
        }

        public bool RayCast(Ray ray, ref RayTriangleHit hitInfo)
        {
#if DEBUG
            Interlocked.Increment(ref Counters.RayTriangleTests);
#endif
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
            Vector3 v1Normal = _mesh.VertexNormals[V1Index];
            Vector3 v2Normal = _mesh.VertexNormals[V2Index];
            Vector3 v3Normal = _mesh.VertexNormals[V3Index];
            Vector3 normal = v1Normal * (1 - u - v) + v2Normal * u + v3Normal * v;
            return normal;
        }
    }
}