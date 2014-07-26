using System;
using System.Globalization;

namespace Raytracer
{
    internal class Vector3
    {
        // ReSharper disable InconsistentNaming
        public readonly float x;
        public readonly float y;
        public readonly float z;
        // ReSharper restore InconsistentNaming

        public Vector3(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public static Vector3 Left
        {
            get { return new Vector3(-1, 0, 0); }
        }

        public static Vector3 Forward
        {
            get { return new Vector3(0, 0, 1); }
        }

        public static Vector3 Back
        {
            get { return new Vector3(0, 0, -1); }
        }

        public static Vector3 Zero
        {
            get { return new Vector3(0.0f, 0.0f, 0.0f); }
        }

        private float LengthSquared()
        {
            return (x * x + y * y + z * z);
        }

        private float Length()
        {
            return (float) Math.Sqrt(LengthSquared());
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

        public static Vector3 operator *(Vector3 lhs, float rhs)
        {
            return new Vector3(lhs.x * rhs, lhs.y * rhs, lhs.z * rhs);
        }

        public static Vector3 operator *(float rhs, Vector3 lhs)
        {
            return new Vector3(lhs.x * rhs, lhs.y * rhs, lhs.z * rhs);
        }
    }
}