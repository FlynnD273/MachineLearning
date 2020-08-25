using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MachineLearning
{
    public partial class Form1 : Form
    {
        private Level level;
        private Player player;

        private bool up = false;
        private bool down = false;
        private bool left = false;
        private bool right = false;

        public Form1()
        {
            InitializeComponent();
            DoubleBuffered = true;
            WindowState = FormWindowState.Maximized;

            level = new Level("Level.txt", "Checkpoints.txt");
            player = new Player(250, 250);

            Timer t = new Timer();
            t.Interval = 10;
            t.Tick += Frame;
            t.Start();
        }

        private void Frame(object sender, EventArgs e)
        {
            player.Input(up, down, left, right);
            player.UpdateLoc();
            if (player.CheckCollisions(level.Walls))
            {
                player.Reset(250, 250, 0);
            }
            player.Color = player.CheckCollisions(level.Checkpoints) ? Color.ForestGreen : Color.Coral;

            player.Sense(level.Walls);

            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            level.Paint(e.Graphics);
            player.Paint(e.Graphics);
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            level.Save("Walls.txt", "Checkpoints.txt");
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                level.Checkpoints.Add(new Line(e.Location, e.Location));
            }
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                level.Checkpoints[level.Checkpoints.Count - 1].b = e.Location;
                //Invalidate();
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                left = true;
            }
            if (e.KeyCode == Keys.Up)
            {
                up = true;
            }
            if (e.KeyCode == Keys.Down)
            {
                down = true;
            }
            if (e.KeyCode == Keys.Right)
            {
                right = true;
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                left = false;
            }
            if (e.KeyCode == Keys.Up)
            {
                up = false;
            }
            if (e.KeyCode == Keys.Down)
            {
                down = false;
            }
            if (e.KeyCode == Keys.Right)
            {
                right = false;
            }
        }
    }
}
