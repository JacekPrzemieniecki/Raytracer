using System.Drawing;
using System.Threading;

namespace Raytracer
{
    internal class Mesh : IRaycastable
    {
        public int[] Triangles;
        public Vector3[] Vertices;
        public int TriangleCount { get; protected set; }
        public Vector3 Position { get; set; }
        private Box _boundingBox;
        private bool _boundingBoxDirty = true;
        public Box BoundingBox {
            get
            {
                if (_boundingBoxDirty)
                {
                    CalculateBoundingBox();
                }
                return _boundingBox;
            }
            protected set { _boundingBox = value; } }

        public Mesh(Vector3[] vertices, int[] triangles, Vector3 position)
        {
            Vertices = vertices;
            Triangles = triangles;
            TriangleCount = Triangles.Length / 3;
            Position = position;
            Init();
        }

        protected Mesh()
        {
        }

        protected void Init()
        {
            CalculateBoundingBox();
        }

        private void CalculateBoundingBox()
        {
            Vector3 firstVertex = Vertices[0];
            _boundingBox = new Box(firstVertex.x, firstVertex.x, firstVertex.y, firstVertex.y, firstVertex.z, firstVertex.z);
            foreach (var vertex in Vertices)
            {
                if (_boundingBox.MaxX < vertex.x)
                {
                    _boundingBox.MaxX = vertex.x;
                }
                if (_boundingBox.MinX > vertex.x)
                {
                    _boundingBox.MinX = vertex.x;
                }
                if (_boundingBox.MaxY < vertex.y)
                {
                    _boundingBox.MaxY = vertex.y;
                }
                if (_boundingBox.MinY > vertex.y)
                {
                    _boundingBox.MinY = vertex.y;
                }
                if (_boundingBox.MaxZ < vertex.z)
                {
                    _boundingBox.MaxZ = vertex.z;
                }
                if (_boundingBox.MinZ > vertex.z)
                {
                    _boundingBox.MinZ = vertex.z;
                }
            }
            _boundingBox.MaxX += Position.x;
            _boundingBox.MinX += Position.x;
            _boundingBox.MaxY += Position.y;
            _boundingBox.MinY += Position.y;
            _boundingBox.MaxZ += Position.z;
            _boundingBox.MinZ += Position.z;

            _boundingBoxDirty = false;
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
            #if DEBUG
            Interlocked.Increment(ref Debugging.Counters.RaysCast);
            #endif
            Ray localRay = new Ray(ray.Origin - Position, ray.Direction);
            var closestHit = new RaycastHit { t = maxDistance };
            for (int i = 0; i < TriangleCount; i++)
            {
                RaycastHit hitInfo;
                if (RaycastTriangle(i, localRay, out hitInfo) && (closestHit.t > hitInfo.t))
                {
                    closestHit = hitInfo;
                }
            }
            if (closestHit.t < maxDistance)
            {
                color = Color.Red;
            }
            return closestHit.t;
        }

        private bool RaycastTriangle(int triangleId, Ray ray, out RaycastHit hitInfo)
        {
            #if DEBUG
            Interlocked.Increment(ref Debugging.Counters.RayTriangleTests);
            #endif
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
            #if DEBUG
            if (hitInfo.t > 0)
            {
                Interlocked.Increment(ref Debugging.Counters.RayHits);
                return true;
            }
            return false;
            #else
            return hitInfo.t > 0;
            #endif
        }
    }
}