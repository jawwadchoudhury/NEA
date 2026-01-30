using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace maze_nea
{
    public static class AStar
    {
        public static List<int> Solve(Maze maze, int startIndex, int endIndex)
        {
            try
            {
                // A* pathfinding is an informed search, using heuristics to guide the search,
                // making it significantly faster than Dijkstra's or BFS for large mazes

                Graph graph = GenerateGraph(maze); // Generate an optimised graph from the maze for pathfinding (this is much faster than using the maze directly)

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
                        return VisualPath(maze, totalPath);
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
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred whilst solving the maze: " + ex.Message);
                return null;
            }
        }
        private static Graph GenerateGraph(Maze maze)
        {
            Graph graph = new Graph();
            DynamicStack<int> toVisit = new DynamicStack<int>(); // Using a custom DynamicStack to manage memory efficiently
            HashSet<int> visited = new HashSet<int>();
            // Use node 0 as the starting point for graph generation (there will always be a node with index 0 in any valid maze)
            toVisit.Push(0);
            visited.Add(0);
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

                        if (walkerNode.IsKeyNode()) // If a key node is found, stop walking, add the weighted edge to the graph (with the neighbour index)
                        {
                            graph.AddEdge(currentIndex, walkerIndex, weight);
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
        private static List<int> VisualPath(Maze maze, List<int> path)
        {
            try
            {
                List<int> visualPath = new List<int>();
                visualPath.Add(path[0]); // Add the starting node
                // Because of the graph's compression, it must be iterated over to fill in the gaps between vertices to show on the UI
                for (int i = 0; i < path.Count - 1; i++)
                {
                    Node nodeA = maze.Nodes[path[i]];     // Current vertex
                    Node nodeB = maze.Nodes[path[i + 1]]; // Next vertex


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
                        visualPath.Add(stepIndex);
                    }
                }

                return visualPath;
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred during path visualisation: " + ex.Message);
                return null;
            }
        }
    }
}
