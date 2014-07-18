using System;
using System.Globalization;

namespace Raytracer
{
    internal struct Vector3
    {
        // ReSharper disable InconsistentNaming
        public float x;
        public float y;
        public float z;
        // ReSharper restore InconsistentNaming

        public Vector3(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public float LengthSquared()
        {
            return (x * x + y * y + z * z);
        }

        public float Length()
        {
            return (float)Math.Sqrt(LengthSquared());
        }

        public Vector3 Normalized()
        {
            float len = Length();
            return new Vector3(x / len, y / len, z / len);
        }

        public override string ToString()
        {
            return string.Format("Vector3 {0}, {1}, {2}",
                x.ToString("+##0.0;-##0.0", CultureInfo.InvariantCulture),
                y.ToString("+##0.0;-##0.0", CultureInfo.InvariantCulture),
                z.ToString("+##0.0;-##0.0", CultureInfo.InvariantCulture));
        }

        public static Vector3 Zero
        {
            get { return new Vector3(0.0f, 0.0f, 0.0f); }
        }

        public static float Dot(Vector3 lhs, Vector3 rhs)
        {
            return lhs.x * rhs.x +
                   lhs.y * rhs.y +
                   lhs.z * rhs.z;
        }

        public static Vector3 Cross(Vector3 lhs, Vector3 rhs)
        {
            return new Vector3(lhs.y * rhs.z - lhs.z * rhs.y,
                lhs.z * rhs.x - lhs.x * rhs.z,
                lhs.x * rhs.y - lhs.y * rhs.x);
        }

        public static Vector3 operator +(Vector3 lhs, Vector3 rhs)
        {
            return new Vector3(lhs.x + rhs.x, lhs.y + rhs.y, lhs.z + rhs.z);
        }

        public static Vector3 operator -(Vector3 lhs, Vector3 rhs)
        {
            return new Vector3(lhs.x - rhs.x, lhs.y - rhs.y, lhs.z - rhs.z);
        }
    }
}