using System;
using System.Drawing;

namespace Raytracer.Shaders
{
    class DiffuseShader : Shader
    {
        public override Color Shade(Scene scene, RaycastHit hitInfo, int maxRecursiveRaycasts)
        {
            float totalIntensity = 0;
            Color c;
            Vector3 normal = hitInfo.Triangle.SurfaceNormal(hitInfo.U, hitInfo.V);
            foreach (var lightSource in scene.LightSources)
            {
                totalIntensity += Math.Max(lightSource.IntensityAt(hitInfo.Position, normal, scene, out c), 0);
            }
            Color diffuse = hitInfo.Triangle.Color;

            return Color.FromArgb(
                Math.Min((int) (diffuse.R * totalIntensity), 0xFF),
                Math.Min((int) (diffuse.G * totalIntensity), 0xFF),
                Math.Min((int) (diffuse.B * totalIntensity), 0xFF)
                );
        }
    }
}
