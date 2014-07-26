using System.Drawing;

namespace Raytracer
{
    abstract class LightSource
    {
        public abstract float IntensityAt(Vector3 position, Vector3 surfaceNormal, Scene scene, out Color color);
    }
}
