namespace Raytracer
{
    public class Quaternion
    {
        public readonly Vector3 XYZ;

        public float X
        {
            get { return XYZ.x; }
        }

        public float Y
        {
            get { return XYZ.y; }
        }

        public float Z
        {
            get { return XYZ.z; }
        }
        public float W;

        public static readonly Quaternion Identity = new Quaternion(0, 0, 0, 1);

        public Quaternion(float x, float y, float z, float w)
        {
            XYZ = new Vector3(x, y, z);
            W = w;
        }
    }
}
