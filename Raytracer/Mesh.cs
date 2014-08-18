#if DEBUG
using System.Threading;
using Raytracer.Debugging;
#endif
using Raytracer.Shaders;

namespace Raytracer
{
    class Mesh
    {
        protected Triangle[] Triangles;
        public Vector3[] VertexNormals;
        public Vector3[] Vertices;
        public Vector2[] UVs;
        private Box _boundingBox;
        private bool _boundingBoxDirty = true;

        public Mesh(Vector3[] vertices, Triangle[] triangles, Vector3 position, Shader shader)
        {
            Vertices = vertices;
            Triangles = triangles;
            Shader = shader;
            Position = position;
            Init();
        }

        public Mesh(Vector3[] vertices, Triangle[] triangles, Vector2[] uvVertices, Vector3 position, Shader shader)
        {
            Vertices = vertices;
            Triangles = triangles;
            Shader = shader;
            Position = position;
            UVs = uvVertices;
            Init();
        }

        protected Mesh()
        {
        }

        protected Shader Shader { private get; set; }
        public Vector3 Position { get; protected set; }


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
        /// <param name="ray">Ray to be cast</param>
        /// <param name="maxDistance">Maximum distance to trace ray</param>
        /// <param name="hitInfo">Will contain hit information if true is returned</param>
        /// <returns>Distance along the ray the hit was found</returns>
        public bool Raycast(Ray ray, float maxDistance, ref RaycastHit hitInfo)
        {
#if DEBUG
            Interlocked.Increment(ref Counters.RaysCast);
#endif
            bool hitFound = false;
            var localRay = new Ray(ray.Origin - Position, ray.Direction);
            var closestHit = new RayTriangleHit {Distance = maxDistance};
            Triangle closestTriangle = null;
            var rayTriangleHitInfo = new RayTriangleHit();
            foreach (Triangle triangle in Triangles)
            {
                if (triangle.RayCast(localRay, ref rayTriangleHitInfo) && (closestHit.Distance > rayTriangleHitInfo.Distance))
                {
                    closestHit = rayTriangleHitInfo;
                    closestTriangle = triangle;
                    hitFound = true;
                }
            }
            if (hitFound)
            {
                hitInfo = new RaycastHit(closestHit, closestTriangle, this, ray);
            }
            return hitFound;
        }

        public Vector3 SampleColor(Scene scene, RaycastHit raycastHit, int maxRecursiveRaycasts)
        {
            return Shader.Shade(scene, raycastHit, maxRecursiveRaycasts);
        }

        protected void Init()
        {
            BindTriangles();
            CalculateBoundingBox();
            CalculateVertexNormals();
        }

        private void BindTriangles()
        {
            foreach (var triangle in Triangles)
            {
                triangle.Init(this);
            }
        }

        private void CalculateVertexNormals()
        {
            VertexNormals = new Vector3[Vertices.Length];
            for (int i = 0; i < Vertices.Length; i++)
            {
                VertexNormals[i] = Vector3.Zero;
            }

            foreach (Triangle triangle in Triangles)
            {
                VertexNormals[triangle.V1Index] += triangle.Normal;
                VertexNormals[triangle.V2Index] += triangle.Normal;
                VertexNormals[triangle.V3Index] += triangle.Normal;
            }

            for (int i = 0; i < Vertices.Length; i++)
            {
                VertexNormals[i] = VertexNormals[i].Normalized();
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
    }
}