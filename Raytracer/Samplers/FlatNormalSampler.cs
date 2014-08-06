namespace Raytracer.Samplers
{
    class FlatNormalSampler : TextureSampler
    {
        public override Vector3 Sample(RaycastHit hitInfo)
        {
            return hitInfo.Triangle.Normal;
        }
    }
}
