using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using Raytracer.Debugging;

namespace Raytracer
{
    public partial class MainWindow : Form
    {
        private readonly Bitmap _drawTarget;
        private readonly PictureBox _display;
        private bool _stopFlag;

        public MainWindow()
        {
            InitializeComponent();
            _drawTarget = new Bitmap(800, 600);
            _display = new PictureBox { Size = new Size(800, 600), Image = _drawTarget };
            Controls.Add(_display);
            _display.Padding = new Padding(25);
        }

        private void RenderButton_Click(object sender, System.EventArgs e)
        {
            _stopFlag = false;
            var renderer = new Renderer();
            var s = new Scene();
            Stopwatch watch = Stopwatch.StartNew();
            renderer.Render(
                _drawTarget,
                s,
                () =>
                {
                    _display.Invalidate();
                    Application.DoEvents();
                },
                ref _stopFlag);
            watch.Stop();
            RenderTimeLabel.Text = string.Format("{0} min, {1} s, {2} ms", watch.Elapsed.Minutes, watch.Elapsed.Seconds, watch.Elapsed.Milliseconds);
            RaysCastLabel.Text = Counters.RaysCast.ToString(CultureInfo.InvariantCulture);
            RayTriangleTestsLabel.Text = Counters.RayTriangleTests.ToString(CultureInfo.InvariantCulture);
            RayHitsLabel.Text = Counters.RayHits.ToString(CultureInfo.InvariantCulture);
            BoundingBoxChecksLabel.Text = Counters.BoundingBoxChecks.ToString(CultureInfo.InvariantCulture);
            BoundingBoxHitsLabel.Text = Counters.BoundingBoxHits.ToString(CultureInfo.InvariantCulture);
            BackfaceCullsLabel.Text = Counters.BackfaceCulls.ToString(CultureInfo.InvariantCulture);
        }

        private void StopButton_Click(object sender, System.EventArgs e)
        {
            _stopFlag = true;
        }
    }
}