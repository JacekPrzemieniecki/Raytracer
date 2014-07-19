using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Raytracer
{
    public partial class MainWindow : Form
    {
        private PictureBox display;
        private Bitmap drawTarget;
        private Renderer renderer;

        public MainWindow()
        {
            InitializeComponent();
            drawTarget = new Bitmap(800, 600);
            renderer = new Renderer();
            display = new PictureBox();
            display.Size = new Size(800, 600);
            display.Image = drawTarget;
            Controls.Add(display);
            display.Padding = new Padding(25);

            var s = new Scene();
            var watch = Stopwatch.StartNew();
            renderer.Render(drawTarget, s);
            watch.Stop();
            RenderTimeLabel.Text = string.Format("{0} s, {1} ms", watch.Elapsed.Seconds, watch.Elapsed.Milliseconds);
            RaysCastLabel.Text = Debugging.Counters.RaysCast.ToString();
            RayTriangleTestsLabel.Text = Debugging.Counters.RayTriangleTests.ToString();
            RayHitsLabel.Text = Debugging.Counters.RayHits.ToString();
        }
    }
}