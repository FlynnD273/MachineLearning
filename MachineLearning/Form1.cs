using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MachineLearning
{
    public partial class Form1 : Form
    {
        private Level level;
        private List<Player> players;
        private Timer mutateTimer;
        private Timer frameTimer;

        private bool runOne = false;

        private bool drawing = false;
        private bool drawWalls = true;

        private List<Tuple<bool, Line>> undoStack = new List<Tuple<bool, Line>>();
        private List<Tuple<bool, Line>> redoStack = new List<Tuple<bool, Line>>();

        public Form1()
        {
            InitializeComponent();
            DoubleBuffered = true;
            WindowState = FormWindowState.Maximized;

            level = new Level("Walls.txt", "Checkpoints.txt");
            players = new List<Player>();

            for (int i = 0; i < 20; i++)
            {
                players.Add(new Player(250, 250));
            }

            frameTimer = new Timer();
            frameTimer.Interval = 10;
            frameTimer.Tick += Frame;
            frameTimer.Start();

            mutateTimer = new Timer();
            mutateTimer.Interval = 10000;
            mutateTimer.Tick += Mutate;
            mutateTimer.Start();
        }

        private void Mutate(object sender, EventArgs e)
        {
            if (!runOne)
            {
                players.Sort((a, b) => a.Net.CompareTo(b.Net));
                players[0].Reset();
                players[0].Net.Fitness = 0;
                for (int i = 1; i < players.Count; i++)
                {
                    //players[i] = new Player(250, 250);
                    players[i].Reset();
                    players[i].Net = players[0].Net.Copy();
                    players[i].Net.Mutate(0.4, 0.5);
                    players[i].Net.Fitness = 0;
                }
            }
        }

        private void Frame(object sender, EventArgs e)
        {
            foreach (Player player in players)
            {
                player.Frame(level);
            }

            if (!runOne)
            {
                players.Sort((a, b) => a.Net.CompareTo(b.Net));
                players[0].Color = Color.Green;

                bool allInactive = true;
                foreach (Player player in players)
                {
                    if (player.Active)
                    {
                        allInactive = false;
                        break;
                    }
                }

                if (allInactive)
                {
                    mutateTimer.Stop();
                    mutateTimer.Start();
                    Mutate(null, null);
                }
            }
            else if (!players[0].Active)
            {
                players[0].Reset();
            }

            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(50, 255, 0, 0)), 225, 225, 50, 50);

            level.Paint(e.Graphics);
            
            foreach (Player player in players)
            {
                player.Paint(e.Graphics);
            }
        }

        private void SaveTrackButton_Click(object sender, EventArgs e)
        {
            File.Copy("Walls.txt", "Walls (old).txt", true);
            File.Copy("Checkpoints.txt", "Checkpoints (old).txt", true);

            level.Save("Walls.txt", "Checkpoints.txt");
        }

        private void NextGenButton_Click(object sender, EventArgs e)
        {
            Mutate(null, null);
            mutateTimer.Stop();
            mutateTimer.Start();
        }

        private void SaveNetButton_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Text Files (*.txt)|*.txt";
            frameTimer.Stop();
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                players.Sort((a, b) => a.Net.CompareTo(b.Net));
                players[0].Net.Save(sfd.FileName);
            }
            frameTimer.Start();
        }

        private void LoadNetButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                players = new List<Player>();
                players.Add(new Player(250, 250));
                players[0].Net.Load(ofd.FileName);
                mutateTimer.Stop();
                runOne = true;
            }
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                level.Checkpoints.Add(new Line(e.Location, e.Location));
                drawWalls = false;
                drawing = true;
                redoStack.Clear();
                undoStack.Add(Tuple.Create(drawWalls, level.Checkpoints.Last()));
            }
            if (e.Button == MouseButtons.Left)
            {
                level.Walls.Add(new Line(e.Location, e.Location));
                drawWalls = true;
                drawing = true;
                redoStack.Clear();
                undoStack.Add(Tuple.Create(drawWalls, level.Walls.Last()));
            }
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right || e.Button == MouseButtons.Left)
            {
                drawing = false;
            }
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (drawing)
            {
                if (drawWalls)
                {
                    level.Walls[level.Walls.Count - 1].b = e.Location;
                }
                else
                {
                    level.Checkpoints[level.Checkpoints.Count - 1].b = e.Location;
                }

                //Invalidate();
            }
        }

        private void UndoButton_Click(object sender, EventArgs e)
        {
            if (undoStack.Count > 0)
            {
                if (undoStack.Last().Item1)
                {
                    level.Walls.Remove(undoStack.Last().Item2);
                }
                else
                {
                    level.Checkpoints.Remove(undoStack.Last().Item2);
                }
                redoStack.Add(undoStack.Last());
                undoStack.RemoveAt(undoStack.Count - 1);
            }
        }

        private void RedoButton_Click(object sender, EventArgs e)
        {
            if (redoStack.Count > 0)
            {
                if (redoStack.Last().Item1)
                {
                    level.Walls.Add(redoStack.Last().Item2);
                }
                else
                {
                    level.Checkpoints.Add(redoStack.Last().Item2);
                }
                undoStack.Add(redoStack.Last());
                redoStack.RemoveAt(redoStack.Count - 1);
            }
        }

        private void ClearCheckpointsButton_Click(object sender, EventArgs e)
        {
            level.Checkpoints.Clear();
            undoStack.Clear();
            redoStack.Clear();
        }

        private void ClearCourseButton_Click(object sender, EventArgs e)
        {
            level.Checkpoints.Clear();
            level.Walls.Clear();
            undoStack.Clear();
            redoStack.Clear();
        }

        private void ResetButton_Click(object sender, EventArgs e)
        {
            foreach (Player player in players)
            {
                player.Reset();
            }
        }
    }
}
