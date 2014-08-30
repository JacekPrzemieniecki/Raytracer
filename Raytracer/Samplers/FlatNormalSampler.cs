namespace Raytracer.Samplers
{
    class FlatNormalSampler : ISampler
    {
        public Vector3 Sample(Triangle triangle, float u, float v)
        {
            return triangle.Normal;
        }
    }
}
