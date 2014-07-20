namespace Raytracer
{
    partial class MainWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.RenderTimeLabel = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.RaysCastLabel = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.RayTriangleTestsLabel = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.RayHitsLabel = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.BoundingBoxChecksLabel = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.BoundingBoxHitsLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(845, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Render Time:";
            // 
            // RenderTimeLabel
            // 
            this.RenderTimeLabel.AutoSize = true;
            this.RenderTimeLabel.Location = new System.Drawing.Point(848, 26);
            this.RenderTimeLabel.Name = "RenderTimeLabel";
            this.RenderTimeLabel.Size = new System.Drawing.Size(35, 13);
            this.RenderTimeLabel.TabIndex = 1;
            this.RenderTimeLabel.Text = "label2";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(845, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Rays cast:";
            // 
            // RaysCastLabel
            // 
            this.RaysCastLabel.AutoSize = true;
            this.RaysCastLabel.Location = new System.Drawing.Point(848, 69);
            this.RaysCastLabel.Name = "RaysCastLabel";
            this.RaysCastLabel.Size = new System.Drawing.Size(35, 13);
            this.RaysCastLabel.TabIndex = 3;
            this.RaysCastLabel.Text = "label3";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(845, 82);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(92, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Ray-Triangle tests";
            // 
            // RayTriangleTestsLabel
            // 
            this.RayTriangleTestsLabel.AutoSize = true;
            this.RayTriangleTestsLabel.Location = new System.Drawing.Point(848, 95);
            this.RayTriangleTestsLabel.Name = "RayTriangleTestsLabel";
            this.RayTriangleTestsLabel.Size = new System.Drawing.Size(35, 13);
            this.RayTriangleTestsLabel.TabIndex = 5;
            this.RayTriangleTestsLabel.Text = "label4";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(845, 108);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(45, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Ray hits";
            // 
            // RayHitsLabel
            // 
            this.RayHitsLabel.AutoSize = true;
            this.RayHitsLabel.Location = new System.Drawing.Point(848, 121);
            this.RayHitsLabel.Name = "RayHitsLabel";
            this.RayHitsLabel.Size = new System.Drawing.Size(35, 13);
            this.RayHitsLabel.TabIndex = 7;
            this.RayHitsLabel.Text = "label5";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(845, 134);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(110, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Bounding box checks";
            // 
            // BoundingBoxChecksLabel
            // 
            this.BoundingBoxChecksLabel.AutoSize = true;
            this.BoundingBoxChecksLabel.Location = new System.Drawing.Point(848, 147);
            this.BoundingBoxChecksLabel.Name = "BoundingBoxChecksLabel";
            this.BoundingBoxChecksLabel.Size = new System.Drawing.Size(35, 13);
            this.BoundingBoxChecksLabel.TabIndex = 9;
            this.BoundingBoxChecksLabel.Text = "label6";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(845, 160);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(91, 13);
            this.label6.TabIndex = 10;
            this.label6.Text = "Bounding box hits";
            // 
            // BoundingBoxHitsLabel
            // 
            this.BoundingBoxHitsLabel.AutoSize = true;
            this.BoundingBoxHitsLabel.Location = new System.Drawing.Point(848, 173);
            this.BoundingBoxHitsLabel.Name = "BoundingBoxHitsLabel";
            this.BoundingBoxHitsLabel.Size = new System.Drawing.Size(35, 13);
            this.BoundingBoxHitsLabel.TabIndex = 11;
            this.BoundingBoxHitsLabel.Text = "label7";
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 661);
            this.Controls.Add(this.BoundingBoxHitsLabel);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.BoundingBoxChecksLabel);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.RayHitsLabel);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.RayTriangleTestsLabel);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.RaysCastLabel);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.RenderTimeLabel);
            this.Controls.Add(this.label1);
            this.Name = "MainWindow";
            this.Text = "MainWindow";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label RenderTimeLabel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label RaysCastLabel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label RayTriangleTestsLabel;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label RayHitsLabel;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label BoundingBoxChecksLabel;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label BoundingBoxHitsLabel;
    }
}