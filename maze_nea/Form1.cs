using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace maze_nea
{
    public partial class Form1 : Form
    {
        Maze maze = new Maze(3, 3);
        Pen pen = new Pen(Color.Black, 5);
        Boolean isGenerating = false;

        public Form1()
        {
            InitializeComponent();
        }

        public class Maze
        {
            public List<Node> Nodes { get; private set; } = new List<Node>();
            public int Width { get; private set; }
            public int Height { get; private set; }
            public int NodeSize { get; private set; }
            public int StartNodeIndex { get; private set; }
            public int EndNodeIndex { get; private set; }
            public Maze(int width, int height)
            {
                Width = width;
                Height = height;
            }
            public void addNode(Node node)
            {
                Nodes.Add(node);
            }
            public void clearNodes()
            {
                Nodes.Clear();
            }
            public void setWidth(int width)
            {
                Width = width;
            }
            public void setHeight(int height)
            {
                Height = height;
            }
            public void setNodeSize(int nodeSize)
            {
                NodeSize = nodeSize;
            }
            public void setEndNodeIndex(int endNodeIndex)
            {
                EndNodeIndex = endNodeIndex;
            }
            public void setStartNodeIndex(int startNodeIndex)
            {
                StartNodeIndex = startNodeIndex;
            }
        }
        public class Node
        {
            public int DecimalValue { get; private set; }
            public int X { get; private set; }
            public int Y { get; private set; }
            public int Index { get; private set; }

            public Node(int decimalValue)
            {
                DecimalValue = decimalValue;
            }
            public bool getTopValue()
            {
                return ((uint)DecimalValue & 1) == 1;
            }
            public bool getRightValue()
            {
                return ((uint)DecimalValue >> 1 & 1) == 1;
            }
            public bool getBottomValue()
            {
                return ((uint)DecimalValue >> 2 & 1) == 1;
            }
            public bool getLeftValue()
            {
                return ((uint)DecimalValue >> 3 & 1) == 1;
            }
            public void toggleTopValue()
            {
                DecimalValue ^= 1;
            }
            public void toggleRightValue()
            {
                DecimalValue ^= 2;
            }
            public void toggleBottomValue()
            {
                DecimalValue ^= 4;
            }
            public void toggleLeftValue()
            {
                DecimalValue ^= 8;
            }
            public void removeTopValue()
            {
                DecimalValue &= ~1;
            }
            public void removeRightValue()
            {
                DecimalValue &= ~2;
            }
            public void removeBottomValue()
            {
                DecimalValue &= ~4;
            }
            public void removeLeftValue()
            {
                DecimalValue &= ~8;
            }
            public void setDecimalValue(int decimalValue)
            {
                DecimalValue = decimalValue;
            }
            private int getWallCount()
            {
                int wallCount = 0;
                if (getTopValue()) wallCount++;
                if (getRightValue()) wallCount++;
                if (getBottomValue()) wallCount++;
                if (getLeftValue()) wallCount++;
                return wallCount;
            }
            public void setX(int x)
            {
                X = x;
            }
            public void setY(int y)
            {
                Y = y;
            }
            public void setIndex(int index)
            {
                Index = index;
            }
            public void Draw(Graphics g, int NodeSize, Pen Pen)
            {
                if (((uint)DecimalValue & 1) == 1)
                {
                    g.DrawLine(Pen, new Point(0, 0), new Point(NodeSize, 0));
                }
                if (((uint)DecimalValue >> 1 & 1) == 1)
                {
                    g.DrawLine(Pen, new Point(NodeSize, 0), new Point(NodeSize, NodeSize));
                }
                if (((uint)DecimalValue >> 2 & 1) == 1)
                {
                    g.DrawLine(Pen, new Point(0, NodeSize), new Point(NodeSize, NodeSize));
                }
                if (((uint)DecimalValue >> 3 & 1) == 1)
                {
                    g.DrawLine(Pen, new Point(0, 0), new Point(0, NodeSize));
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            generateEmptyMaze(maze.Width, maze.Height);
        }

        private void generateEmptyMaze(int width, int height)
        {
            maze.clearNodes();
            maze.setNodeSize(Math.Min(540 / width, 540 / height));
            for (int i = 0; i < width * height; i++)
            {
                Node node = new Node(15);
                node.setX(i % width);
                node.setY(i / width);
                node.setIndex(i);
                maze.addNode(node);
            }
            drawMaze();
        }
        private async void primsAlgorithm()
        {
            int startIndex = new Random().Next(0, maze.Nodes.Count);
            List<int> primaryIndexes = new List<int>();
            List<int> frontierIndexes = new List<int>();
            primaryIndexes.Add(startIndex); // Turn the starting node into a primary node

            void getFrontierIndexes(int index)
            {
                int x = index % maze.Width; // Column
                int y = index / maze.Width; // Row

                if (x != maze.Width - 1 && !primaryIndexes.Contains(index + 1) && !frontierIndexes.Contains(index + 1))
                {
                    frontierIndexes.Add(index + 1);
                }
                if (x != 0 && !primaryIndexes.Contains(index - 1) && !frontierIndexes.Contains(index - 1))
                {
                    frontierIndexes.Add(index - 1);
                }
                if (y != maze.Height - 1 && !primaryIndexes.Contains(index + maze.Width) && !frontierIndexes.Contains(index + maze.Width))
                {
                    frontierIndexes.Add(index + maze.Width);
                }
                if (y != 0 && !primaryIndexes.Contains(index - maze.Width) && !frontierIndexes.Contains(index - maze.Width))
                {
                    frontierIndexes.Add(index - maze.Width);
                }
            }
            void refreshColours()
            {
                foreach (Control control in mazePanel.Controls)
                {
                    control.BackColor = Color.Gray;
                }
                foreach (int index in primaryIndexes)
                {
                    mazePanel.Controls[index].BackColor = Color.LightGoldenrodYellow;
                }
                foreach (int index in frontierIndexes)
                {
                    mazePanel.Controls[index].BackColor = Color.LightGreen;
                }
            }

            getFrontierIndexes(startIndex);
            refreshColours();

            Random random = new Random();
            while (frontierIndexes.Count > 0)
            {
                await Task.Delay(20);
                int frontierIndex = frontierIndexes[new Random().Next(0, frontierIndexes.Count)];
                List<int> currentFrontiers = new List<int>();
                int x = frontierIndex % maze.Width; // Column
                int y = frontierIndex / maze.Width; // Row
                if (x != maze.Width - 1 && primaryIndexes.Contains(frontierIndex + 1))
                {
                    currentFrontiers.Add(frontierIndex + 1);
                }
                if (x != 0 && primaryIndexes.Contains(frontierIndex - 1))
                {
                    currentFrontiers.Add(frontierIndex - 1);
                }
                if (y != maze.Height - 1 && primaryIndexes.Contains(frontierIndex + maze.Width))
                {
                    currentFrontiers.Add(frontierIndex + maze.Width);
                }
                if (y != 0 && primaryIndexes.Contains(frontierIndex - maze.Width))
                {
                    currentFrontiers.Add(frontierIndex - maze.Width);
                }

                if (currentFrontiers.Count > 0)
                {
                    int primaryIndex = currentFrontiers[random.Next(0, currentFrontiers.Count)];
                    int getDirection(int indexA, int indexB)
                    {
                        switch (indexA)
                        {
                            case int n when (n - maze.Width == indexB):
                                return 0; // Up
                            case int n when (n + 1 == indexB):
                                return 1; // Right
                            case int n when (n + maze.Width == indexB):
                                return 2; // Down
                            case int n when (n - 1 == indexB):
                                return 3; // Left
                            default:
                                return -1;
                        }
                    }

                    switch (getDirection(primaryIndex, frontierIndex))
                    {
                        case 0:
                            maze.Nodes[primaryIndex].removeTopValue();
                            maze.Nodes[frontierIndex].removeBottomValue();
                            break;
                        case 1:
                            maze.Nodes[primaryIndex].removeRightValue();
                            maze.Nodes[frontierIndex].removeLeftValue();
                            break;
                        case 2:
                            maze.Nodes[primaryIndex].removeBottomValue();
                            maze.Nodes[frontierIndex].removeTopValue();
                            break;
                        case 3:
                            maze.Nodes[primaryIndex].removeLeftValue();
                            maze.Nodes[frontierIndex].removeRightValue();
                            break;
                        case -1:
                            MessageBox.Show("Invalid direction");
                            break;
                    }
                    primaryIndexes.Add(frontierIndex);
                    frontierIndexes.Remove(frontierIndex);
                    getFrontierIndexes(frontierIndex);
                    refreshColours();
                }
            }

            //Block to create a start and exit (on two of the edge nodes, opposite ends)
            switch (random.Next(0, 2))
            {
                case 0:
                    int startNodeIndex = random.Next(0, maze.Width);
                    maze.setStartNodeIndex(startNodeIndex);
                    maze.Nodes[startNodeIndex].removeTopValue();
                    int endNodeIndex = maze.Width * (maze.Height - 1) + random.Next(0, maze.Width);
                    maze.setEndNodeIndex(endNodeIndex);
                    maze.Nodes[endNodeIndex].removeBottomValue();
                    break;
                case 1:
                    startNodeIndex = random.Next(0, maze.Height) * maze.Width + (maze.Width - 1);
                    maze.setStartNodeIndex(startNodeIndex);
                    maze.Nodes[startNodeIndex].removeRightValue();
                    endNodeIndex = maze.Width * random.Next(0, maze.Height);
                    maze.setEndNodeIndex(endNodeIndex);
                    maze.Nodes[endNodeIndex].removeLeftValue();
                    break;
            }
            foreach (Control control in mazePanel.Controls)
            {
                control.BackColor = DefaultBackColor;
            }
            isGenerating = false;
            widthUpDown.Enabled = true;
            heightUpDown.Enabled = true;
            generateMazeButton.Enabled = true;
        }

        private void drawMaze()
        {
            mazePanel.Controls.Clear();
            int nodeSize = maze.NodeSize;
            int mazeWidth = maze.Width * nodeSize;
            int mazeHeight = maze.Height * nodeSize;
            mazePanel.Size = new Size(mazeWidth, mazeHeight);
            for (int i = 0; i < maze.Nodes.Count; i++)
            {
                Node node = maze.Nodes[i];
                PictureBox pictureBox = new PictureBox();
                pictureBox.Location = new Point(node.X * nodeSize, node.Y * nodeSize);
                pictureBox.Size = new Size(nodeSize, nodeSize);
                pictureBox.Tag = node;
                pictureBox.BackColor = Color.Gray;
                pictureBox.Paint += pictureBox_Paint;
                mazePanel.Controls.Add(pictureBox);
            }
        }
        private void pictureBox_Paint(object sender, PaintEventArgs e)
        {
            PictureBox pictureBox = (PictureBox)sender;
            Node node = (Node)pictureBox.Tag;
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            node.Draw(e.Graphics, maze.NodeSize, pen);
        }
        private void generateMazeButton_Click(object sender, EventArgs e)
        {
            if (!isGenerating) {
                isGenerating = true;
                widthUpDown.Enabled = false;
                heightUpDown.Enabled = false;
                generateMazeButton.Enabled = false;
                generateEmptyMaze(maze.Width, maze.Height);
                primsAlgorithm();
            }
        }

        private void widthUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (!isGenerating)
            {
                maze.setWidth((int)widthUpDown.Value);
                generateEmptyMaze(maze.Width, maze.Height);
            }
        }

        private void heightUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (!isGenerating)
            {
                maze.setHeight((int)heightUpDown.Value);
                generateEmptyMaze(maze.Width, maze.Height);
            }
        }
    }
}
