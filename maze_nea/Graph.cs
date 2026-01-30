using System.Collections.Generic;

namespace maze_nea
{
    public class Graph
    {
        // I'm using an adjacency list rather than an adjacency matrix as it's more memory efficient for sparse graphs
        // Dictionaries also have O(1) average time complexity

        // Key = Node index
        // Value = List of arrays, each array contains a neighbour node index, and the weight of the edge to that neighbour
        public Dictionary<int, List<int[]>> adjacencyList = new Dictionary<int, List<int[]>>();

        public void AddEdge(int startNodeIndex, int endNodeIndex, int weight)
        {
            if (!adjacencyList.ContainsKey(startNodeIndex)) // If the node hasn't been added yet
            {
                adjacencyList[startNodeIndex] = new List<int[]>(); // Initalise its list
            }

            adjacencyList[startNodeIndex].Add(new int[] { endNodeIndex, weight }); // Store the neighbour index and weight as an array, this is later used to calculate the G score in A* pathfinding
        }
    }
}
