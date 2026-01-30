using System;
using System.Drawing;

namespace maze_nea
{
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
        public bool GetTopValue()
        {
            return ((uint)DecimalValue & 1) == 1;
        }
        public bool GetRightValue()
        {
            return ((uint)DecimalValue >> 1 & 1) == 1; // This uses a bitwise right shift to reference a different bit
        }
        public bool GetBottomValue()
        {
            return ((uint)DecimalValue >> 2 & 1) == 1;
        }
        public bool GetLeftValue()
        {
            return ((uint)DecimalValue >> 3 & 1) == 1;
        }
        // The toggle methods use bitwise manipulation to flip specific bits using the XOR operator
        public void ToggleTopValue()
        {
            DecimalValue ^= 1;
        }
        public void ToggleRightValue()
        {
            DecimalValue ^= 2;
        }
        public void ToggleBottomValue()
        {
            DecimalValue ^= 4;
        }
        public void ToggleLeftValue()
        {
            DecimalValue ^= 8;
        }
        // The remove methods use the AND NOT operator to set a specific bit to 0 (remove wall)
        public void RemoveTopValue()
        {
            DecimalValue &= ~1;
        }
        public void RemoveRightValue()
        {
            DecimalValue &= ~2;
        }
        public void RemoveBottomValue()
        {
            DecimalValue &= ~4;
        }
        public void RemoveLeftValue()
        {
            DecimalValue &= ~8;
        }
        public void SetDecimalValue(int decimalValue)
        {
            DecimalValue = decimalValue;
        }
        private int GetWallCount()
        {
            int wallCount = 0;
            if (GetTopValue()) wallCount++;
            if (GetRightValue()) wallCount++;
            if (GetBottomValue()) wallCount++;
            if (GetLeftValue()) wallCount++;
            return wallCount;
        }
        public void SetX(int x)
        {
            X = x;
        }
        public void SetY(int y)
        {
            Y = y;
        }
        public void SetIndex(int index)
        {
            Index = index;
        }
        public Boolean IsKeyNode()
        {
            if (ExitNode) return true;
            if (GetWallCount() < 2) return true;
            if (GetWallCount() == 2 && !(GetTopValue() && GetBottomValue()) && !(GetLeftValue() && GetRightValue())) return true;
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
}
