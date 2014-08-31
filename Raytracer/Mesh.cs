using Raytracer.Samplers;
using Raytracer.Shaders;
#if DEBUG
using System.Threading;
using Raytracer.Debugging;
#endif

namespace Raytracer
{
    internal class Mesh
    {
        public readonly Vector2[] UVs;
        public readonly Vector3[] Vertices;
        private readonly ISampler _normalSampler;
        private readonly Triangle[] _triangles;
        public Vector3[] VertexNormals;
        private Octree _octree;

        public Mesh(Vector3[] vertices, Triangle[] triangles, Vector3 position, Quaternion rotation, Shader shader, ISampler normalSampler)
        {
            Vertices = vertices;
            _triangles = triangles;
            Shader = shader;
            Position = position;
            Rotation = rotation;
            _normalSampler = normalSampler;
            Init();
        }

        public Mesh(Vector3[] vertices, Triangle[] triangles, Vector2[] uvVertices, Vector3 position, Quaternion rotation, Shader shader,
            ISampler normalSampler)
        {
            Vertices = vertices;
            _triangles = triangles;
            Shader = shader;
            Position = position;
            Rotation = rotation;
            _normalSampler = normalSampler;
            UVs = uvVertices;
            Init();
        }

        private Shader Shader { get; set; }
        public Vector3 Position { get; set; }
        private Quaternion _rotation;
        private Quaternion _invRotation;

        public Quaternion Rotation
        {
            get
            {
                return _rotation;
            }
            set
            {
                _rotation = value;
                _invRotation = value.Inverse();
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
            var localRayDirection = ray.Direction.RotatedBy(_invRotation);
            var localRayOrigin = (ray.Origin - Position).RotatedBy(_invRotation);
            var localRay = new Ray(localRayOrigin, localRayDirection);
            var hit = new RayTriangleHit {Distance = maxDistance};
            Triangle closestTriangle = null;
            bool hitFound = _octree.Raycast(localRay, ref hit, ref closestTriangle);
            if (!hitFound) return false;
            Vector3 normal = _normalSampler.Sample(closestTriangle, hit.U, hit.V).RotatedBy(_rotation);
            hitInfo = new RaycastHit(hit, closestTriangle, this, ray, normal);
            return true;
        }

        public Vector3 SampleColor(Scene scene, RaycastHit raycastHit, int maxRecursiveRaycasts)
        {
            return Shader.Shade(scene, raycastHit, maxRecursiveRaycasts);
        }

        public void Rescale(float scale)
        {
            for (int i = 0; i < Vertices.Length; i++)
            {
                Vertices[i] = Vertices[i] * scale;
            }
            Init();
        }

        private void Init()
        {
            BindTriangles();
            CalculateVertexNormals();
            BuildOctree();
        }

        private void BindTriangles()
        {
            foreach (Triangle triangle in _triangles)
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

            foreach (Triangle triangle in _triangles)
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

        private void BuildOctree()
        {
            _octree = new Octree(_triangles, this);
        }
    }
}