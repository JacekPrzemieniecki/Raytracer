using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Raytracer
{
    public partial class MainWindow : Form
    {
        Renderer renderer;
        Bitmap drawTarget;
        PictureBox display;

        public MainWindow()
        {
            InitializeComponent();
            drawTarget = new Bitmap(800, 600);
            renderer = new Renderer();
            display = new PictureBox();
            display.Size = new System.Drawing.Size(800, 600);
            display.Image = drawTarget;
            this.Controls.Add(display);
            display.Padding = new Padding(25);

            Scene s = new Scene();
            renderer.Render(drawTarget, s);
        }
    }
}
