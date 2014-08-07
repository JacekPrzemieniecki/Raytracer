namespace Raytracer.Samplers
{
    class InterpolatedNormalSampler : TextureSampler
    {
        public override Vector3 Sample(RaycastHit hitInfo)
        {
            return hitInfo.Triangle.SurfaceNormal(hitInfo.U, hitInfo.V);
        }
    }
}
