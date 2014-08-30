namespace Raytracer.Samplers
{
    interface ISampler
    {
        Vector3 Sample(RaycastHit hitInfo);
    }
}
