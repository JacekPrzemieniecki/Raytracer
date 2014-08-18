namespace Raytracer
{
    internal class RaycastHit
    {
        public Mesh Mesh;
        public Ray Ray;
        public Triangle Triangle;

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
        private RayTriangleHit _rayTriangleHit;
        private Vector3 _position;
        private bool _positionCalculated;

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

        public RaycastHit(RayTriangleHit rayTriangleHit, Triangle triangle, Mesh mesh, Ray ray)
        {
            _rayTriangleHit = rayTriangleHit;
            Triangle = triangle;
            Mesh = mesh;
            Ray = ray;
            _position = null;
            _positionCalculated = false;

        }
    }
}