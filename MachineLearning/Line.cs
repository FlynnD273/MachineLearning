using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace MachineLearning
{
    class Line
    {
        public Point a;
        public Point b;

        public Line(Point a, Point b)
        {
            this.a = a;
            this.b = b;
        }

        public Line(int ax, int ay, int bx, int by) : this(new Point(ax, ay), new Point(bx, by)) { }

        public static bool IntersectsRect(Line l, Rectangle r)
        {
            return Intersects(l, new Line(r.X, r.Y, r.X + r.Width, r.Y)) ||
                   Intersects(l, new Line(r.X + r.Width, r.Y, r.X + r.Width, r.Y + r.Height)) ||
                   Intersects(l, new Line(r.X + r.Width, r.Y + r.Height, r.X, r.Y + r.Height)) ||
                   Intersects(l, new Line(r.X, r.Y + r.Height, r.X, r.Y)) ||
                   (r.Contains(l.a) && r.Contains(l.b));
        }

        public static bool Intersects(Line l1, Line l2)
        {
            float q = (l1.a.Y - l2.a.Y) * (l2.b.X - l2.a.X) - (l1.a.X - l2.a.X) * (l2.b.Y - l2.a.Y);
            float d = (l1.b.X - l1.a.X) * (l2.b.Y - l2.a.Y) - (l1.b.Y - l1.a.Y) * (l2.b.X - l2.a.X);

            if (d == 0)
            {
                return false;
            }

            float r = q / d;

            q = (l1.a.Y - l2.a.Y) * (l1.b.X - l1.a.X) - (l1.a.X - l2.a.X) * (l1.b.Y - l1.a.Y);
            float s = q / d;

            if (r < 0 || r > 1 || s < 0 || s > 1)
            {
                return false;
            }

            return true;
        }

        public static Point IntersectPoint(Line l1, Line l2)
        {
            double m1 = (double)(l1.a.Y - l1.b.Y) / (l1.a.X - l1.b.X);
            double m2 = (double)(l2.a.Y - l2.b.Y) / (l2.a.X - l2.b.X);
            if (double.IsInfinity(m1))
            {
                m1 = 200;
            }
            if (double.IsInfinity(m2))
            {
                m2 = 200;
            }

            double b1 = l1.a.Y - l1.a.X * m1;
            double b2 = l2.a.Y - l2.a.X * m2;

            double x = (b1 - b2) / (m2 - m1);
            double y = m1 * x + b1;

            if (m1 == m2)
            {
                return new Point(0, 0);
            }

            return new Point((int)x, (int)y);
        }
    }
}
