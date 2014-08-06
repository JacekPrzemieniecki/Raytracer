namespace Raytracer.Samplers
{
    class TextureSampler
    {
        public virtual Vector3 Sample(RaycastHit hitInfo)
        {
            return new Vector3(0, 0, 0);
        }
    }
}
