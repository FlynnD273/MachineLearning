using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace MachineLearning
{
    class Player
    {
        private Point startLoc;

        public Point PrevLoc { get; private set; }
        public Point Loc { get; private set; }
        public double Rot { get; private set; }
        public double ForwardVel { get; private set; }
        public double TurnVel { get; private set; }
        public Color Color { get; set; }
        public bool Active { get; private set; }
        public NeuralNet Net { get; set; }

        private int r = 50;

        private double[] sensors;
        private int sensorLength = 300;
        private List<int> checkpoints;

        public Player (int x, int y)
        {
            checkpoints = new List<int>();
            Loc = new Point(x, y);
            Rot = 0;
            ForwardVel = 0;
            TurnVel = 0;
            sensors = new double[8];
            Net = new NeuralNet(new int[] { sensors.Length, 16, 16, 4 });
            startLoc = Loc;
            PrevLoc = Loc;
        }

        public void Paint(Graphics g)
        {
            if (Active)
            {
                //for (int i = 0; i < sensors.Length; i++)
                //{
                //    g.DrawLine(new Pen(Brushes.Red, 2), Loc, ProjectPolar(Loc, Rot - 90 + i * 180 / (sensors.Length - 1), sensors[i] * sensorLength));
                //}

                g.TranslateTransform(Loc.X, Loc.Y);
                g.RotateTransform((float)Rot);
                g.FillRectangle(new SolidBrush(Color), -r / 2, -r / 2, r, r);
                g.ResetTransform();
            }
        }

        internal void Frame(Level level)
        {
            if (Active)
            {
                Sense(level.Walls);

                Net.FeedForward(sensors);
                Point prevLoc = Loc;
                Input(Net.Output[0] > 0.5, Net.Output[1] > 0.5, Net.Output[2] > 0.5, Net.Output[3] > 0.5);
                UpdateLoc();
                Net.Fitness += ForwardVel / 20;
                //Net.Fitness += ForwardVel / 10 - 2;

                if (CheckCollisions(level.Walls).Item1 || Math.Round(ForwardVel, 1) == 0)
                {
                    Active = false;
                }
                var checkpointCollision = CheckCollisions(level.Checkpoints);
                //Color = checkpoint ? Color.ForestGreen : Color.Coral;
                Color = Color.DarkBlue;

                if (checkpointCollision.Item1 && !checkpoints.Contains(checkpointCollision.Item2))
                {
                    if (checkpoints.Count == level.Checkpoints.Count)
                    {
                        checkpoints.Clear();
                    }
                    checkpoints.Add(checkpointCollision.Item2);
                    Net.Fitness += 1500 / level.Checkpoints.Count;
                }
            }
        }

        public void UpdateLoc()
        {
            PrevLoc = Loc;
            Rot += TurnVel;
            TurnVel *= 0.9;
            Loc = ProjectPolar(Loc, Rot, ForwardVel);
            ForwardVel *= 0.9;
        }

        public void Reset()
        {
            Loc = startLoc;
            Rot = 0;
            ForwardVel = 0;
            TurnVel = 0;
            Active = true;
        }

        public void Sense(List<Line> walls)
        {
            for (int i = 0; i < sensors.Length; i++)
            {
                sensors[i] = 1;
                Line sensor = new Line(Loc, ProjectPolar(Loc, Rot - 90 + i * 180 / (sensors.Length - 1), sensorLength));

                foreach (Line l in walls)
                {
                    if (Line.Intersects(l, sensor))
                    {
                        double dist = Dist(Loc, Line.IntersectPoint(l, sensor));
                        sensors[i] = Math.Min(dist / sensorLength, sensors[i]);
                    }
                }
            }
        }

        private double Dist (Point a, Point b)
        {
            return Math.Sqrt((a.X - b.X) * (a.X - b.X) + (a.Y - b.Y) * (a.Y - b.Y));
        }

        public Tuple<bool, int> CheckCollisions(List<Line> lines)
        {
            for (int i = 0; i < lines.Count; i++)
            {
                Line l = lines[i];
                if (Line.IntersectsRect(l, new Rectangle(new Point(Loc.X - r/2, Loc.Y - r/2), new Size(r, r))))
                {
                    return Tuple.Create(true, i);
                }
            }
            return Tuple.Create(false, -1);
        }

        public void Input(bool up, bool down, bool left, bool right)
        {
            Move((up ? 1 : 0) - (down ? 1: 0), (right ? 0.6 : 0) - (left ? 0.6 : 0));
        }

        public void Move(double vert, double hor)
        {
            ForwardVel += vert;
            TurnVel += hor;
        }

        private double ToRadians(double val)
        {
            return (Math.PI / 180) * val;
        }

        private Point ProjectPolar(Point loc, double angle, double radius)
        {
            return new Point(loc.X + (int)(Math.Cos(ToRadians(angle)) * radius), loc.Y + (int)(Math.Sin(ToRadians(angle)) * radius));
        }
    }
}
