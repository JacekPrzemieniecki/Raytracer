namespace Raytracer
{
    internal class RaycastHit
    {
        public readonly Mesh Mesh;
        public readonly Vector3 Normal;
        public readonly Ray Ray;
        public readonly Triangle Triangle;
        private Vector3 _position;
        private bool _positionCalculated;
        private RayTriangleHit _rayTriangleHit;

        public RaycastHit(RayTriangleHit rayTriangleHit, Triangle triangle, Mesh mesh, Ray ray, Vector3 normal)
        {
            _rayTriangleHit = rayTriangleHit;
            Triangle = triangle;
            Mesh = mesh;
            Ray = ray;
            _position = null;
            _positionCalculated = false;
            Normal = normal;
        }

        public float Distance
        {
            get { return _rayTriangleHit.Distance; }
        }

        public float U
        {
            get { return _rayTriangleHit.U; }
        }

        public float V
        {
            get { return _rayTriangleHit.V; }
        }

        public Vector3 Position
        {
            get
            {
                if (_positionCalculated) return _position;
                Vector3 v1 = Triangle.V1;
                Vector3 v2 = Triangle.V2;
                Vector3 v3 = Triangle.V3;
                _position = v1 * (1 - U - V) + U * v2 + V * v3 + Mesh.Position;
                _positionCalculated = true;
                return _position;
            }
        }
    }
}