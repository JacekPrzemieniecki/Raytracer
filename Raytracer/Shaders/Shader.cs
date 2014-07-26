﻿using System.Drawing;

namespace Raytracer.Shaders
{
    abstract class Shader
    {
        public abstract Color Shade(Scene scene, RaycastHit hitInfo, int maxRecursiveRaycasts);

        protected static Ray ReflectedRay(RaycastHit hitInfo)
        {
            Vector3 normal = hitInfo.Triangle.SurfaceNormal(hitInfo.U, hitInfo.V);
            Vector3 incident = hitInfo.Ray.Direction;
            Vector3 reflected = incident - 2 * Vector3.Dot(incident, normal) * normal;
            return new Ray(hitInfo.Position, reflected);
        }
    }
}