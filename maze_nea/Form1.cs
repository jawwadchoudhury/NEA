using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace maze_nea
{
    public partial class Form1 : Form
    {
        Maze maze = new Maze(3, 3);
        Pen pen = new Pen(Color.Black, 4);
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
            public bool ExitNode { get; set; }

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
            public Boolean isKeyNode()
            {
                if (ExitNode) return true;
                if (getWallCount() < 2) return true;
                if (getWallCount() == 2 && !(getTopValue() && getBottomValue()) && !(getLeftValue() && getRightValue())) return true;
                return false;
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
        public class Graph 
        {
            public Dictionary<int, List<int[]>> adjacencyList = new Dictionary<int, List<int[]>>();

            public void addEdge(int startNodeIndex, int endNodeIndex, int weight)
            {
                if (!adjacencyList.ContainsKey(startNodeIndex))
                {
                    adjacencyList[startNodeIndex] = new List<int[]>();
                }
                adjacencyList[startNodeIndex].Add(new int[] { endNodeIndex, weight });
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
                await Task.Delay(5);
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
                    maze.Nodes[endNodeIndex].ExitNode = true;
                    maze.Nodes[startNodeIndex].ExitNode = true;
                    break;
                case 1:
                    startNodeIndex = random.Next(0, maze.Height) * maze.Width + (maze.Width - 1);
                    maze.setStartNodeIndex(startNodeIndex);
                    maze.Nodes[startNodeIndex].removeRightValue();
                    endNodeIndex = maze.Width * random.Next(0, maze.Height);
                    maze.setEndNodeIndex(endNodeIndex);
                    maze.Nodes[endNodeIndex].removeLeftValue();
                    maze.Nodes[endNodeIndex].ExitNode = true;
                    maze.Nodes[startNodeIndex].ExitNode = true;
                    break;
            }
            foreach (Control control in mazePanel.Controls)
            {
                control.BackColor = Color.White;
            }
            isGenerating = false;
            setControls(true);
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
        private Graph generateGraph(int startingNodeIndex)
        {
            Graph graph = new Graph();
            Stack<int> toVisit = new Stack<int>();
            HashSet<int> visited = new HashSet<int>();
            toVisit.Push(startingNodeIndex);
            int[] bitmasks = new int[4] { 1, 2, 4, 8 }; // Up, Right, Down, Left
            int[] offsets = new int[4] { -maze.Width, 1, maze.Width, -1 }; // Up, Right, Down, Left
            while (toVisit.Count > 0)
            {
                int currentIndex = toVisit.Pop();
                Node currentNode = maze.Nodes[currentIndex];
                for (int direction = 0; direction < 4; direction++)
                {
                    if ((currentNode.DecimalValue & bitmasks[direction]) != 0) continue;
                    int walkerIndex = currentIndex;
                    int weight = 0;
                    while (true)
                    {
                        if ((maze.Nodes[walkerIndex].DecimalValue & bitmasks[direction]) != 0) break;

                        walkerIndex += offsets[direction];
                        weight++;

                        if (walkerIndex < 0 || walkerIndex >= maze.Nodes.Count) break; // Out of bounds

                        Node walkerNode = maze.Nodes[walkerIndex];

                        if (walkerNode.isKeyNode())
                        {
                            graph.addEdge(currentIndex, walkerIndex, weight);
                            if (!visited.Contains(walkerIndex))
                            {
                                toVisit.Push(walkerIndex);
                                visited.Add(walkerIndex);
                            }
                            break;
                        }
                    }
                }
            }
            return graph;
        }
        private List<int> AStarPath(int startIndex, int endIndex)
        {
            Graph graph = generateGraph(startIndex);

            // The set of currently discovered nodes that are not evaluated yet.
            // We assume the start node is known.
            List<int> openSet = new List<int> { startIndex };

            // For node n, cameFrom[n] is the node immediately preceding it on the cheapest path
            Dictionary<int, int> cameFrom = new Dictionary<int, int>();

            // gScore[n] is the cost of the cheapest path from start to n.
            Dictionary<int, int> gScore = new Dictionary<int, int>();
            gScore[startIndex] = 0;

            // fScore[n] := gScore[n] + h(n). Estimated total cost from start to goal through n.
            Dictionary<int, int> fScore = new Dictionary<int, int>();
            fScore[startIndex] = Heuristic(startIndex, endIndex);

            // 2. The Loop
            while (openSet.Count > 0)
            {
                // Find node in openSet with the lowest fScore
                int current = openSet[0];
                foreach (int nodeIndex in openSet)
                {
                    int score = fScore.ContainsKey(nodeIndex) ? fScore[nodeIndex] : int.MaxValue;
                    int currentScore = fScore.ContainsKey(current) ? fScore[current] : int.MaxValue;
                    if (score < currentScore) current = nodeIndex;
                }

                // Did we reach the goal?
                if (current == endIndex)
                {
                    return ReconstructPath(cameFrom, current);
                }

                openSet.Remove(current);

                // 3. Check Neighbors using your Graph Adjacency List
                // Note: graph.adjacencyList might be private, you might need to make it public 
                // or add a getter in your Graph class: public Dictionary<int, List<int[]>> GetAdjacencyList() { return adjacencyList; }

                if (graph.adjacencyList.ContainsKey(current))
                {
                    foreach (int[] edge in graph.adjacencyList[current])
                    {
                        int neighbor = edge[0]; // The neighbor's index
                        int weight = edge[1];   // The distance to that neighbor

                        // tentative_gScore is the distance from start to the neighbor through current
                        int tentative_gScore = gScore[current] + weight;

                        int neighborGScore = gScore.ContainsKey(neighbor) ? gScore[neighbor] : int.MaxValue;

                        if (tentative_gScore < neighborGScore)
                        {
                            // This path to neighbor is better than any previous one. Record it!
                            cameFrom[neighbor] = current;
                            gScore[neighbor] = tentative_gScore;
                            fScore[neighbor] = gScore[neighbor] + Heuristic(neighbor, endIndex);

                            if (!openSet.Contains(neighbor))
                            {
                                openSet.Add(neighbor);
                            }
                        }
                    }
                }
            }
            return null;
        }
        private List<int> ReconstructPath(Dictionary<int, int> cameFrom, int current)
        {
            List<int> totalPath = new List<int> { current };
            while (cameFrom.ContainsKey(current))
            {
                current = cameFrom[current];
                totalPath.Insert(0, current); // Add to front
            }
            return totalPath;
        }
        private int Heuristic(int nodeIndexA, int nodeIndexB)
        {
            Node nodeA = maze.Nodes[nodeIndexA];
            Node nodeB = maze.Nodes[nodeIndexB];
            return Math.Abs(nodeA.X - nodeB.X) + Math.Abs(nodeA.Y - nodeB.Y);
        }
        private void pictureBox_Paint(object sender, PaintEventArgs e)
        {
            PictureBox pictureBox = (PictureBox)sender;
            Node node = (Node)pictureBox.Tag;
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            node.Draw(e.Graphics, maze.NodeSize, pen);
        }
        private void setControls(Boolean enabled)
        {
            widthUpDown.Enabled = enabled;
            heightUpDown.Enabled = enabled;
            generateMazeButton.Enabled = enabled;
            seedTextBox.Enabled = enabled;
            solveMazeButton.Enabled = enabled;
        }
        private void generateMazeButton_Click(object sender, EventArgs e)
        {
            setControls(false);
            generateEmptyMaze(maze.Width, maze.Height);
            primsAlgorithm();
        }

        private void widthUpDown_ValueChanged(object sender, EventArgs e)
        {
            maze.setWidth((int)widthUpDown.Value);
            generateEmptyMaze(maze.Width, maze.Height);
        }

        private void heightUpDown_ValueChanged(object sender, EventArgs e)
        {
            maze.setHeight((int)heightUpDown.Value);
            generateEmptyMaze(maze.Width, maze.Height);
        }

        private void solveMazeButton_Click(object sender, EventArgs e)
        {
            List<int> path = AStarPath(maze.StartNodeIndex, maze.EndNodeIndex);
            foreach (int index in path)
            {
                mazePanel.Controls[index].BackColor = Color.LightBlue;
            }
        }
    }
}
