using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace Raytracer
{
    public partial class MainWindow : Form
    {
        private Bitmap drawTarget;

        public MainWindow()
        {
            InitializeComponent();
            drawTarget = new Bitmap(800, 600);
            var renderer = new Renderer();
            var display = new PictureBox {Size = new Size(800, 600), Image = drawTarget};
            Controls.Add(display);
            display.Padding = new Padding(25);

            var s = new Scene();
            var watch = Stopwatch.StartNew();
            renderer.Render(drawTarget, s);
            watch.Stop();
            RenderTimeLabel.Text = string.Format("{0} s, {1} ms", watch.Elapsed.Seconds, watch.Elapsed.Milliseconds);
            RaysCastLabel.Text = Debugging.Counters.RaysCast.ToString(CultureInfo.InvariantCulture);
            RayTriangleTestsLabel.Text = Debugging.Counters.RayTriangleTests.ToString(CultureInfo.InvariantCulture);
            RayHitsLabel.Text = Debugging.Counters.RayHits.ToString(CultureInfo.InvariantCulture);
            BoundingBoxChecksLabel.Text = Debugging.Counters.BoundingBoxChecks.ToString(CultureInfo.InvariantCulture);
            BoundingBoxHitsLabel.Text = Debugging.Counters.BoundingBoxHits.ToString(CultureInfo.InvariantCulture);
        }
    }
}