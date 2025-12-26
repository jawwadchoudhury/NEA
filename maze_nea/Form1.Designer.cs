namespace maze_nea
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
            this.mazePanel = new System.Windows.Forms.Panel();
            this.widthUpDown = new System.Windows.Forms.NumericUpDown();
            this.heightUpDown = new System.Windows.Forms.NumericUpDown();
            this.widthLabel = new System.Windows.Forms.Label();
            this.heightLabel = new System.Windows.Forms.Label();
            this.generateMazeButton = new System.Windows.Forms.Button();
            this.solveMazeButton = new System.Windows.Forms.Button();
            this.stepCountLabel = new System.Windows.Forms.Label();
            this.importMazeButton = new System.Windows.Forms.Button();
            this.exportMazeButton = new System.Windows.Forms.Button();
            this.wallsRemovedLabel = new System.Windows.Forms.Label();
            this.wallsRemovedUpDown = new System.Windows.Forms.NumericUpDown();
            this.exportAsImageButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.widthUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.heightUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.wallsRemovedUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // mazePanel
            // 
            this.mazePanel.BackColor = System.Drawing.Color.White;
            this.mazePanel.Location = new System.Drawing.Point(77, 35);
            this.mazePanel.Name = "mazePanel";
            this.mazePanel.Size = new System.Drawing.Size(550, 550);
            this.mazePanel.TabIndex = 0;
            // 
            // widthUpDown
            // 
            this.widthUpDown.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.widthUpDown.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.widthUpDown.Location = new System.Drawing.Point(376, 620);
            this.widthUpDown.Maximum = new decimal(new int[] {
            25,
            0,
            0,
            0});
            this.widthUpDown.Minimum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.widthUpDown.Name = "widthUpDown";
            this.widthUpDown.Size = new System.Drawing.Size(65, 23);
            this.widthUpDown.TabIndex = 1;
            this.widthUpDown.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.widthUpDown.ValueChanged += new System.EventHandler(this.widthUpDown_ValueChanged);
            // 
            // heightUpDown
            // 
            this.heightUpDown.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.heightUpDown.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.heightUpDown.Location = new System.Drawing.Point(460, 620);
            this.heightUpDown.Maximum = new decimal(new int[] {
            25,
            0,
            0,
            0});
            this.heightUpDown.Minimum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.heightUpDown.Name = "heightUpDown";
            this.heightUpDown.Size = new System.Drawing.Size(65, 23);
            this.heightUpDown.TabIndex = 2;
            this.heightUpDown.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.heightUpDown.ValueChanged += new System.EventHandler(this.heightUpDown_ValueChanged);
            // 
            // widthLabel
            // 
            this.widthLabel.AutoSize = true;
            this.widthLabel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.widthLabel.Location = new System.Drawing.Point(385, 602);
            this.widthLabel.Name = "widthLabel";
            this.widthLabel.Size = new System.Drawing.Size(39, 15);
            this.widthLabel.TabIndex = 3;
            this.widthLabel.Text = "Width";
            // 
            // heightLabel
            // 
            this.heightLabel.AutoSize = true;
            this.heightLabel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.heightLabel.Location = new System.Drawing.Point(469, 602);
            this.heightLabel.Name = "heightLabel";
            this.heightLabel.Size = new System.Drawing.Size(43, 15);
            this.heightLabel.TabIndex = 4;
            this.heightLabel.Text = "Height";
            // 
            // generateMazeButton
            // 
            this.generateMazeButton.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.generateMazeButton.Location = new System.Drawing.Point(30, 598);
            this.generateMazeButton.Name = "generateMazeButton";
            this.generateMazeButton.Size = new System.Drawing.Size(150, 45);
            this.generateMazeButton.TabIndex = 5;
            this.generateMazeButton.Text = "Generate New Maze";
            this.generateMazeButton.UseVisualStyleBackColor = true;
            this.generateMazeButton.Click += new System.EventHandler(this.generateMazeButton_Click);
            // 
            // solveMazeButton
            // 
            this.solveMazeButton.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.solveMazeButton.Location = new System.Drawing.Point(186, 598);
            this.solveMazeButton.Name = "solveMazeButton";
            this.solveMazeButton.Size = new System.Drawing.Size(150, 45);
            this.solveMazeButton.TabIndex = 9;
            this.solveMazeButton.Text = "Solve Maze";
            this.solveMazeButton.UseVisualStyleBackColor = true;
            this.solveMazeButton.Click += new System.EventHandler(this.solveMazeButton_Click);
            // 
            // stepCountLabel
            // 
            this.stepCountLabel.AutoSize = true;
            this.stepCountLabel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.stepCountLabel.Location = new System.Drawing.Point(218, 644);
            this.stepCountLabel.Name = "stepCountLabel";
            this.stepCountLabel.Size = new System.Drawing.Size(90, 15);
            this.stepCountLabel.TabIndex = 10;
            this.stepCountLabel.Text = "Step Count: 999";
            this.stepCountLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.stepCountLabel.Visible = false;
            // 
            // importMazeButton
            // 
            this.importMazeButton.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.importMazeButton.Location = new System.Drawing.Point(78, 6);
            this.importMazeButton.Name = "importMazeButton";
            this.importMazeButton.Size = new System.Drawing.Size(80, 23);
            this.importMazeButton.TabIndex = 12;
            this.importMazeButton.Text = "Import";
            this.importMazeButton.UseVisualStyleBackColor = true;
            this.importMazeButton.Click += new System.EventHandler(this.importMazeButton_Click);
            // 
            // exportMazeButton
            // 
            this.exportMazeButton.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.exportMazeButton.Location = new System.Drawing.Point(164, 6);
            this.exportMazeButton.Name = "exportMazeButton";
            this.exportMazeButton.Size = new System.Drawing.Size(80, 23);
            this.exportMazeButton.TabIndex = 13;
            this.exportMazeButton.Text = "Export";
            this.exportMazeButton.UseVisualStyleBackColor = true;
            this.exportMazeButton.Click += new System.EventHandler(this.exportMazeButton_Click);
            // 
            // wallsRemovedLabel
            // 
            this.wallsRemovedLabel.AutoSize = true;
            this.wallsRemovedLabel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.wallsRemovedLabel.Location = new System.Drawing.Point(544, 602);
            this.wallsRemovedLabel.Name = "wallsRemovedLabel";
            this.wallsRemovedLabel.Size = new System.Drawing.Size(109, 15);
            this.wallsRemovedLabel.TabIndex = 15;
            this.wallsRemovedLabel.Text = "Walls Removed (%)";
            // 
            // wallsRemovedUpDown
            // 
            this.wallsRemovedUpDown.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.wallsRemovedUpDown.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.wallsRemovedUpDown.Location = new System.Drawing.Point(547, 620);
            this.wallsRemovedUpDown.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.wallsRemovedUpDown.Name = "wallsRemovedUpDown";
            this.wallsRemovedUpDown.Size = new System.Drawing.Size(106, 23);
            this.wallsRemovedUpDown.TabIndex = 14;
            this.wallsRemovedUpDown.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            // 
            // exportAsImageButton
            // 
            this.exportAsImageButton.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.exportAsImageButton.Location = new System.Drawing.Point(517, 6);
            this.exportAsImageButton.Name = "exportAsImageButton";
            this.exportAsImageButton.Size = new System.Drawing.Size(100, 23);
            this.exportAsImageButton.TabIndex = 16;
            this.exportAsImageButton.Text = "Export as Image";
            this.exportAsImageButton.UseVisualStyleBackColor = true;
            this.exportAsImageButton.Click += new System.EventHandler(this.exportAsImageButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(684, 661);
            this.Controls.Add(this.exportAsImageButton);
            this.Controls.Add(this.wallsRemovedLabel);
            this.Controls.Add(this.wallsRemovedUpDown);
            this.Controls.Add(this.exportMazeButton);
            this.Controls.Add(this.importMazeButton);
            this.Controls.Add(this.stepCountLabel);
            this.Controls.Add(this.solveMazeButton);
            this.Controls.Add(this.generateMazeButton);
            this.Controls.Add(this.heightLabel);
            this.Controls.Add(this.widthLabel);
            this.Controls.Add(this.heightUpDown);
            this.Controls.Add(this.widthUpDown);
            this.Controls.Add(this.mazePanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Maze NEA";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.widthUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.heightUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.wallsRemovedUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel mazePanel;
        private System.Windows.Forms.NumericUpDown widthUpDown;
        private System.Windows.Forms.NumericUpDown heightUpDown;
        private System.Windows.Forms.Label widthLabel;
        private System.Windows.Forms.Label heightLabel;
        private System.Windows.Forms.Button generateMazeButton;
        private System.Windows.Forms.Button solveMazeButton;
        private System.Windows.Forms.Label stepCountLabel;
        private System.Windows.Forms.Button importMazeButton;
        private System.Windows.Forms.Button exportMazeButton;
        private System.Windows.Forms.Label wallsRemovedLabel;
        private System.Windows.Forms.NumericUpDown wallsRemovedUpDown;
        private System.Windows.Forms.Button exportAsImageButton;
    }
}

