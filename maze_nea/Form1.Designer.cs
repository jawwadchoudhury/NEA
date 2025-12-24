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
            this.codeLabel = new System.Windows.Forms.Label();
            this.codeTextBox = new System.Windows.Forms.TextBox();
            this.solveMazeButton = new System.Windows.Forms.Button();
            this.stepCountLabel = new System.Windows.Forms.Label();
            this.importMazeButton = new System.Windows.Forms.Button();
            this.exportMazeButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.widthUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.heightUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // mazePanel
            // 
            this.mazePanel.BackColor = System.Drawing.Color.White;
            this.mazePanel.Location = new System.Drawing.Point(90, 55);
            this.mazePanel.Name = "mazePanel";
            this.mazePanel.Size = new System.Drawing.Size(525, 525);
            this.mazePanel.TabIndex = 0;
            // 
            // widthUpDown
            // 
            this.widthUpDown.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.widthUpDown.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.widthUpDown.Location = new System.Drawing.Point(477, 616);
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
            this.heightUpDown.Location = new System.Drawing.Point(560, 616);
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
            this.widthLabel.Location = new System.Drawing.Point(474, 598);
            this.widthLabel.Name = "widthLabel";
            this.widthLabel.Size = new System.Drawing.Size(39, 15);
            this.widthLabel.TabIndex = 3;
            this.widthLabel.Text = "Width";
            // 
            // heightLabel
            // 
            this.heightLabel.AutoSize = true;
            this.heightLabel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.heightLabel.Location = new System.Drawing.Point(557, 598);
            this.heightLabel.Name = "heightLabel";
            this.heightLabel.Size = new System.Drawing.Size(43, 15);
            this.heightLabel.TabIndex = 4;
            this.heightLabel.Text = "Height";
            // 
            // generateMazeButton
            // 
            this.generateMazeButton.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.generateMazeButton.Location = new System.Drawing.Point(75, 598);
            this.generateMazeButton.Name = "generateMazeButton";
            this.generateMazeButton.Size = new System.Drawing.Size(150, 45);
            this.generateMazeButton.TabIndex = 5;
            this.generateMazeButton.Text = "Generate New Maze";
            this.generateMazeButton.UseVisualStyleBackColor = true;
            this.generateMazeButton.Click += new System.EventHandler(this.generateMazeButton_Click);
            // 
            // codeLabel
            // 
            this.codeLabel.AutoSize = true;
            this.codeLabel.Location = new System.Drawing.Point(267, 10);
            this.codeLabel.Name = "codeLabel";
            this.codeLabel.Size = new System.Drawing.Size(35, 13);
            this.codeLabel.TabIndex = 6;
            this.codeLabel.Text = "Code:";
            // 
            // codeTextBox
            // 
            this.codeTextBox.BackColor = System.Drawing.SystemColors.Window;
            this.codeTextBox.Location = new System.Drawing.Point(304, 6);
            this.codeTextBox.Name = "codeTextBox";
            this.codeTextBox.Size = new System.Drawing.Size(128, 20);
            this.codeTextBox.TabIndex = 7;
            // 
            // solveMazeButton
            // 
            this.solveMazeButton.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.solveMazeButton.Location = new System.Drawing.Point(231, 598);
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
            this.stepCountLabel.Location = new System.Drawing.Point(263, 644);
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
            this.importMazeButton.Location = new System.Drawing.Point(266, 29);
            this.importMazeButton.Name = "importMazeButton";
            this.importMazeButton.Size = new System.Drawing.Size(80, 20);
            this.importMazeButton.TabIndex = 12;
            this.importMazeButton.Text = "Import";
            this.importMazeButton.UseVisualStyleBackColor = true;
            this.importMazeButton.Click += new System.EventHandler(this.generateMazeFromCodeButton_Click);
            // 
            // exportMazeButton
            // 
            this.exportMazeButton.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.exportMazeButton.Location = new System.Drawing.Point(352, 29);
            this.exportMazeButton.Name = "exportMazeButton";
            this.exportMazeButton.Size = new System.Drawing.Size(80, 20);
            this.exportMazeButton.TabIndex = 13;
            this.exportMazeButton.Text = "Export";
            this.exportMazeButton.UseVisualStyleBackColor = true;
            this.exportMazeButton.Click += new System.EventHandler(this.exportMazeButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(684, 661);
            this.Controls.Add(this.exportMazeButton);
            this.Controls.Add(this.importMazeButton);
            this.Controls.Add(this.stepCountLabel);
            this.Controls.Add(this.solveMazeButton);
            this.Controls.Add(this.codeTextBox);
            this.Controls.Add(this.codeLabel);
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
        private System.Windows.Forms.Label codeLabel;
        private System.Windows.Forms.TextBox codeTextBox;
        private System.Windows.Forms.Button solveMazeButton;
        private System.Windows.Forms.Label stepCountLabel;
        private System.Windows.Forms.Button importMazeButton;
        private System.Windows.Forms.Button exportMazeButton;
    }
}

