using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raytracer.Debugging
{
    static class Counters
    {
        public static int RaysCast;
        public static int RayTriangleTests;
        public static int RayHits;
        public static int BoundingBoxChecks;
        public static int BoundingBoxHits;
    }
}
