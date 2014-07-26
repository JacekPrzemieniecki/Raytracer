using System.Drawing;

namespace Raytracer.Shaders
{
    abstract class Shader
    {
        public abstract Color Shade(Scene scene, RaycastHit hitInfo);
    }
}
