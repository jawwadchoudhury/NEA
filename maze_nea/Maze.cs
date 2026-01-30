using System.Collections.Generic;

namespace maze_nea
{
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
        public void AddNode(Node node)
        {
            Nodes.Add(node);
        }
        public void ClearNodes()
        {
            Nodes.Clear();
        }
        public void SetWidth(int width)
        {
            Width = width;
        }
        public void SetHeight(int height)
        {
            Height = height;
        }
        public void SetNodeSize(int nodeSize)
        {
            NodeSize = nodeSize;
        }
        public void SetEndNodeIndex(int endNodeIndex)
        {
            EndNodeIndex = endNodeIndex;
        }
        public void SetStartNodeIndex(int startNodeIndex)
        {
            StartNodeIndex = startNodeIndex;
        }
        public void SetGenerated(bool generated)
        {
            Generated = generated;
        }

        public void SetCode(string code)
        {
            Code = code;
        }
    }
}
