using System.Drawing;

namespace Raytracer
{
    interface IRaycastable
    {
        float Raycast(Ray ray, ref Color color, float maxDistance);
    }
}
