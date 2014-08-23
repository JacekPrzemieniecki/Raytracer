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
        private Octree _octree;

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
            var localRay = new Ray(ray.Origin - Position, ray.Direction);
            return _octree.Raycast(localRay, maxDistance, ref hitInfo);
        }

        public Vector3 SampleColor(Scene scene, RaycastHit raycastHit, int maxRecursiveRaycasts)
        {
            return Shader.Shade(scene, raycastHit, maxRecursiveRaycasts);
        }

        protected void Init()
        {
            BindTriangles();
            CalculateVertexNormals();
            _octree = new Octree(Triangles, this);
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
    }
}