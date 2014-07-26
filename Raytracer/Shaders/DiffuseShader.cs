using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raytracer.Shaders
{
    class DiffuseShader : Shader
    {
        public override Color Shade(Scene scene, Mesh mesh, RaycastHit hitInfo)
        {
            float totalIntensity = 0;
            Color c;
            Vector3 normal = hitInfo.Mesh.SurfaceNormal(hitInfo);
            foreach (var lightSource in scene.LightSources)
            {
                totalIntensity += Math.Max(lightSource.IntensityAt(hitInfo.Position, normal, scene, out c), 0);
            }
            Color diffuse = hitInfo.Mesh.GetDiffuseColor(hitInfo);

            return Color.FromArgb(
                Math.Min((int) (diffuse.R * totalIntensity), 0xFF),
                Math.Min((int) (diffuse.G * totalIntensity), 0xFF),
                Math.Min((int) (diffuse.B * totalIntensity), 0xFF)
                );
        }
    }
}
