using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace maze_nea
{
    public static class Generator
    {
        public static void newEmpty(Maze maze, MainForm form)
        {
            int width = maze.Width;
            int height = maze.Height;
            maze.ClearNodes();
            int nodeCount = width * height;
            maze.SetNodeSize(Math.Min(540 / width, 540 / height)); // This calculates the maximum size each node can be to fit in the 550x550 panel (with some padding)
            for (int i = 0; i < width * height; i++)
            {
                Node node = new Node(15); // The binary equivalent of 15 is 1111, meaning all walls are present, this is necessary for Prim's algorithm (which removes walls to create a maze)
                node.SetX(i % width); // Calculate column
                node.SetY(i / width); // Calculate row
                node.SetIndex(i);
                maze.AddNode(node);
            }
            form.drawMaze();
        }
        public static async void runPrims(Maze maze, MainForm form, Panel panel, int complexity, bool visualise)
        {
            int startIndex = new Random().Next(0, maze.Nodes.Count); // Random starting node to branch out from
            List<int> primaryIndexes = new List<int>(); // Nodes that are part of the maze
            List<int> frontierIndexes = new List<int>(); // Nodes that are adjacent to primary nodes
            Color primaryColour = Color.White;
            Color frontierColour = Color.PaleGreen;

            void GetFrontierIndexes(int index)
            {
                int x = index % maze.Width; // Column
                int y = index / maze.Width; // Row

                if (x != maze.Width - 1) AddFrontier(index + 1);
                if (x != 0) AddFrontier(index - 1);
                if (y != maze.Height - 1) AddFrontier(index + maze.Width);
                if (y != 0) AddFrontier(index - maze.Width);
            }

            void AddFrontier(int index)
            {
                if (!primaryIndexes.Contains(index) && !frontierIndexes.Contains(index))
                {
                    frontierIndexes.Add(index);
                    panel.Controls[index].BackColor = frontierColour;
                }
            }

            primaryIndexes.Add(startIndex); // Turn the starting node into a primary node
            panel.Controls[startIndex].BackColor = primaryColour;
            GetFrontierIndexes(startIndex);

            Random random = new Random();
            // This block of code randomly picks a frontier node, connects it to a random adjacent primary node, then turns into a primary node
            // The random selection allows for branches, preventing long corridors
            while (frontierIndexes.Count > 0)
            {
                if (visualise) // If visualisation is enabled, a small delay will be added to allow the user to see the generation process
                {
                    await Task.Delay(10);
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
                            maze.Nodes[primaryIndex].RemoveTopValue();
                            maze.Nodes[frontierIndex].RemoveBottomValue();
                            break;
                        case 1:
                            maze.Nodes[primaryIndex].RemoveRightValue();
                            maze.Nodes[frontierIndex].RemoveLeftValue();
                            break;
                        case 2:
                            maze.Nodes[primaryIndex].RemoveBottomValue();
                            maze.Nodes[frontierIndex].RemoveTopValue();
                            break;
                        case 3:
                            maze.Nodes[primaryIndex].RemoveLeftValue();
                            maze.Nodes[frontierIndex].RemoveRightValue();
                            break;
                        case -1:
                            MessageBox.Show("Invalid direction");
                            break;
                    }
                    // Invalidate forces the wall to disappear on the UI
                    panel.Controls[primaryIndex].Invalidate();
                    panel.Controls[frontierIndex].Invalidate();

                    primaryIndexes.Add(frontierIndex);
                    frontierIndexes.Remove(frontierIndex);

                    panel.Controls[frontierIndex].BackColor = primaryColour; // Paint as a primary node

                    GetFrontierIndexes(frontierIndex);
                }
            }

            //Block to create a start and exit (on two of the edge nodes on opposite ends to increase path length)
            switch (random.Next(0, 2))
            {
                case 0:
                    int startNodeIndex = random.Next(0, maze.Width);
                    maze.SetStartNodeIndex(startNodeIndex);
                    maze.Nodes[startNodeIndex].RemoveTopValue();
                    int endNodeIndex = maze.Width * (maze.Height - 1) + random.Next(0, maze.Width);
                    maze.SetEndNodeIndex(endNodeIndex);
                    maze.Nodes[endNodeIndex].RemoveBottomValue();
                    maze.Nodes[endNodeIndex].ExitNode = true;
                    maze.Nodes[startNodeIndex].ExitNode = true;
                    break;
                case 1:
                    startNodeIndex = random.Next(0, maze.Height) * maze.Width + (maze.Width - 1);
                    maze.SetStartNodeIndex(startNodeIndex);
                    maze.Nodes[startNodeIndex].RemoveRightValue();
                    endNodeIndex = maze.Width * random.Next(0, maze.Height);
                    maze.SetEndNodeIndex(endNodeIndex);
                    maze.Nodes[endNodeIndex].RemoveLeftValue();
                    maze.Nodes[endNodeIndex].ExitNode = true;
                    maze.Nodes[startNodeIndex].ExitNode = true;
                    break;
            }

            // Prim's algorithm creates a "Perfect Maze" (a maze with no loops and only one solution)
            // This block removes additional walls to create loops and multiple solutions, and the proportion of walls removed is up to the user
            for (int i = 0; i < (maze.Width * maze.Height * (100 - complexity)) / 100; i++)
            {
                int randomNodeIndex = random.Next(0, maze.Nodes.Count);
                int x = randomNodeIndex % maze.Width; // Column
                int y = randomNodeIndex / maze.Width; // Row
                List<int> possibleDirections = new List<int>();

                if (y != 0 && maze.Nodes[randomNodeIndex].GetTopValue())
                {
                    possibleDirections.Add(0);
                }
                if (x != maze.Width - 1 && maze.Nodes[randomNodeIndex].GetRightValue())
                {
                    possibleDirections.Add(1);
                }
                if (y != maze.Height - 1 && maze.Nodes[randomNodeIndex].GetBottomValue())
                {
                    possibleDirections.Add(2);
                }
                if (x != 0 && maze.Nodes[randomNodeIndex].GetLeftValue())
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
                            maze.Nodes[randomNodeIndex].RemoveTopValue();
                            maze.Nodes[neighbourIndex].RemoveBottomValue();
                            break;
                        case 1:
                            neighbourIndex = randomNodeIndex + 1;
                            maze.Nodes[randomNodeIndex].RemoveRightValue();
                            maze.Nodes[neighbourIndex].RemoveLeftValue();
                            break;
                        case 2:
                            neighbourIndex = randomNodeIndex + maze.Width;
                            maze.Nodes[randomNodeIndex].RemoveBottomValue();
                            maze.Nodes[neighbourIndex].RemoveTopValue();
                            break;
                        case 3:
                            neighbourIndex = randomNodeIndex - 1;
                            maze.Nodes[randomNodeIndex].RemoveLeftValue();
                            maze.Nodes[neighbourIndex].RemoveRightValue();
                            break;
                    }
                    if (neighbourIndex != -1)
                    {
                        panel.Controls[neighbourIndex].Invalidate();
                        panel.Controls[randomNodeIndex].Invalidate();
                        if (visualise)
                        {
                            panel.Controls[neighbourIndex].BackColor = Color.LightSalmon;
                            panel.Controls[randomNodeIndex].BackColor = Color.LightSalmon;
                            await Task.Delay(30);
                            panel.Controls[neighbourIndex].BackColor = Color.White;
                            panel.Controls[randomNodeIndex].BackColor = Color.White;
                        }

                    }
                }
            }
            //Set colours back to default
            foreach (Control control in panel.Controls)
            {
                control.BackColor = Color.White;
            }
            FileManager.GenerateMazeCode(maze); // Generate a corresponding code for the maze
            form.SetControls(true);
            maze.SetGenerated(true);
        }
        
    }
}
