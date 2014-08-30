namespace Raytracer.Samplers
{
    class FlatNormalSampler : ISampler
    {
        public Vector3 Sample(RaycastHit hitInfo)
        {
            return hitInfo.Triangle.Normal;
        }
    }
}
