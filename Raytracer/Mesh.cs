using System.Drawing;
using System.Threading;
using Raytracer.Debugging;
using Raytracer.Shaders;

namespace Raytracer
{
    internal class Mesh
    {
        public int[] Triangles;
        public Vector3[] Vertices;
        private Box _boundingBox;
        private bool _boundingBoxDirty = true;
        private Color _color = Color.Red;
        private Vector3[] _triangleNormals;
        private Vector3[] _vertexNormals;
        public bool IsSmoothShaded { get; set; }
        protected Shader Shader { get; set; }

        public Mesh(Vector3[] vertices, int[] triangles, Vector3 position, Shader shader, bool smooth)
        {
            Vertices = vertices;
            Triangles = triangles;
            TriangleCount = Triangles.Length / 3;
            Shader = shader;
            Position = position;
            IsSmoothShaded = smooth;
            Init();
        }

        protected Mesh()
        {
        }

        public int TriangleCount { get; protected set; }
        public Vector3 Position { get; set; }

        public Box BoundingBox
        {
            get
            {
                if (_boundingBoxDirty)
                {
                    CalculateBoundingBox();
                }
                return _boundingBox;
            }
        }

        /// <summary>
        ///     Casts a ray against mesh
        /// </summary>
        /// <param name="scene">Scene context to raycast in</param>
        /// <param name="ray">Ray to be cast</param>
        /// <param name="maxDistance">Maximum distance to trace ray</param>
        /// <returns>Distance along the ray the hit was found</returns>
        public RaycastHit Raycast(Scene scene, Ray ray, float maxDistance)
        {
#if DEBUG
            Interlocked.Increment(ref Counters.RaysCast);
#endif
            var localRay = new Ray(ray.Origin - Position, ray.Direction);
            var closestHit = new RaycastHit { Distance = maxDistance };
            for (int i = 0; i < TriangleCount; i++)
            {
                RaycastHit hitInfo;
                if (RaycastTriangle(i, localRay, out hitInfo) && (closestHit.Distance > hitInfo.Distance))
                {
                    closestHit = hitInfo;
                    closestHit.TriangleId = i;
                }
            }
            closestHit.Mesh = this;
            closestHit.Ray = ray;
            return closestHit;
        }

        public Color SampleColor(Scene scene, RaycastHit raycastHit)
        {
            return Shader.Shade(scene, this, raycastHit);
        }

        public Color GetDiffuseColor(RaycastHit hit)
        {
            return _color;
        }

        private Vector3 InterpolatedSurfaceNormal(RaycastHit hit)
        {
            int triangleOffset = hit.TriangleId * 3;
            Vector3 v1Normal = _vertexNormals[Triangles[triangleOffset]];
            Vector3 v2Normal = _vertexNormals[Triangles[triangleOffset + 1]];
            Vector3 v3Normal = _vertexNormals[Triangles[triangleOffset + 2]];
            Vector3 normal = v1Normal * (1 - hit.U - hit.V) + v2Normal * hit.U + v3Normal * hit.V;
            return normal;
        }

        private Vector3 FlatSurfaceNormal(int triangleId)
        {
            int triangleOffset = triangleId * 3;
            Vector3 v1 = Vertices[Triangles[triangleOffset]];
            Vector3 v2 = Vertices[Triangles[triangleOffset + 1]];
            Vector3 v3 = Vertices[Triangles[triangleOffset + 2]];
            Vector3 edge1 = v3 - v1;
            Vector3 edge2 = v2 - v1;
            Vector3 crossProduct = Vector3.Cross(edge1, edge2);
            Vector3 normal = crossProduct.Normalized();
            return normal;
        }

        public Vector3 SurfaceNormal(RaycastHit hit)
        {
            return IsSmoothShaded ? InterpolatedSurfaceNormal(hit) : _triangleNormals[hit.TriangleId];
        }

        protected void Init()
        {
            TriangleCount = Triangles.Length / 3;
            CalculateBoundingBox();
            CalculateTriangleNormals();
            CalculateVertexNormals();
        }

        private void CalculateTriangleNormals()
        {
            _triangleNormals = new Vector3[TriangleCount];
            for (int i = 0; i < TriangleCount; i++)
            {
                _triangleNormals[i] = FlatSurfaceNormal(i);
            }
        }

        private void CalculateVertexNormals()
        {
            _vertexNormals = new Vector3[Vertices.Length];
            for (int i = 0; i < Vertices.Length; i++)
            {
                _vertexNormals[i] = Vector3.Zero;
            }

            for (int triangleId = 0; triangleId < TriangleCount; triangleId++)
            {
                int vertexOffset = 3 * triangleId;
                _vertexNormals[Triangles[vertexOffset]] += _triangleNormals[triangleId];
                _vertexNormals[Triangles[vertexOffset + 1]] += _triangleNormals[triangleId];
                _vertexNormals[Triangles[vertexOffset + 2]] += _triangleNormals[triangleId];

            }

            for (int i = 0; i < Vertices.Length; i++)
            {
                _vertexNormals[i] = _vertexNormals[i].Normalized();
            }

        }

        private void CalculateBoundingBox()
        {
            Vector3 firstVertex = Vertices[0];
            _boundingBox = new Box(
                firstVertex.x, firstVertex.x,
                firstVertex.y, firstVertex.y,
                firstVertex.z, firstVertex.z);
            foreach (Vector3 vertex in Vertices)
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

        private bool RaycastTriangle(int triangleId, Ray ray, out RaycastHit hitInfo)
        {
#if DEBUG
            Interlocked.Increment(ref Counters.RayTriangleTests);
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
            hitInfo.U = Vector3.Dot(tVec, pVec) * invDeterminant;
            if (hitInfo.U < 0 || hitInfo.U > 1) return false;
            Vector3 qVec = Vector3.Cross(tVec, edge1);
            hitInfo.V = Vector3.Dot(ray.Direction, qVec) * invDeterminant;
            if (hitInfo.V < 0 || hitInfo.V + hitInfo.U > 1) return false;
            hitInfo.Distance = Vector3.Dot(edge2, qVec) * invDeterminant;
#if DEBUG
            if (hitInfo.Distance < 0) return false;
            Interlocked.Increment(ref Counters.RayHits);
            return true;
#else
            return hitInfo.Distance > 0;
#endif
        }
    }
}