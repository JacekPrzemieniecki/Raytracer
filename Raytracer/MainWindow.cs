using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

#if DEBUG
using System.Globalization;
using Raytracer.Debugging;
#endif

namespace Raytracer
{
    public partial class MainWindow : Form
    {
        private const int RenderPasses = 10;
        private readonly PictureBox _display;
        private readonly Bitmap _drawTarget;
        private bool _stopFlag;

        public MainWindow()
        {
            InitializeComponent();
            _drawTarget = new Bitmap(800, 600);
            _display = new PictureBox {Size = new Size(800, 600), Image = _drawTarget};
            Controls.Add(_display);
            _display.Padding = new Padding(25);
        }

        private void RenderButton_Click(object sender, EventArgs e)
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
                    UpdateLabels();
                },
                RenderPasses,
                ref _stopFlag);
            watch.Stop();
            RenderTimeLabel.Text = string.Format("{0} min, {1} s, {2} ms", watch.Elapsed.Minutes, watch.Elapsed.Seconds,
                watch.Elapsed.Milliseconds);
        }

        private void UpdateLabels()
        {
            #if DEBUG
            RaysCastLabel.Text = Counters.RaysCast.ToString(CultureInfo.InvariantCulture);
            RayTriangleTestsLabel.Text = Counters.RayTriangleTests.ToString(CultureInfo.InvariantCulture);
            RayHitsLabel.Text = Counters.RayHits.ToString(CultureInfo.InvariantCulture);
            BoundingBoxChecksLabel.Text = Counters.BoundingBoxChecks.ToString(CultureInfo.InvariantCulture);
            BoundingBoxHitsLabel.Text = Counters.BoundingBoxHits.ToString(CultureInfo.InvariantCulture);
            BackfaceCullsLabel.Text = Counters.BackfaceCulls.ToString(CultureInfo.InvariantCulture);
            RaycastsSkippedLabel.Text = Counters.RaycastsSkipped.ToString(CultureInfo.InvariantCulture);
            #endif
        }

        private void StopButton_Click(object sender, EventArgs e)
        {
            _stopFlag = true;
        }
    }
}