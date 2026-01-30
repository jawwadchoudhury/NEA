using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;

namespace maze_nea
{
    public partial class MainForm : Form
    {
        Maze maze = new Maze(3, 3);
        Pen pen = new Pen(Color.Black, 4);
        bool isImporting = false;
        public MainForm()
        {
            InitializeComponent();
        }
        
        private void Form1_Load(object sender, EventArgs e)
        {
            Generator.newEmpty(maze, this);
        }
        
        
        public void drawMaze()
        {
            mazePanel.Controls.Clear(); // Clears the current controls ahead of redrawing
            int nodeSize = maze.NodeSize;
            int mazeWidth = maze.Width * nodeSize;
            int mazeHeight = maze.Height * nodeSize;
            // Dynamically change the pen width (wall thickness) based on the node size, this makes the maze look better at different sizes
            switch (nodeSize)
            {
                case int n when (n >= 40):
                    pen.Width = 7;
                    break;
                case int n when (n >= 30):
                    pen.Width = 5;
                    break;
                default:
                    pen.Width = 4;
                    break;
            }

            for (int i = 0; i < maze.Nodes.Count; i++)
            {
                Node node = maze.Nodes[i];
                PictureBox pictureBox = new PictureBox();
                pictureBox.Location = new Point(node.X * nodeSize, node.Y * nodeSize); // Position the picture box relative to the node size
                pictureBox.Size = new Size(nodeSize, nodeSize);
                pictureBox.Tag = node; // This stores the node object in the picture box's Tag property. Used later when drawing the walls
                pictureBox.BackColor = Color.Gray;
                pictureBox.Paint += pictureBox_Paint; // Attach the paint event handler to each picture box
                mazePanel.Controls.Add(pictureBox);
            }
        }
        
        private void pictureBox_Paint(object sender, PaintEventArgs e)
        {
            PictureBox pictureBox = (PictureBox)sender; // Sender class is polymorphic, and must be casted to a PictureBox to get its properties
            Node node = (Node)pictureBox.Tag; // The Tag property stores the node object, and allows access to its DecimalValue property (which determines which walls are drawn)
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias; // Anti-aliasing for better visuals
            node.Draw(e.Graphics, maze.NodeSize, pen); // Uses encapsulation to call the draw method with the graphics context of the picture box, allowing the node to draw its own walls
        }
        public void SetControls(Boolean enabled) // Locks the GUI during certain actions to stop user interference causing errors/crashes
        {
            widthUpDown.Enabled = enabled;
            heightUpDown.Enabled = enabled;
            generateMazeButton.Enabled = enabled;
            solveMazeButton.Enabled = enabled;
            importMazeButton.Enabled = enabled;
            exportMazeButton.Enabled = enabled;
            complexityUpDown.Enabled = enabled;
            exportAsImageButton.Enabled = enabled;
            visualiseGenerationCheckbox.Enabled = enabled;
        }
        private void generateMazeButton_Click(object sender, EventArgs e)
        {
            SetControls(false);
            maze.SetGenerated(false);
            Generator.newEmpty(maze, this); // Reset the grid
            stepCountLabel.Visible = false; // Hide the step count label when generating a new maze
            Generator.runPrims(maze, this, mazePanel, (int)complexityUpDown.Value, visualiseGenerationCheckbox.Checked); // Run Prim's algorithm to generate the maze
        }
        private void widthUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (isImporting) return; // Stops the maze from resetting whilst importing is taking place
            maze.SetWidth((int)widthUpDown.Value);
            stepCountLabel.Visible = false;
            Generator.newEmpty(maze, this);
        }
        private void heightUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (isImporting) return; // Stops the maze from resetting whilst importing is taking place
            maze.SetHeight((int)heightUpDown.Value);
            stepCountLabel.Visible = false;
            Generator.newEmpty(maze, this);
        }
        private void solveMazeButton_Click(object sender, EventArgs e)
        {
            List<int> path = AStar.Solve(maze, maze.StartNodeIndex, maze.EndNodeIndex); // Get the shortest path using A* pathfinding, but this path only returns key nodes, not all the steps

            if (path != null && path.Count > 0 && maze.Generated)
            {
                foreach (int index in path)
                {
                    mazePanel.Controls[index].BackColor = Color.LightBlue; // Colour each node in the path light blue
                }
                stepCountLabel.Visible = true;
                stepCountLabel.Text = $"Steps: {path.Count + 1}"; // Take the jumps into account when displaying the step count
            }
        }
        private void importMazeButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog(); // Using the Windows file dialog
            fileDialog.Filter = "Maze Files|*.maze"; // Limit to .maze files to prevent user error
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                stepCountLabel.Visible = false; // Hide the step count label when importing a new maze
                Stream stream = fileDialog.OpenFile(); 
                StreamReader streamReader = new StreamReader(stream); // Open a file stream to read the file data
                string code = streamReader.ReadToEnd(); // Load the entire file as a string into memory

                // Close the streams to prevent file locking
                streamReader.Close();
                stream.Close();

                bool loaded = FileManager.LoadMazeFromCode(maze, this, mazePanel, code); // Call a seperate method to load the maze from the code
                if (loaded)
                {
                    isImporting = true;
                    // Update the controls to match the loaded maze
                    widthUpDown.Value = maze.Width;
                    heightUpDown.Value = maze.Height;
                    isImporting = false;
                    
                }
                
            }
        }
        private void exportMazeButton_Click(object sender, EventArgs e)
        {
            if (!maze.Generated) // Defensive programming to prevent exporting when there is no maze
            {
                MessageBox.Show("There is no maze to export!");
                return;
            }
            SaveFileDialog fileDialog = new SaveFileDialog(); // Using the Windows file dialog
            fileDialog.Filter = "Maze Files|*.maze";

            // Generate a unique default filename based on the current datetime and maze dimensions
            // Follows the format "DD-MM-YYYY_HH-MM-SS_WIDTHxHEIGHT.maze"
            // This prevents overwriting files, and easy for Molesey News to identify when mazes were created
            DateTime now = DateTime.Now;
            fileDialog.FileName = $"{now.Day.ToString().PadLeft(2, '0')}-{now.Month.ToString().PadLeft(2, '0')}-{now.Year}_{now.Hour.ToString().PadLeft(2, '0')}-{now.Minute.ToString().PadLeft(2, '0')}-{now.Second.ToString().PadLeft(2, '0')}_{maze.Width}x{maze.Height}";

            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                FileManager.WriteMazeCode(maze, fileDialog.FileName); // Call a seperate method to write the maze code to a file
            }
        }
        private void exportAsImageButton_Click(object sender, EventArgs e)
        {
            if (!maze.Generated) // Defensive programming to prevent exporting when there is no maze
            {
                MessageBox.Show("There is no maze to export!");
                return;
            }
            SaveFileDialog fileDialog = new SaveFileDialog();
            fileDialog.Filter = "PNG Image|*.png";

            // Uses a unique default filename based on the current datetime and maze dimensions
            DateTime now = DateTime.Now;
            fileDialog.FileName = $"{now.Day.ToString().PadLeft(2, '0')}-{now.Month.ToString().PadLeft(2, '0')}-{now.Year}_{now.Hour.ToString().PadLeft(2, '0')}-{now.Minute.ToString().PadLeft(2, '0')}-{now.Second.ToString().PadLeft(2, '0')}_{maze.Width}x{maze.Height}";
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                FileManager.SaveAsImage(maze, fileDialog.FileName, pen);
            }
        }
    }
}
