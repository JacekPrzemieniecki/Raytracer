using System.Collections.Generic;

namespace Raytracer
{
    class Octree
    {
        private const int TriangleCountInLeaf = 10;
        private List<Triangle> _directTriangleChildren;
        private readonly Octree[] _children;
        private readonly Mesh _parentMesh;
        private readonly Box _boundingBox;
        private readonly Triangle[] _triangles;

        public Octree(Triangle[] triangles, Mesh parentMesh)
        {
            _parentMesh = parentMesh;
            _children = new Octree[8];
            _triangles = triangles;
            Vector3 max;
            Vector3 min;
            FindEdges(out min, out max);
            _boundingBox = new Box(min.x, max.x, min.y, max.y, min.z, max.z);
            if (triangles.Length > TriangleCountInLeaf)
            {
                Subdivide(min, max);
            }
            else
            {
                _directTriangleChildren = new List<Triangle>(triangles);
            }
        }

        public bool Raycast(Ray ray, ref RayTriangleHit closestRayTriangleHitInfo,
            ref Triangle closestTriangle)
        {
            if (!_boundingBox.Raycast(ray, closestRayTriangleHitInfo.Distance)) return false;
            bool hitFound = false;
            var hitInfo = new RayTriangleHit();
            foreach (Triangle triangle in _directTriangleChildren)
            {
                if (triangle.RayCast(ray, ref hitInfo) && (closestRayTriangleHitInfo.Distance > hitInfo.Distance))
                {
                    closestRayTriangleHitInfo = hitInfo;
                    closestTriangle = triangle;
                    hitFound = true;
                }
            }

            for (int i = 0; i < 8; i++)
            {
                if (_children[i] != null)
                {
                    bool childHit = _children[i].Raycast(ray, ref closestRayTriangleHitInfo, ref closestTriangle);
                    hitFound |= childHit;
                }
            }
            return hitFound;
        }

        private void FindEdges(out Vector3 mins, out Vector3 maxes)
        {
            Vector3 firstVertex = _triangles[0].V1;
            float minX = firstVertex.x;
            float maxX = firstVertex.x;
            float minY = firstVertex.y;
            float maxY = firstVertex.y;
            float minZ = firstVertex.z;
            float maxZ = firstVertex.z;
            foreach (var triangle in _triangles)
            {
                for (int i = 0; i < 3; i++)
                {
                    Vector3 v = triangle[i];
                    if (v.x < minX) minX = v.x;
                    if (v.x > maxX) maxX = v.x;
                    if (v.y < minY) minY = v.y;
                    if (v.y > maxY) maxY = v.y;
                    if (v.z < minZ) minZ = v.z;
                    if (v.z > maxZ) maxZ = v.z;
                }
            }
            mins = new Vector3(minX, minY, minZ);
            maxes = new Vector3(maxX, maxY, maxZ);
        }

        private void Subdivide(Vector3 mins, Vector3 maxes)
        {
            float avgX = (mins.x + maxes.x) / 2;
            float avgY = (mins.y + maxes.y) / 2;
            float avgZ = (mins.z + maxes.z) / 2;

            var subnodes = new List<Triangle>[8];
            for (int i = 0; i < 8; i++)
            {
                subnodes[i] = new List<Triangle>();
            }
            _directTriangleChildren = new List<Triangle>();

            foreach (var triangle in _triangles)
            {
                int subnodeIndex = AssignSubnode(triangle, avgX, avgY, avgZ);
                if (subnodeIndex == -1)
                {
                    _directTriangleChildren.Add(triangle);
                }
                else
                {
                    subnodes[subnodeIndex].Add(triangle);
                }
            }

            for (int i = 0; i < 8; i++)
            {
                if (subnodes[i].Count == 0) continue;
                _children[i] = new Octree(subnodes[i].ToArray(), _parentMesh);
            }
        }

        private static int AssignSubnode(Triangle triangle, float avgX, float avgY, float avgZ)
        {
            int subNodeIndex = 0;
            if (triangle.V1.x > avgX &&
                triangle.V2.x > avgX &&
                triangle.V3.x > avgX)
            {
                subNodeIndex += 1;
            }
            else if (triangle.V1.x > avgX ||
                     triangle.V2.x > avgX ||
                     triangle.V3.x > avgX)
            {
                return -1;
            }

            if (triangle.V1.y > avgY &&
                triangle.V2.y > avgY &&
                triangle.V3.y > avgY)
            {
                subNodeIndex += 2;
            }
            else if (triangle.V1.y > avgY ||
                     triangle.V2.y > avgY ||
                     triangle.V3.y > avgY)
            {
                return -1;
            }

            if (triangle.V1.z > avgZ &&
                triangle.V2.z > avgZ &&
                triangle.V3.z > avgZ)
            {
                subNodeIndex += 4;
            }
            else if (triangle.V1.z > avgZ ||
                     triangle.V2.z > avgZ ||
                     triangle.V3.z > avgZ)
            {
                return -1;
            }

            return subNodeIndex;
        }
    }
}
