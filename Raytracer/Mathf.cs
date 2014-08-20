namespace Raytracer
{
    class Mathf
    {
        public static float Clamp(float value, float min, float max)
        {
            return value > max ? max : value < min ? min : value;
        }

        public static int Clamp(int value, int min, int max)
        {
            return value > max ? max : value < min ? min : value;
        }
    }
}
