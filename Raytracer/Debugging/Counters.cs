#if DEBUG
namespace Raytracer.Debugging
{
    internal static class Counters
    {
        public static int RaysCast;
        public static int RayTriangleTests;
        public static int RayHits;
        public static int BoundingBoxChecks;
        public static int BoundingBoxHits;
        public static int BackfaceCulls;
        public static int RaycastsSkipped;
    }
}
#endif