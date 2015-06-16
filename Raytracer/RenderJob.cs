namespace Raytracer
{
    internal class RenderJob
    {
        public int StartX;
        public int StartY;
        public int Width;
        public int Height;
        public int PictureWidth;
        public int PictureHeight;
        public Vector3[] Output;
        public int PassesLeft;
        public Scene Scene;
        public bool ForceStop;

        public RenderJob(int x, int y, int width, int height, int pictureWidth, int pictureHeight, int passes, Scene scene)
        {
            StartY = y;
            StartX = x;
            Width = width;
            Height = height;
            PictureHeight = pictureHeight;
            PictureWidth = pictureWidth;
            PassesLeft = passes;
            Output = new Vector3[width*height];
            Scene = scene;
        }
    }
}