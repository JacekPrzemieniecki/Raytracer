namespace Raytracer
{
    internal struct Ray
    {
        public Vector3 Direction;
        public Vector3 Origin;

        public Ray(Vector3 origin, Vector3 direction)
        {
            Origin = origin;
            Direction = direction.Normalized();
        }
    }
}