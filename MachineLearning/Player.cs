using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace MachineLearning
{
    class Player
    {
        public Point Loc { get; set; }
        public double Rot { get; set; }
        public double ForwardVel { get; set; }
        public double TurnVel { get; set; }
        public Color Color { get; set; }

        private int r = 50;

        private double[] sensors;
        private int sensorLength = 300;
        public Player (int x, int y)
        {
            Loc = new Point(x, y);
            Rot = 0;
            ForwardVel = 0;
            TurnVel = 0;
            sensors = new double[5];
        }

        public void Paint(Graphics g)
        {
            for (int i = 0; i < sensors.Length; i++)
            {
                g.DrawLine(new Pen(Brushes.Red, 10), Loc, ProjectPolar(Loc, Rot - 90 + i * 180 / (sensors.Length - 1), sensors[i] * sensorLength));
            }

            g.TranslateTransform(Loc.X, Loc.Y);
            g.RotateTransform((float)Rot);
            g.FillRectangle(new SolidBrush(Color), -r / 2, -r / 2, r, r);
            g.ResetTransform();
        }

        public void UpdateLoc()
        {
            Rot += TurnVel;
            TurnVel *= 0.9;
            Loc = ProjectPolar(Loc, Rot, ForwardVel);
            ForwardVel *= 0.9;
        }

        public void Reset(int x, int y, double dir)
        {
            Loc = new Point(x, y);
            Rot = dir;
            ForwardVel = 0;
            TurnVel = 0;
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

        public bool CheckCollisions(List<Line> lines)
        {
            foreach (Line l in lines)
            {
                if (Line.IntersectsRect(l, new Rectangle(new Point(Loc.X - r/2, Loc.Y - r/2), new Size(r, r))))
                {
                    return true;
                }
            }
            return false;
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
