﻿using System;

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

        public static Quaternion LookRotation(Vector3 direction, Vector3 up)
        {
            Vector3 directionNormalized = direction.Normalized();
            // Rotate around up vector
            Vector3 directionProjected = (directionNormalized - up * Vector3.Dot(directionNormalized, up)).Normalized();
            Vector3 forwardProjected = (Vector3.Forward - up * Vector3.Dot(Vector3.Forward, up)).Normalized();
            float rotationAroundUpAngleHalf = (float) Math.Acos((Vector3.Dot(directionProjected, forwardProjected))) / 2;
            Quaternion rotationAroundUp = new Quaternion(up * (float)Math.Sin(rotationAroundUpAngleHalf), (float)Math.Cos(rotationAroundUpAngleHalf));

            // Rotate around horizontal vector
            Vector3 rotationAxis = Vector3.Cross(directionNormalized, directionProjected);
            double angleHalf = Math.Acos(Vector3.Dot(directionNormalized, directionProjected)) / 2;
            Quaternion rotationAroundHorizontal = new Quaternion(rotationAxis * (float) Math.Sin(angleHalf), (float) Math.Cos(angleHalf));

            return rotationAroundUp * rotationAroundHorizontal;
        }

        public Quaternion(float x, float y, float z, float w)
        {
            XYZ = new Vector3(x, y, z);
            W = w;
        }

        public Quaternion(Vector3 xyz, float w)
        {
            XYZ = xyz;
            W = w;
        }

        public Quaternion Normalized()
        {
            var length = (float) Math.Sqrt(XYZ.LengthSquared() + W * W);
            return new Quaternion(X/length, Y/length, Z/length, W/length);
        }

        public static Quaternion operator *(Quaternion lhs, Quaternion rhs)
        {
            return new Quaternion(
                        lhs.W * rhs.X + lhs.X * rhs.W + lhs.Y * rhs.Z - lhs.Z * rhs.Y,
                        lhs.W * rhs.Y - lhs.X * rhs.Z + lhs.Y * rhs.W + lhs.Z * rhs.X,
                        lhs.W * rhs.Z + lhs.X * rhs.Y - lhs.Y * rhs.X + lhs.Z * rhs.W,
                        lhs.W * rhs.W - lhs.X * rhs.X - lhs.Y * rhs.Y - lhs.Z * rhs.Z);
        }
    }
}
