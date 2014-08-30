namespace Raytracer.Samplers
{
    interface ISampler
    {
        Vector3 Sample(Triangle triangle, float u, float v);
    }
}
