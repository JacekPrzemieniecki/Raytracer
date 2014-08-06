namespace Raytracer
{
    public struct Ray
    {
        public readonly Vector3 Direction;
        public readonly Vector3 Origin;

        public Ray(Vector3 origin, Vector3 direction)
        {
            Origin = origin;
            Direction = direction.Normalized();
        }
    }
}