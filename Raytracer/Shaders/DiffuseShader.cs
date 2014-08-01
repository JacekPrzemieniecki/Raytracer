using System;
using System.Drawing;
using Raytracer.LightSources;

namespace Raytracer.Shaders
{
    internal class DiffuseShader : Shader
    {
        public override Vector3 Shade(Scene scene, RaycastHit hitInfo, int maxRecursiveRaycasts)
        {
            float totalIntensity = 0;
            Vector3 normal = hitInfo.Triangle.SurfaceNormal(hitInfo.U, hitInfo.V);
            foreach (LightSource lightSource in scene.LightSources)
            {
                Color c;
                totalIntensity += lightSource.IntensityAt(hitInfo.Position, normal, scene, out c);
            }
            Vector3 diffuse = Vector3.FromColor(hitInfo.Triangle.Color);
            return diffuse * totalIntensity;
        }
    }
}