using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace maze_nea
{
    public partial class Form1 : Form
    {
        Maze maze = new Maze(3, 3);
        Pen pen = new Pen(Color.Black, 4);
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
            public bool Generated { get; private set; } = false;
            public string Code { get; private set; }
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
            public void setGenerated(bool generated)
            {
                Generated = generated;
            }

            public void setCode(string code)
            {
                Code = code;
            }
        }
        public class Node
        {
            // Wall states are represented by the 4-bit integer DecimalValue,
            // as it saves significantly more memory than 4 seperate booleans, one for each side.
            // First bit = Top wall (0001)
            // Second bit = Right wall (0010)
            // Third bit = Bottom wall (0100)
            // Fourth bit = Left wall (1000)
            public int DecimalValue { get; private set; }
            public int X { get; private set; }
            public int Y { get; private set; }
            public int Index { get; private set; }
            public bool ExitNode { get; set; } // Flags this node as a key node for graph generation and forces it to be considered as a vertex, instead of part of an edge

            public Node(int decimalValue)
            {
                DecimalValue = decimalValue;
            }
            // The get methods use the bitwise AND operator to check if a specific bit is set to 1 (there is a wall)
            public bool getTopValue()
            {
                return ((uint)DecimalValue & 1) == 1; 
            }
            public bool getRightValue()
            {
                return ((uint)DecimalValue >> 1 & 1) == 1; // This uses a bitwise right shift to reference a different bit
            }
            public bool getBottomValue()
            {
                return ((uint)DecimalValue >> 2 & 1) == 1;
            }
            public bool getLeftValue()
            {
                return ((uint)DecimalValue >> 3 & 1) == 1;
            }
            // The toggle methods use bitwise manipulation to flip specific bits using the XOR operator
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
            // The remove methods use the AND NOT operator to set a specific bit to 0 (remove wall)
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
            // The Draw method uses GDI+ (System.Drawing) to render walls based on the DecimalValue's state
            // Co-ordinates are relative to the top-left of the node (0, 0 is top left)
            public void Draw(Graphics g, int NodeSize, Pen pen)
            {
                if (((uint)DecimalValue & 1) == 1)
                {
                    g.DrawLine(pen, new Point(0, 0), new Point(NodeSize, 0));
                }
                if (((uint)DecimalValue >> 1 & 1) == 1)
                {
                    g.DrawLine(pen, new Point(NodeSize, 0), new Point(NodeSize, NodeSize));
                }
                if (((uint)DecimalValue >> 2 & 1) == 1)
                {
                    g.DrawLine(pen, new Point(0, NodeSize), new Point(NodeSize, NodeSize));
                }
                if (((uint)DecimalValue >> 3 & 1) == 1)
                {
                    g.DrawLine(pen, new Point(0, 0), new Point(0, NodeSize));
                }
            }
        }
        public class Graph
        {
            // I'm using an adjacency list rather than an adjacency matrix as it's more memory efficient for sparse graphs
            // Dictionaries also have O(1) average time complexity

            // Key = Node index
            // Value = List of arrays, each array contains a neighbour node index, and the weight of the edge to that neighbour
            public Dictionary<int, List<int[]>> adjacencyList = new Dictionary<int, List<int[]>>();

            public void addEdge(int startNodeIndex, int endNodeIndex, int weight)
            {
                if (!adjacencyList.ContainsKey(startNodeIndex)) // If the node hasn't been added yet
                {
                    adjacencyList[startNodeIndex] = new List<int[]>(); // Initalise its list
                }
                
                adjacencyList[startNodeIndex].Add(new int[] { endNodeIndex, weight }); // Store the neighbour index and weight as an array, this is later used to calculate the G score in A* pathfinding
            }
        }
        public class DynamicStack<T> // Using Generics to allow for a dynamic stack of any type
        {
            private T[] elements;
            private int count;
            private int capacity;
            public int getCount()
            {
                return count;
            }

            public DynamicStack(int initialCapacity = 1)
            {
                capacity = initialCapacity;
                elements = new T[capacity];
                count = 0;
            }

            public void Push(T item)
            {
                if (count == capacity)
                {
                    capacity *= 2; // Using array doubling to reduce the resize frequency, reducing copy operations and ulitmately average time complexity to O(1)
                    T[] newElements = new T[capacity];
                    Array.Copy(elements, newElements, count);
                    elements = newElements;
                }

                elements[count] = item;
                count++;
            }

            public T Pop()
            {
                if (count == 0) throw new InvalidOperationException("Stack is empty.");
                count--; 
                T item = elements[count];
                elements[count] = default(T); // Setting this index to default value to prevent memory leaks, and allows the garbage collector to reclaim memory
                return item;
            }

            public T Peek()
            {
                if (count == 0) throw new InvalidOperationException("Stack is empty.");
                return elements[count - 1];
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            generateEmptyMaze(maze.Width, maze.Height);
        }
        private void generateEmptyMaze(int width, int height)
        {
            maze.clearNodes();
            int nodeCount = width * height;
            maze.setNodeSize(Math.Min(540 / width, 540 / height)); // This calculates the maximum size each node can be to fit in the 550x550 panel (with some padding)
            for (int i = 0; i < width * height; i++)
            {
                Node node = new Node(15); // The binary equivalent of 15 is 1111, meaning all walls are present, this is necessary for Prim's algorithm (which removes walls to create a maze)
                node.setX(i % width); // Calculate column
                node.setY(i / width); // Calculate row
                node.setIndex(i);
                maze.addNode(node);
            }
            drawMaze();
        }
        private void LoadMazeFromCode(string code)
        {
            // Corrupted/invalid codes are handled using a try block
            try
            {
                if (code.Length < 10) // This is the very minimum length for a valid code (including headers)
                {
                    MessageBox.Show("Code is invalid!");
                    return;
                }

                // -65 converts ASCII character to decimal value (A=65 in ASCII, so A=0 regarding width/height.)
                int width = code[0] - 65; 
                int height = code[1] - 65;
                // Next 3 characters are the start node index, following 3 characters are the end node index (padded 3 digit numbers)
                int startNodeIndex = int.Parse(code.Substring(2, 3));
                int endNodeIndex = int.Parse(code.Substring(5, 3));

                // Prevents invalid maze dimensions and invalid start/end node indexes
                if (width < 3 || height < 3 || width > 25 || height > 25 || startNodeIndex < 0 || endNodeIndex < 0 ||
                    startNodeIndex >= width * height || endNodeIndex >= width * height)
                {
                    MessageBox.Show("Code is invalid!");
                    return;
                }
                else
                {
                    // Update the UI to match the loaded maze dimensions
                    widthUpDown.Value = width;
                    heightUpDown.Value = height;
                }

                // Update the current maze with the new parameters
                maze.setWidth(width);
                maze.setHeight(height);
                maze.setStartNodeIndex(startNodeIndex);
                maze.setEndNodeIndex(endNodeIndex);

                // Generate an empty maze with these dimensions
                generateEmptyMaze(width, height);

                int currentNodeIndex = 0;
                string numberBuffer = "";

                // This block decodes the maze data that uses RLE (run-length encoding) to compress the data (eg. 3A = AAA)
                for (int i = 8; i < code.Length; i++) // The 8th character index is the first character of the maze data (after the headers)
                {
                    char c = code[i];

                    if (char.IsDigit(c)) // Digits represent repetitions, so we store them in a buffer to accumulate multi-digit numbers
                    {
                        numberBuffer += c;
                    }
                    else
                    {
                        int decimalValue = c - 65; // Convert character to decimal value that represents wall state (A=0, B=1 ... P=15)

                        int repeatCount = (numberBuffer == "") ? 1 : int.Parse(numberBuffer); // If the buffers empty, it means the count is 1, otherwise parse the buffer as an integer

                        for (int j = 0; j < repeatCount; j++) // Repeat for the specified count
                        {
                            if (currentNodeIndex < maze.Nodes.Count)
                            {
                                Node node = maze.Nodes[currentNodeIndex];
                                node.setDecimalValue(decimalValue);

                                // Make sure the start and end nodes are marked as exit nodes (and considered vertexes during graph generation)
                                if (currentNodeIndex == startNodeIndex || currentNodeIndex == endNodeIndex)
                                {
                                    node.ExitNode = true;
                                }

                                currentNodeIndex++;
                            }
                        }

                        numberBuffer = ""; // Clear the buffer for the next time a digit appears
                    }
                }

                // Set each node's background colour to white
                foreach (Control control in mazePanel.Controls)
                {
                    control.BackColor = Color.White;
                } 
                mazePanel.Refresh(); // Refresh the maze panel
                maze.setGenerated(true);
            }
            catch (Exception e)
            {
                MessageBox.Show("An error occurred while loading the maze: " + e.Message);
            }
        }
        private async void primsAlgorithm(int wallsToRemovePercent)
        {
            int startIndex = new Random().Next(0, maze.Nodes.Count); // Random starting node to branch out from
            List<int> primaryIndexes = new List<int>(); // Nodes that are part of the maze
            List<int> frontierIndexes = new List<int>(); // Nodes that are adjacent to primary nodes
            Color primaryColour = Color.White;
            Color frontierColour = Color.PaleGreen;

            void getFrontierIndexes(int index)
            {
                int x = index % maze.Width; // Column
                int y = index / maze.Width; // Row

                if (x != maze.Width - 1) addFrontier(index + 1);
                if (x != 0) addFrontier(index - 1);
                if (y != maze.Height - 1) addFrontier(index + maze.Width);
                if (y != 0) addFrontier(index - maze.Width);
            }

            void addFrontier(int index)
            {
                if (!primaryIndexes.Contains(index) && !frontierIndexes.Contains(index))
                {
                    frontierIndexes.Add(index);
                    mazePanel.Controls[index].BackColor = frontierColour;
                }
            }

            primaryIndexes.Add(startIndex); // Turn the starting node into a primary node
            mazePanel.Controls[startIndex].BackColor = primaryColour;
            getFrontierIndexes(startIndex);

            Random random = new Random();
            // This block of code randomly picks a frontier node, connects it to a random adjacent primary node, then turns into a primary node
            // The random selection allows for branches, preventing long corridors
            while (frontierIndexes.Count > 0)
            {
                if (visualiseGenerationCheckbox.Checked) // If visualisation is enabled, a small delay will be added to allow the user to see the generation process
                {
                    await Task.Delay(1);
                }
                int frontierIndex = frontierIndexes[new Random().Next(0, frontierIndexes.Count)]; // Picks a random frontier node
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
                    // Invalidate forces the wall to disappear on the UI
                    mazePanel.Controls[primaryIndex].Invalidate();
                    mazePanel.Controls[frontierIndex].Invalidate();

                    primaryIndexes.Add(frontierIndex);
                    frontierIndexes.Remove(frontierIndex);

                    mazePanel.Controls[frontierIndex].BackColor = primaryColour; // Paint as a primary node

                    getFrontierIndexes(frontierIndex);
                }
            }

            //Block to create a start and exit (on two of the edge nodes on opposite ends to increase path length)
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
            // Prim's algorithm creates a "Perfect Maze" (a maze with no loops and only one solution)
            // This block removes additional walls to create loops and multiple solutions, and the proportion of walls removed is up to the user
            if (wallsToRemovePercent != 0) // Makes sure it doesn't attempt to divide by 0, otherwise there'll be an error
            {
                for (int i = 0; i < (maze.Width * maze.Height) / (100 / wallsToRemovePercent); i++)
                {
                    int randomNodeIndex = random.Next(0, maze.Nodes.Count);
                    int x = randomNodeIndex % maze.Width; // Column
                    int y = randomNodeIndex / maze.Width; // Row
                    List<int> possibleDirections = new List<int>();

                    if (y != 0 && maze.Nodes[randomNodeIndex].getTopValue())
                    {
                        possibleDirections.Add(0);
                    }
                    if (x != maze.Width - 1 && maze.Nodes[randomNodeIndex].getRightValue())
                    {
                        possibleDirections.Add(1);
                    }
                    if (y != maze.Height - 1 && maze.Nodes[randomNodeIndex].getBottomValue())
                    {
                        possibleDirections.Add(2);
                    }
                    if (x != 0 && maze.Nodes[randomNodeIndex].getLeftValue())
                    {
                        possibleDirections.Add(3);
                    }

                    if (possibleDirections.Count > 0)
                    {
                        
                        int direction = possibleDirections[random.Next(0, possibleDirections.Count)];
                        int neighbourIndex = -1;
                        switch (direction)
                        {
                            case 0:
                                neighbourIndex = randomNodeIndex - maze.Width;
                                maze.Nodes[randomNodeIndex].removeTopValue();
                                maze.Nodes[neighbourIndex].removeBottomValue();
                                break;
                            case 1:
                                neighbourIndex = randomNodeIndex + 1;
                                maze.Nodes[randomNodeIndex].removeRightValue();
                                maze.Nodes[neighbourIndex].removeLeftValue();
                                break;
                            case 2:
                                neighbourIndex = randomNodeIndex + maze.Width;
                                maze.Nodes[randomNodeIndex].removeBottomValue();
                                maze.Nodes[neighbourIndex].removeTopValue();
                                break;
                            case 3:
                                neighbourIndex = randomNodeIndex - 1;
                                maze.Nodes[randomNodeIndex].removeLeftValue();
                                maze.Nodes[neighbourIndex].removeRightValue();
                                break;
                        }
                        if (neighbourIndex != -1)
                        {
                            
                            mazePanel.Controls[neighbourIndex].Invalidate();
                            mazePanel.Controls[randomNodeIndex].Invalidate();
                            if (visualiseGenerationCheckbox.Checked)
                            {
                                mazePanel.Controls[neighbourIndex].BackColor = Color.LightSalmon;
                                mazePanel.Controls[randomNodeIndex].BackColor = Color.LightSalmon;
                                await Task.Delay(30);
                                mazePanel.Controls[neighbourIndex].BackColor = Color.White;
                                mazePanel.Controls[randomNodeIndex].BackColor = Color.White;
                            }

                        }
                    }
                }
            }
            //Set colours back to default
            foreach (Control control in mazePanel.Controls)
            {
                control.BackColor = Color.White;
            }
            generateMazeCode(); // Generate a corresponding code for the maze
            setControls(true);
            maze.setGenerated(true);
        }
        private void generateMazeCode()
        {
            StringBuilder code = new StringBuilder(maze.Height * maze.Width);
            // These append lines add the header information to the code (and means the header will always be 8 characters long)
            code.Append(Convert.ToChar(maze.Width + 65));
            code.Append(Convert.ToChar(maze.Height + 65));
            code.Append(maze.StartNodeIndex.ToString().PadLeft(3, '0'));
            code.Append(maze.EndNodeIndex.ToString().PadLeft(3, '0'));

            
            int repeatCount = 1;
            char previousChar = Convert.ToChar(maze.Nodes[0].DecimalValue + 65);

            // This block encodes the maze data using RLE (run-length encoding) to lower file size (eg. 3A = AAA)
            for (int i = 1; i < maze.Nodes.Count; i++)
            {
                char currentChar = Convert.ToChar(maze.Nodes[i].DecimalValue + 65);
                if (currentChar == previousChar)
                {
                    repeatCount++;
                }
                else
                {
                    if (repeatCount > 1) code.Append(repeatCount);
                    code.Append(previousChar);
                    previousChar = currentChar;
                    repeatCount = 1;
                }
            }
            if (repeatCount > 1) code.Append(repeatCount);
            code.Append(previousChar);
            maze.setCode(code.ToString());
        }
        private void drawMaze()
        {
            mazePanel.Controls.Clear(); // Resets the UI to prevent memory leaks
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
        private Graph generateGraph(int startingNodeIndex)
        {
            Graph graph = new Graph();
            DynamicStack<int> toVisit = new DynamicStack<int>(); // Using a custom DynamicStack to manage memory efficiently
            HashSet<int> visited = new HashSet<int>();
            toVisit.Push(startingNodeIndex);
            visited.Add(startingNodeIndex);
            // Using bitmasks and offsets reduces if statements, improving performance and code readability
            int[] bitmasks = new int[4] { 1, 2, 4, 8 };
            int[] offsets = new int[4] { -maze.Width, 1, maze.Width, -1 };
            while (toVisit.getCount() > 0)
            {
                int currentIndex = toVisit.Pop(); // Handle this index, and remove it from the stack
                Node currentNode = maze.Nodes[currentIndex];
                for (int direction = 0; direction < 4; direction++)
                {
                    if ((currentNode.DecimalValue & bitmasks[direction]) != 0) continue; // If there is a wall in that direction, it means it is untraversable - skip it.

                    // Walk down a 'corridor' in the specified direction until a key node is found, and mark it as a neighbour in the graph
                    int walkerIndex = currentIndex;
                    int weight = 0;
                    while (true)
                    {
                        if ((maze.Nodes[walkerIndex].DecimalValue & bitmasks[direction]) != 0) break; // Check if there is a wall in that direction

                        int walkerX = walkerIndex % maze.Width;
                        // These lines prevent the walker from wrapping
                        if (direction == 1 && walkerX == maze.Width - 1) break; // Check for right edge
                        if (direction == 3 && walkerX == 0) break; // Check for left edge

                        walkerIndex += offsets[direction]; // Move the walker in that direction
                        weight++; // Increment the edge weight

                        if (walkerIndex < 0 || walkerIndex >= maze.Nodes.Count) break; // Out of bounds check

                        Node walkerNode = maze.Nodes[walkerIndex];

                        if (walkerNode.isKeyNode()) // If a key node is found, stop walking, add the weighted edge to the graph (with the neighbour index)
                        {
                            graph.addEdge(currentIndex, walkerIndex, weight);
                            if (!visited.Contains(walkerIndex))
                            {
                                toVisit.Push(walkerIndex);
                                visited.Add(walkerIndex);
                            }
                            break; // Stop walking
                        }
                    }
                }
            }
            return graph;
        }
        private List<int> AStarPath(int startIndex, int endIndex)
        {
            // A* pathfinding is an informed search, using heuristics to guide the search,
            // making it significantly faster than Dijkstra's or BFS for large mazes

            Graph graph = generateGraph(startIndex); // Generate an optimised graph from the maze for pathfinding (this is much faster than using the maze directly)

            List<int> frontierNodes = new List<int>() { startIndex }; // Nodes that still need to be evaluated
            List<int> processedNodes = new List<int>(); // Fully evaluated nodes

            Dictionary<int, int> gScores = new Dictionary<int, int>(); // The actual cost to get from the start node to the current node
            Dictionary<int, int> fScores = new Dictionary<int, int>(); // Estimated total cost from start to end (via current node, calculated using gScore + Heuristic)
            Dictionary<int, int> pathDictionary = new Dictionary<int, int>(); // Used to reconstruct the path once the end node is reached

            int Heuristic(int nodeAIndex, int nodeBIndex) // Using Manhattan distance as the heuristic function, as movement is restricted to 4 directions (diagonals not allowed, so euclidean distance is unsuitable) 
            {
                Node nodeA = maze.Nodes[nodeAIndex];
                Node nodeB = maze.Nodes[nodeBIndex];
                return Math.Abs(nodeA.X - nodeB.X) + Math.Abs(nodeA.Y - nodeB.Y); // Manhattan distance (|dx| + |dy|)
            }

            while (frontierNodes.Count > 0)
            {
                // Find the node in frontierNodes with the lowest F score
                // This means the most 'promising' node is always evaluated next
                int currentNodeIndex = frontierNodes[0];
                int lowestScore = fScores.ContainsKey(currentNodeIndex) ? fScores[currentNodeIndex] : int.MaxValue; // (int.MaxValue is (2^31) - 1, the maximum value for an integer, meaning there is always a lower score)
                foreach (int index in frontierNodes)
                {
                    int score = fScores.ContainsKey(index) ? fScores[index] : int.MaxValue;
                    if (score < lowestScore)
                    {
                        lowestScore = score;
                        currentNodeIndex = index;
                    }
                }

                // If the current node is the end node, reconstruct the path and return it
                if (currentNodeIndex == endIndex)
                {
                    List<int> totalPath = new List<int>();
                    int temporaryIndex = currentNodeIndex;
                    totalPath.Add(temporaryIndex);
                    while (pathDictionary.ContainsKey(temporaryIndex)) // Backtrack through the parents in the path dictionary to reconstruct the full path
                    {
                        temporaryIndex = pathDictionary[temporaryIndex];
                        totalPath.Add(temporaryIndex);
                    }
                    totalPath.Reverse(); // Reverse the path to get the correct order (Start -> End)
                    return totalPath;
                }


                frontierNodes.Remove(currentNodeIndex);
                processedNodes.Add(currentNodeIndex); // Mark the current node as fully evaluated

                foreach (int[] neighbour in graph.adjacencyList[currentNodeIndex]) // For each neighbour of the current node
                {
                    int neighbourIndex = neighbour[0];
                    int weight = neighbour[1]; // The weight of the edge to this neighbour
                    {
                        if (processedNodes.Contains(neighbourIndex)) continue; // If it's already evaluated, skip it

                        int tentativeGScore = (gScores.ContainsKey(currentNodeIndex) ? gScores[currentNodeIndex] : 0) + weight; // Calculate the cost to reach that neighbour via the current node

                        if (!frontierNodes.Contains(neighbourIndex)) frontierNodes.Add(neighbourIndex); // Add to frontier nodes if it's not already there (to continue iteration)

                        if (tentativeGScore < (gScores.ContainsKey(neighbourIndex) ? gScores[neighbourIndex] : int.MaxValue)) // If this path to this neighbour is better than any previously evaluated path, update its scores and parent
                        {
                            pathDictionary[neighbourIndex] = currentNodeIndex; // Record the best path
                            gScores[neighbourIndex] = tentativeGScore;
                            fScores[neighbourIndex] = tentativeGScore + Heuristic(neighbourIndex, endIndex);
                        }
                    }
                }
            }

            return null; // No path found (unlikely)
        }
        private void pictureBox_Paint(object sender, PaintEventArgs e)
        {
            PictureBox pictureBox = (PictureBox)sender; // Sender class is polymorphic, and must be casted to a PictureBox to get its properties
            Node node = (Node)pictureBox.Tag; // The Tag property stores the node object, and allows access to its DecimalValue property (which determines which walls are drawn)
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias; // Anti-aliasing for better visuals
            node.Draw(e.Graphics, maze.NodeSize, pen); // Uses encapsulation to call the draw method with the graphics context of the picture box, allowing the node to draw its own walls
        }
        private void setControls(Boolean enabled) // Locks the GUI during certain actions to stop user interference causing errors/crashes
        {
            widthUpDown.Enabled = enabled;
            heightUpDown.Enabled = enabled;
            generateMazeButton.Enabled = enabled;
            solveMazeButton.Enabled = enabled;
            importMazeButton.Enabled = enabled;
            exportMazeButton.Enabled = enabled;
            wallsRemovedUpDown.Enabled = enabled;
            exportAsImageButton.Enabled = enabled;
            visualiseGenerationCheckbox.Enabled = enabled;
        }
        private void generateMazeButton_Click(object sender, EventArgs e)
        {
            setControls(false);
            maze.setGenerated(false);
            stepCountLabel.Visible = false;
            generateEmptyMaze(maze.Width, maze.Height); // Reset the grid
            primsAlgorithm((int)wallsRemovedUpDown.Value); // Run Prim's algorithm to generate the maze
        }
        private void widthUpDown_ValueChanged(object sender, EventArgs e)
        {
            maze.setWidth((int)widthUpDown.Value);
            stepCountLabel.Visible = false;
            generateEmptyMaze(maze.Width, maze.Height);
        }
        private void heightUpDown_ValueChanged(object sender, EventArgs e)
        {
            maze.setHeight((int)heightUpDown.Value);
            stepCountLabel.Visible = false;
            generateEmptyMaze(maze.Width, maze.Height);
        }
        private void solveMazeButton_Click(object sender, EventArgs e)
        {
            List<int> path = AStarPath(maze.StartNodeIndex, maze.EndNodeIndex); // Get the shortest path using A* pathfinding, but this path only returns key nodes, not all the steps

            if (path != null && path.Count > 0 && maze.Generated)
            {
                int totalSteps = 2; // Start and end nodes are included in the step count
                // Because of the graph's compression, it must be iterated over to fill in the gaps between vertices to show on the UI
                for (int i = 0; i < path.Count - 1; i++)
                {
                    Node nodeA = maze.Nodes[path[i]];     // Current vertex
                    Node nodeB = maze.Nodes[path[i + 1]]; // Next vertex

                    mazePanel.Controls[nodeA.Index].BackColor = Color.LightBlue; // Colour the start node

                    totalSteps += Math.Abs(nodeB.X - nodeA.X) + Math.Abs(nodeB.Y - nodeA.Y); // Add the weight (Manhattan distance) between the two vertices to the step count

                    // Returns -1, 0 or 1 depending on the sign of the difference, and tells which direction to travel in
                    int dx = Math.Sign(nodeB.X - nodeA.X);
                    int dy = Math.Sign(nodeB.Y - nodeA.Y);

                    int currentX = nodeA.X;
                    int currentY = nodeA.Y;

                    while (currentX != nodeB.X || currentY != nodeB.Y) // Keep walking until co-ordinates for the next node is reached
                    {
                        currentX += dx;
                        currentY += dy;

                        int stepIndex = currentY * maze.Width + currentX; // Convert the co-ordinates back to an index
                        mazePanel.Controls[stepIndex].BackColor = Color.LightBlue; // Colour each step in the path
                    }
                }

                mazePanel.Controls[path[path.Count - 1]].BackColor = Color.LightBlue; // Colour the end node
                stepCountLabel.Visible = true;
                stepCountLabel.Text = "Steps: " + totalSteps.ToString();
            }
        }
        private void importMazeButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog(); // Using the Windows file dialog
            fileDialog.Filter = "Maze Files|*.maze"; // Limit to .maze files to prevent user error

            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                Stream stream = fileDialog.OpenFile(); 
                StreamReader streamReader = new StreamReader(stream); // Open a file stream to read the file data
                string code = streamReader.ReadToEnd(); // Load the entire file as a string into memory

                // Close the streams to prevent file locking
                streamReader.Close();
                stream.Close();

                LoadMazeFromCode(code); // Call a seperate method to load the maze from the code
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
                Stream stream = fileDialog.OpenFile(); // Open a file stream 
                StreamWriter streamWriter = new StreamWriter(stream); // Create a stream writer to write data to the file
                streamWriter.Write(maze.Code); // Writes the RLE encoded maze code to the file

                // Close the streams to prevent file locking
                streamWriter.Close();
                stream.Close();
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
                // Creates a bitmap in memory with the corresponding dimensions of the maze
                int imageWidth = maze.Width * maze.NodeSize;
                int imageHeight = maze.Height * maze.NodeSize;
                Bitmap bitmap = new Bitmap(imageWidth, imageHeight);

                
                using (Graphics g = Graphics.FromImage(bitmap)) // Using statement allows for instant disposal of the graphics object after use, preventing memory leaks
                {
                    g.Clear(Color.Transparent); // Set background to transparent
                    foreach (Node node in maze.Nodes)
                    {
                        g.TranslateTransform(node.X * maze.NodeSize, node.Y * maze.NodeSize); // Use the top left corner of each node as the origin for drawing
                        node.Draw(g, maze.NodeSize, pen); // Draw the node relative to the new origin
                        g.TranslateTransform(-node.X * maze.NodeSize, -node.Y * maze.NodeSize); // Reset the origin for the next node
                    }
                }
                bitmap.Save(fileDialog.FileName, ImageFormat.Png);
                bitmap.Dispose(); // Free up memory used by the bitmap
            }
        }
    }
}
