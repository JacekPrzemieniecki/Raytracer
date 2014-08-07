using System;
using System.Globalization;

namespace Raytracer
{
    internal class Vector2
    {
        // ReSharper disable InconsistentNaming
        public readonly float x;
        public readonly float y;
        // ReSharper restore InconsistentNaming

        public Vector2(float x, float y)
        {
            this.x = x;
            this.y = y;
        }

        public static Vector2 Left
        {
            get { return new Vector2(-1, 0); }
        }

        public static Vector2 Right
        {
            get { return new Vector2(1, 0); }
        }

        public static Vector2 Up
        {
            get { return new Vector2(0, 1); }
        }

        public static Vector2 Down
        {
            get { return new Vector2(0, -1); }
        }

        public static Vector2 Zero
        {
            get { return new Vector2(0, 0); }
        }

        private float LengthSquared()
        {
            return (x * x + y * y);
        }

        public float DistanceFrom(Vector2 other)
        {
            return (this - other).Length();
        }

        public float Length()
        {
            return (float) Math.Sqrt(LengthSquared());
        }

        public Vector2 Normalized()
        {
            float len = Length();
            return new Vector2(x / len, y / len);
        }

        public override string ToString()
        {
            return string.Format("Vector3 {0}, {1}",
                x.ToString("+##0.0;-##0.0", CultureInfo.InvariantCulture),
                y.ToString("+##0.0;-##0.0", CultureInfo.InvariantCulture));
        }

        public static float Dot(Vector2 lhs, Vector2 rhs)
        {
            return lhs.x * rhs.x +
                   lhs.y * rhs.y;
        }

        public static Vector2 operator +(Vector2 lhs, Vector2 rhs)
        {
            return new Vector2(lhs.x + rhs.x, lhs.y + rhs.y);
        }

        public static Vector2 operator -(Vector2 lhs, Vector2 rhs)
        {
            return new Vector2(lhs.x - rhs.x, lhs.y - rhs.y);
        }

        public static Vector2 operator *(Vector2 lhs, float rhs)
        {
            return new Vector2(lhs.x * rhs, lhs.y * rhs);
        }

        public static Vector2 operator *(float rhs, Vector2 lhs)
        {
            return new Vector2(lhs.x * rhs, lhs.y * rhs);
        }
    }
}
