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
            this.outputLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.widthUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.heightUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // mazePanel
            // 
            this.mazePanel.Location = new System.Drawing.Point(112, 46);
            this.mazePanel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.mazePanel.Name = "mazePanel";
            this.mazePanel.Size = new System.Drawing.Size(825, 846);
            this.mazePanel.TabIndex = 0;
            // 
            // widthUpDown
            // 
            this.widthUpDown.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.widthUpDown.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.widthUpDown.Location = new System.Drawing.Point(716, 948);
            this.widthUpDown.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
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
            this.widthUpDown.Size = new System.Drawing.Size(98, 31);
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
            this.heightUpDown.Location = new System.Drawing.Point(840, 948);
            this.heightUpDown.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
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
            this.heightUpDown.Size = new System.Drawing.Size(98, 31);
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
            this.widthLabel.Location = new System.Drawing.Point(711, 920);
            this.widthLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.widthLabel.Name = "widthLabel";
            this.widthLabel.Size = new System.Drawing.Size(60, 25);
            this.widthLabel.TabIndex = 3;
            this.widthLabel.Text = "Width";
            // 
            // heightLabel
            // 
            this.heightLabel.AutoSize = true;
            this.heightLabel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.heightLabel.Location = new System.Drawing.Point(836, 920);
            this.heightLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.heightLabel.Name = "heightLabel";
            this.heightLabel.Size = new System.Drawing.Size(65, 25);
            this.heightLabel.TabIndex = 4;
            this.heightLabel.Text = "Height";
            // 
            // generateMazeButton
            // 
            this.generateMazeButton.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.generateMazeButton.Location = new System.Drawing.Point(112, 920);
            this.generateMazeButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.generateMazeButton.Name = "generateMazeButton";
            this.generateMazeButton.Size = new System.Drawing.Size(225, 69);
            this.generateMazeButton.TabIndex = 5;
            this.generateMazeButton.Text = "Generate Maze";
            this.generateMazeButton.UseVisualStyleBackColor = true;
            this.generateMazeButton.Click += new System.EventHandler(this.generateMazeButton_Click);
            // 
            // outputLabel
            // 
            this.outputLabel.AutoSize = true;
            this.outputLabel.Location = new System.Drawing.Point(2, 0);
            this.outputLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.outputLabel.Name = "outputLabel";
            this.outputLabel.Size = new System.Drawing.Size(94, 20);
            this.outputLabel.TabIndex = 6;
            this.outputLabel.Text = "outputLabel";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1026, 1017);
            this.Controls.Add(this.outputLabel);
            this.Controls.Add(this.generateMazeButton);
            this.Controls.Add(this.heightLabel);
            this.Controls.Add(this.widthLabel);
            this.Controls.Add(this.heightUpDown);
            this.Controls.Add(this.widthUpDown);
            this.Controls.Add(this.mazePanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Maze NEA";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.widthUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.heightUpDown)).EndInit();
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
        private System.Windows.Forms.Label outputLabel;
    }
}

