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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
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
            this.complexityLabel = new System.Windows.Forms.Label();
            this.complexityUpDown = new System.Windows.Forms.NumericUpDown();
            this.exportAsImageButton = new System.Windows.Forms.Button();
            this.visualiseGenerationCheckbox = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.widthUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.heightUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.complexityUpDown)).BeginInit();
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
            this.stepCountLabel.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.stepCountLabel.Location = new System.Drawing.Point(186, 643);
            this.stepCountLabel.Name = "stepCountLabel";
            this.stepCountLabel.Size = new System.Drawing.Size(150, 13);
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
            // complexityLabel
            // 
            this.complexityLabel.AutoSize = true;
            this.complexityLabel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.complexityLabel.Location = new System.Drawing.Point(544, 602);
            this.complexityLabel.Name = "complexityLabel";
            this.complexityLabel.Size = new System.Drawing.Size(88, 15);
            this.complexityLabel.TabIndex = 15;
            this.complexityLabel.Text = "Complexity (%)";
            // 
            // complexityUpDown
            // 
            this.complexityUpDown.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.complexityUpDown.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.complexityUpDown.Location = new System.Drawing.Point(547, 620);
            this.complexityUpDown.Name = "complexityUpDown";
            this.complexityUpDown.Size = new System.Drawing.Size(106, 23);
            this.complexityUpDown.TabIndex = 14;
            this.complexityUpDown.Value = new decimal(new int[] {
            80,
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
            // visualiseGenerationCheckbox
            // 
            this.visualiseGenerationCheckbox.AutoSize = true;
            this.visualiseGenerationCheckbox.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.visualiseGenerationCheckbox.Location = new System.Drawing.Point(39, 642);
            this.visualiseGenerationCheckbox.Name = "visualiseGenerationCheckbox";
            this.visualiseGenerationCheckbox.Size = new System.Drawing.Size(132, 17);
            this.visualiseGenerationCheckbox.TabIndex = 17;
            this.visualiseGenerationCheckbox.Text = "Visualise Generation";
            this.visualiseGenerationCheckbox.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(684, 661);
            this.Controls.Add(this.visualiseGenerationCheckbox);
            this.Controls.Add(this.exportAsImageButton);
            this.Controls.Add(this.complexityLabel);
            this.Controls.Add(this.complexityUpDown);
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
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Molesey Maze Generator";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.widthUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.heightUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.complexityUpDown)).EndInit();
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
        private System.Windows.Forms.Label complexityLabel;
        private System.Windows.Forms.NumericUpDown complexityUpDown;
        private System.Windows.Forms.Button exportAsImageButton;
        private System.Windows.Forms.CheckBox visualiseGenerationCheckbox;
    }
}

