namespace Raytracer
{
    internal class RaycastHit
    {
        public float Distance;
        public Mesh Mesh;
        public Ray Ray;
        public Triangle Triangle;
        public float U;
        public float V;
        private Vector3 _position;
        private bool _positionNotCalculated = true;

        public Vector3 Position
        {
            get
            {
                if (!_positionNotCalculated) return _position;
                Vector3 v1 = Triangle.V1;
                Vector3 v2 = Triangle.V2;
                Vector3 v3 = Triangle.V3;
                _position = v1 * (1 - U - V) + U * v2 + V * v3;
                _positionNotCalculated = false;
                return _position + Mesh.Position;
            }
        }
    }
}