﻿namespace Raytracer
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
                _drawTarget.Dispose();
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
            this.label7 = new System.Windows.Forms.Label();
            this.BackfaceCullsLabel = new System.Windows.Forms.Label();
            this.RenderButton = new System.Windows.Forms.Button();
            this.StopButton = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.RaycastsSkippedLabel = new System.Windows.Forms.Label();
            this.DebugPanel = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.BoxOfSolidsRadio = new System.Windows.Forms.RadioButton();
            this.SpheresRadio = new System.Windows.Forms.RadioButton();
            this.DebugPanel.SuspendLayout();
            this.groupBox1.SuspendLayout();
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
            this.RenderTimeLabel.Size = new System.Drawing.Size(13, 13);
            this.RenderTimeLabel.TabIndex = 1;
            this.RenderTimeLabel.Text = "0";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 4);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Rays cast:";
            // 
            // RaysCastLabel
            // 
            this.RaysCastLabel.AutoSize = true;
            this.RaysCastLabel.Location = new System.Drawing.Point(7, 17);
            this.RaysCastLabel.Name = "RaysCastLabel";
            this.RaysCastLabel.Size = new System.Drawing.Size(13, 13);
            this.RaysCastLabel.TabIndex = 3;
            this.RaysCastLabel.Text = "0";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(4, 30);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(92, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Ray-Triangle tests";
            // 
            // RayTriangleTestsLabel
            // 
            this.RayTriangleTestsLabel.AutoSize = true;
            this.RayTriangleTestsLabel.Location = new System.Drawing.Point(7, 43);
            this.RayTriangleTestsLabel.Name = "RayTriangleTestsLabel";
            this.RayTriangleTestsLabel.Size = new System.Drawing.Size(13, 13);
            this.RayTriangleTestsLabel.TabIndex = 5;
            this.RayTriangleTestsLabel.Text = "0";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(4, 56);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(45, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Ray hits";
            // 
            // RayHitsLabel
            // 
            this.RayHitsLabel.AutoSize = true;
            this.RayHitsLabel.Location = new System.Drawing.Point(7, 69);
            this.RayHitsLabel.Name = "RayHitsLabel";
            this.RayHitsLabel.Size = new System.Drawing.Size(13, 13);
            this.RayHitsLabel.TabIndex = 7;
            this.RayHitsLabel.Text = "0";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(4, 82);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(110, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Bounding box checks";
            // 
            // BoundingBoxChecksLabel
            // 
            this.BoundingBoxChecksLabel.AutoSize = true;
            this.BoundingBoxChecksLabel.Location = new System.Drawing.Point(7, 95);
            this.BoundingBoxChecksLabel.Name = "BoundingBoxChecksLabel";
            this.BoundingBoxChecksLabel.Size = new System.Drawing.Size(13, 13);
            this.BoundingBoxChecksLabel.TabIndex = 9;
            this.BoundingBoxChecksLabel.Text = "0";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(4, 108);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(91, 13);
            this.label6.TabIndex = 10;
            this.label6.Text = "Bounding box hits";
            // 
            // BoundingBoxHitsLabel
            // 
            this.BoundingBoxHitsLabel.AutoSize = true;
            this.BoundingBoxHitsLabel.Location = new System.Drawing.Point(7, 121);
            this.BoundingBoxHitsLabel.Name = "BoundingBoxHitsLabel";
            this.BoundingBoxHitsLabel.Size = new System.Drawing.Size(13, 13);
            this.BoundingBoxHitsLabel.TabIndex = 11;
            this.BoundingBoxHitsLabel.Text = "0";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(4, 134);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(78, 13);
            this.label7.TabIndex = 12;
            this.label7.Text = "Backface Culls";
            // 
            // BackfaceCullsLabel
            // 
            this.BackfaceCullsLabel.AutoSize = true;
            this.BackfaceCullsLabel.Location = new System.Drawing.Point(7, 147);
            this.BackfaceCullsLabel.Name = "BackfaceCullsLabel";
            this.BackfaceCullsLabel.Size = new System.Drawing.Size(13, 13);
            this.BackfaceCullsLabel.TabIndex = 13;
            this.BackfaceCullsLabel.Text = "0";
            // 
            // RenderButton
            // 
            this.RenderButton.Location = new System.Drawing.Point(848, 295);
            this.RenderButton.Name = "RenderButton";
            this.RenderButton.Size = new System.Drawing.Size(75, 23);
            this.RenderButton.TabIndex = 14;
            this.RenderButton.Text = "Render";
            this.RenderButton.UseVisualStyleBackColor = true;
            this.RenderButton.Click += new System.EventHandler(this.RenderButton_Click);
            // 
            // StopButton
            // 
            this.StopButton.Location = new System.Drawing.Point(848, 325);
            this.StopButton.Name = "StopButton";
            this.StopButton.Size = new System.Drawing.Size(75, 23);
            this.StopButton.TabIndex = 15;
            this.StopButton.Text = "Stop";
            this.StopButton.UseVisualStyleBackColor = true;
            this.StopButton.Click += new System.EventHandler(this.StopButton_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(4, 160);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(90, 13);
            this.label8.TabIndex = 16;
            this.label8.Text = "RaycastsSkipped";
            // 
            // RaycastsSkippedLabel
            // 
            this.RaycastsSkippedLabel.AutoSize = true;
            this.RaycastsSkippedLabel.Location = new System.Drawing.Point(7, 173);
            this.RaycastsSkippedLabel.Name = "RaycastsSkippedLabel";
            this.RaycastsSkippedLabel.Size = new System.Drawing.Size(13, 13);
            this.RaycastsSkippedLabel.TabIndex = 17;
            this.RaycastsSkippedLabel.Text = "0";
            // 
            // DebugPanel
            // 
            this.DebugPanel.Controls.Add(this.RaycastsSkippedLabel);
            this.DebugPanel.Controls.Add(this.label8);
            this.DebugPanel.Controls.Add(this.BackfaceCullsLabel);
            this.DebugPanel.Controls.Add(this.label7);
            this.DebugPanel.Controls.Add(this.BoundingBoxHitsLabel);
            this.DebugPanel.Controls.Add(this.label6);
            this.DebugPanel.Controls.Add(this.BoundingBoxChecksLabel);
            this.DebugPanel.Controls.Add(this.label5);
            this.DebugPanel.Controls.Add(this.RayHitsLabel);
            this.DebugPanel.Controls.Add(this.label4);
            this.DebugPanel.Controls.Add(this.RayTriangleTestsLabel);
            this.DebugPanel.Controls.Add(this.label3);
            this.DebugPanel.Controls.Add(this.RaysCastLabel);
            this.DebugPanel.Controls.Add(this.label2);
            this.DebugPanel.Location = new System.Drawing.Point(845, 454);
            this.DebugPanel.Name = "DebugPanel";
            this.DebugPanel.Size = new System.Drawing.Size(127, 195);
            this.DebugPanel.TabIndex = 18;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.SpheresRadio);
            this.groupBox1.Controls.Add(this.BoxOfSolidsRadio);
            this.groupBox1.Location = new System.Drawing.Point(845, 116);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(113, 146);
            this.groupBox1.TabIndex = 19;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Scene";
            // 
            // BoxOfSolidsRadio
            // 
            this.BoxOfSolidsRadio.AutoSize = true;
            this.BoxOfSolidsRadio.Checked = true;
            this.BoxOfSolidsRadio.Location = new System.Drawing.Point(7, 20);
            this.BoxOfSolidsRadio.Name = "BoxOfSolidsRadio";
            this.BoxOfSolidsRadio.Size = new System.Drawing.Size(84, 17);
            this.BoxOfSolidsRadio.TabIndex = 0;
            this.BoxOfSolidsRadio.TabStop = true;
            this.BoxOfSolidsRadio.Text = "Box of solids";
            this.BoxOfSolidsRadio.UseVisualStyleBackColor = true;
            // 
            // SpheresRadio
            // 
            this.SpheresRadio.AutoSize = true;
            this.SpheresRadio.Location = new System.Drawing.Point(7, 44);
            this.SpheresRadio.Name = "SpheresRadio";
            this.SpheresRadio.Size = new System.Drawing.Size(64, 17);
            this.SpheresRadio.TabIndex = 1;
            this.SpheresRadio.TabStop = true;
            this.SpheresRadio.Text = "Spheres";
            this.SpheresRadio.UseVisualStyleBackColor = true;
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 661);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.DebugPanel);
            this.Controls.Add(this.StopButton);
            this.Controls.Add(this.RenderButton);
            this.Controls.Add(this.RenderTimeLabel);
            this.Controls.Add(this.label1);
            this.Name = "MainWindow";
            this.Text = "MainWindow";
            this.DebugPanel.ResumeLayout(false);
            this.DebugPanel.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
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
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label BackfaceCullsLabel;
        private System.Windows.Forms.Button RenderButton;
        private System.Windows.Forms.Button StopButton;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label RaycastsSkippedLabel;
        private System.Windows.Forms.Panel DebugPanel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton SpheresRadio;
        private System.Windows.Forms.RadioButton BoxOfSolidsRadio;
    }
}