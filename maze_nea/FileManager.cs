using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace maze_nea
{
    public static class FileManager
    {
        public static void GenerateMazeCode(Maze maze)
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
            maze.SetCode(code.ToString());
        }
        public static bool LoadMazeFromCode(Maze maze, MainForm form, Panel panel, string code)
        {
            // Corrupted/invalid codes are handled using a try block
            try
            {
                if (code.Length < 10) // This is the very minimum length for a valid code (including headers)
                {
                    MessageBox.Show("Code is invalid!");
                    return false;
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
                    return false;
                }

                // Update the current maze with the new parameters
                maze.SetWidth(width);
                maze.SetHeight(height);
                maze.SetStartNodeIndex(startNodeIndex);
                maze.SetEndNodeIndex(endNodeIndex);

                // Generate an empty maze with these dimensions
                Generator.newEmpty(maze, form);

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
                                node.SetDecimalValue(decimalValue);

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
                foreach (Control control in panel.Controls)
                {
                    control.BackColor = Color.White;
                }
                panel.Refresh(); // Refresh the maze panel
                maze.SetCode(code); // If the user wants to export this maze again, they can import it without any errors being thrown
                maze.SetGenerated(true);
                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show("An error occurred while loading the maze: " + e.Message);
                return false;
            }
        }
        public static void SaveAsImage(Maze maze, string filePath, Pen pen)
        {
            try
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
                bitmap.Save(filePath, ImageFormat.Png);
                bitmap.Dispose(); // Free up memory used by the bitmap
            }
            catch (Exception e)
            {
                MessageBox.Show("An error occurred while saving the image: " + e.Message);
            }
        }
        public static void WriteMazeCode(Maze maze, string fileName)
        {
            try
            {
                StreamWriter streamWriter = new StreamWriter(fileName); // Create a stream writer to write data to the file
                streamWriter.Write(maze.Code); // Writes the RLE encoded maze code to the file
                // Close the streams to prevent file locking
                streamWriter.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show("An error occurred while exporting the maze: " + e.Message);
            }
        }
    }
}
