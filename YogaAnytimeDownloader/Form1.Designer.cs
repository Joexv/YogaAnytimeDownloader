
namespace YogaAnytimeDownloader
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.label1 = new System.Windows.Forms.Label();
            this.DL_Season = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.DL_Video = new System.Windows.Forms.Button();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.qualityPicker = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.linkLabel2 = new System.Windows.Forms.LinkLabel();
            this.VideoPB = new System.Windows.Forms.ProgressBar();
            this.DLabel = new System.Windows.Forms.Label();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.fromFireFoxToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fromChromeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 405);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(787, 75);
            this.label1.TabIndex = 0;
            this.label1.Text = "Downloading from YogaAnytime isn\'t anything fancy, just uses m3u8 and ts files. \r" +
    "\nThis program helps automize it a smidge. \r\nFillout one of the URLs and hit down" +
    "load.";
            // 
            // DL_Season
            // 
            this.DL_Season.Location = new System.Drawing.Point(12, 149);
            this.DL_Season.Name = "DL_Season";
            this.DL_Season.Size = new System.Drawing.Size(249, 45);
            this.DL_Season.TabIndex = 1;
            this.DL_Season.Text = "Download Season";
            this.DL_Season.UseVisualStyleBackColor = true;
            this.DL_Season.Click += new System.EventHandler(this.DL_Season_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(12, 97);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(951, 31);
            this.textBox1.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(133, 25);
            this.label2.TabIndex = 3;
            this.label2.Text = "Season URL";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 233);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(133, 25);
            this.label3.TabIndex = 5;
            this.label3.Text = "Single Video";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(12, 281);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(951, 31);
            this.textBox2.TabIndex = 4;
            // 
            // DL_Video
            // 
            this.DL_Video.Location = new System.Drawing.Point(12, 328);
            this.DL_Video.Name = "DL_Video";
            this.DL_Video.Size = new System.Drawing.Size(249, 45);
            this.DL_Video.TabIndex = 6;
            this.DL_Video.Text = "Download Video";
            this.DL_Video.UseVisualStyleBackColor = true;
            this.DL_Video.Click += new System.EventHandler(this.DL_Video_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.qualityPicker,
            this.toolStripSeparator1,
            this.toolStripDropDownButton1,
            this.toolStripButton1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(981, 50);
            this.toolStrip1.TabIndex = 7;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(160, 36);
            this.toolStripLabel1.Text = "Video Quality";
            // 
            // qualityPicker
            // 
            this.qualityPicker.Items.AddRange(new object[] {
            "720p",
            "504p",
            "360p",
            "216p"});
            this.qualityPicker.Name = "qualityPicker";
            this.qualityPicker.Size = new System.Drawing.Size(121, 42);
            this.qualityPicker.Text = "720p";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 42);
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(789, 159);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(174, 25);
            this.linkLabel1.TabIndex = 8;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "Season Example";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // linkLabel2
            // 
            this.linkLabel2.AutoSize = true;
            this.linkLabel2.Location = new System.Drawing.Point(741, 338);
            this.linkLabel2.Name = "linkLabel2";
            this.linkLabel2.Size = new System.Drawing.Size(222, 25);
            this.linkLabel2.TabIndex = 9;
            this.linkLabel2.TabStop = true;
            this.linkLabel2.Text = "Single Video Example";
            this.linkLabel2.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel2_LinkClicked);
            // 
            // VideoPB
            // 
            this.VideoPB.Location = new System.Drawing.Point(12, 492);
            this.VideoPB.Name = "VideoPB";
            this.VideoPB.Size = new System.Drawing.Size(951, 45);
            this.VideoPB.TabIndex = 11;
            // 
            // DLabel
            // 
            this.DLabel.Location = new System.Drawing.Point(12, 540);
            this.DLabel.Name = "DLabel";
            this.DLabel.Size = new System.Drawing.Size(951, 41);
            this.DLabel.TabIndex = 12;
            this.DLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // toolStripDropDownButton1
            // 
            this.toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fromFireFoxToolStripMenuItem,
            this.fromChromeToolStripMenuItem});
            this.toolStripDropDownButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton1.Image")));
            this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.Size = new System.Drawing.Size(213, 44);
            this.toolStripDropDownButton1.Text = "Get Auth Cookie";
            // 
            // fromFireFoxToolStripMenuItem
            // 
            this.fromFireFoxToolStripMenuItem.Name = "fromFireFoxToolStripMenuItem";
            this.fromFireFoxToolStripMenuItem.Size = new System.Drawing.Size(359, 44);
            this.fromFireFoxToolStripMenuItem.Text = "From FireFox";
            this.fromFireFoxToolStripMenuItem.Click += new System.EventHandler(this.fromFireFoxToolStripMenuItem_Click);
            // 
            // fromChromeToolStripMenuItem
            // 
            this.fromChromeToolStripMenuItem.Name = "fromChromeToolStripMenuItem";
            this.fromChromeToolStripMenuItem.Size = new System.Drawing.Size(359, 44);
            this.fromChromeToolStripMenuItem.Text = "From Chrome";
            this.fromChromeToolStripMenuItem.Click += new System.EventHandler(this.fromChromeToolStripMenuItem_Click);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(172, 44);
            this.toolStripButton1.Text = "Convert Video";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(981, 590);
            this.Controls.Add(this.DLabel);
            this.Controls.Add(this.VideoPB);
            this.Controls.Add(this.linkLabel2);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.DL_Video);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.DL_Season);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "YogaAnytime Downloader";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button DL_Season;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button DL_Video;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripComboBox qualityPicker;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.LinkLabel linkLabel2;
        private System.Windows.Forms.ProgressBar VideoPB;
        private System.Windows.Forms.Label DLabel;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
        private System.Windows.Forms.ToolStripMenuItem fromFireFoxToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fromChromeToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
    }
}

