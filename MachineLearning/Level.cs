using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;

namespace MachineLearning
{
    class Level
    {
        public List<Line> Walls { get; set; }
        public List<Line> Checkpoints { get; set; }

        public Level ()
        {
            Walls = new List<Line>();
            Checkpoints = new List<Line>();
        }
        public Level (string wallPath, string checkpointPath)
        {
            Walls = Load(wallPath);
            Checkpoints = Load(checkpointPath);
        }

        public List<Line> Load (string path)
        {
            if (!File.Exists(path))
            {
                return new List<Line>();
            }

            string[] fileText = File.ReadAllLines(path);
            List<Line> Lines = new List<Line>();

            foreach (string line in fileText)
            {
                int[] coords = new int[4];
                string[] coordStrings = line.Split(' ');
                if (coordStrings.Length == 4)
                {
                    for (int i = 0; i < coordStrings.Length; i++)
                    {
                        int num = 0;
                        if (int.TryParse(coordStrings[i], out num))
                        {
                            coords[i] = num;
                        }
                    }
                    Lines.Add(new Line(coords[0], coords[1], coords[2], coords[3]));
                }
            }

            return Lines;
        }

        public void Save (string wallPath, string checkpointPath)
        {
            StringBuilder sb = new StringBuilder();
            foreach (Line l in Walls)
            {
                sb.Append(l.a.X).Append(" ").Append(l.a.Y);
                sb.Append(" ");
                sb.Append(l.b.X).Append(" ").Append(l.b.Y);
                sb.AppendLine();
            }
            sb.Remove(sb.Length - 1, 1);

            File.WriteAllText(wallPath, sb.ToString());

            sb = new StringBuilder();
            foreach (Line l in Checkpoints)
            {
                sb.Append(l.a.X).Append(" ").Append(l.a.Y);
                sb.Append(" ");
                sb.Append(l.b.X).Append(" ").Append(l.b.Y);
                sb.AppendLine();
            }
            sb.Remove(sb.Length - 1, 1);

            File.WriteAllText(checkpointPath, sb.ToString());
        }

        public void Paint (Graphics g)
        {
            foreach (Line l in Walls)
            {
                g.DrawLine(new Pen(Brushes.Black, 5), l.a, l.b);
            }
            foreach (Line l in Checkpoints)
            {
                g.DrawLine(new Pen(Brushes.SteelBlue, 5), l.a, l.b);
            }
        }
    }
}
