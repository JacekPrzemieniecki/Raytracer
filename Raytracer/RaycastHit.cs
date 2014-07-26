namespace Raytracer
{
    internal class RaycastHit
    {
        public float U;
        public float V;
        public float Distance;
        public Ray Ray;
        public Mesh Mesh;
        public int TriangleId;
        private Vector3 _position;
        private bool _positionNotCalculated = true;

        public Vector3 Position
        {
            get
            {
                if (!_positionNotCalculated) return _position;
                int triangleOffset = TriangleId * 3;
                int v1Index = Mesh.Triangles[triangleOffset];
                int v2Index = Mesh.Triangles[triangleOffset + 1];
                int v3Index = Mesh.Triangles[triangleOffset + 2];
                Vector3 v1 = Mesh.Vertices[v1Index];
                Vector3 v2 = Mesh.Vertices[v2Index];
                Vector3 v3 = Mesh.Vertices[v3Index];
                _position = v1 * (1 - U - V) + U * v2 + V * v3;
                _positionNotCalculated = false;
                return _position;
            }
        }
    }
}