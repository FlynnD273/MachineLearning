namespace MachineLearning
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.SaveButton = new System.Windows.Forms.Button();
            this.SaveNetButton = new System.Windows.Forms.Button();
            this.LoadNetButton = new System.Windows.Forms.Button();
            this.NextGenButton = new System.Windows.Forms.Button();
            this.UndoButton = new System.Windows.Forms.Button();
            this.RedoButton = new System.Windows.Forms.Button();
            this.ClearCourseButton = new System.Windows.Forms.Button();
            this.ClearCheckpointsButton = new System.Windows.Forms.Button();
            this.ResetButton = new System.Windows.Forms.Button();
            this.PlayButton = new System.Windows.Forms.Button();
            this.MutateNumberBox = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.MutateNumberBox)).BeginInit();
            this.SuspendLayout();
            // 
            // SaveButton
            // 
            this.SaveButton.Location = new System.Drawing.Point(184, 12);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(125, 34);
            this.SaveButton.TabIndex = 1;
            this.SaveButton.Text = "Save Course";
            this.SaveButton.UseVisualStyleBackColor = true;
            this.SaveButton.Click += new System.EventHandler(this.SaveTrackButton_Click);
            // 
            // SaveNetButton
            // 
            this.SaveNetButton.Location = new System.Drawing.Point(315, 12);
            this.SaveNetButton.Name = "SaveNetButton";
            this.SaveNetButton.Size = new System.Drawing.Size(149, 34);
            this.SaveNetButton.TabIndex = 2;
            this.SaveNetButton.Text = "Export Network";
            this.SaveNetButton.UseVisualStyleBackColor = true;
            this.SaveNetButton.Click += new System.EventHandler(this.SaveNetButton_Click);
            // 
            // LoadNetButton
            // 
            this.LoadNetButton.Location = new System.Drawing.Point(470, 12);
            this.LoadNetButton.Name = "LoadNetButton";
            this.LoadNetButton.Size = new System.Drawing.Size(149, 34);
            this.LoadNetButton.TabIndex = 3;
            this.LoadNetButton.Text = "Import Network";
            this.LoadNetButton.UseVisualStyleBackColor = true;
            this.LoadNetButton.Click += new System.EventHandler(this.LoadNetButton_Click);
            // 
            // NextGenButton
            // 
            this.NextGenButton.Location = new System.Drawing.Point(13, 12);
            this.NextGenButton.Name = "NextGenButton";
            this.NextGenButton.Size = new System.Drawing.Size(165, 34);
            this.NextGenButton.TabIndex = 0;
            this.NextGenButton.Text = "Next Generation";
            this.NextGenButton.UseVisualStyleBackColor = true;
            this.NextGenButton.Click += new System.EventHandler(this.NextGenButton_Click);
            // 
            // UndoButton
            // 
            this.UndoButton.Location = new System.Drawing.Point(329, 52);
            this.UndoButton.Name = "UndoButton";
            this.UndoButton.Size = new System.Drawing.Size(112, 34);
            this.UndoButton.TabIndex = 6;
            this.UndoButton.Text = "Undo";
            this.UndoButton.UseVisualStyleBackColor = true;
            this.UndoButton.Click += new System.EventHandler(this.UndoButton_Click);
            // 
            // RedoButton
            // 
            this.RedoButton.Location = new System.Drawing.Point(447, 52);
            this.RedoButton.Name = "RedoButton";
            this.RedoButton.Size = new System.Drawing.Size(112, 34);
            this.RedoButton.TabIndex = 7;
            this.RedoButton.Text = "Redo";
            this.RedoButton.UseVisualStyleBackColor = true;
            this.RedoButton.Click += new System.EventHandler(this.RedoButton_Click);
            // 
            // ClearCourseButton
            // 
            this.ClearCourseButton.Location = new System.Drawing.Point(12, 52);
            this.ClearCourseButton.Name = "ClearCourseButton";
            this.ClearCourseButton.Size = new System.Drawing.Size(131, 34);
            this.ClearCourseButton.TabIndex = 4;
            this.ClearCourseButton.Text = "Clear Course";
            this.ClearCourseButton.UseVisualStyleBackColor = true;
            this.ClearCourseButton.Click += new System.EventHandler(this.ClearCourseButton_Click);
            // 
            // ClearCheckpointsButton
            // 
            this.ClearCheckpointsButton.Location = new System.Drawing.Point(149, 52);
            this.ClearCheckpointsButton.Name = "ClearCheckpointsButton";
            this.ClearCheckpointsButton.Size = new System.Drawing.Size(174, 34);
            this.ClearCheckpointsButton.TabIndex = 5;
            this.ClearCheckpointsButton.Text = "Clear Checkpoints";
            this.ClearCheckpointsButton.UseVisualStyleBackColor = true;
            this.ClearCheckpointsButton.Click += new System.EventHandler(this.ClearCheckpointsButton_Click);
            // 
            // ResetButton
            // 
            this.ResetButton.Location = new System.Drawing.Point(565, 52);
            this.ResetButton.Name = "ResetButton";
            this.ResetButton.Size = new System.Drawing.Size(112, 34);
            this.ResetButton.TabIndex = 8;
            this.ResetButton.Text = "Reset Players";
            this.ResetButton.UseVisualStyleBackColor = true;
            this.ResetButton.Click += new System.EventHandler(this.ResetButton_Click);
            // 
            // PlayButton
            // 
            this.PlayButton.Location = new System.Drawing.Point(683, 52);
            this.PlayButton.Name = "PlayButton";
            this.PlayButton.Size = new System.Drawing.Size(112, 34);
            this.PlayButton.TabIndex = 9;
            this.PlayButton.Text = "Pause";
            this.PlayButton.UseVisualStyleBackColor = true;
            this.PlayButton.Click += new System.EventHandler(this.PlayButton_Click);
            // 
            // MutateNumberBox
            // 
            this.MutateNumberBox.DecimalPlaces = 1;
            this.MutateNumberBox.Location = new System.Drawing.Point(625, 15);
            this.MutateNumberBox.Name = "MutateNumberBox";
            this.MutateNumberBox.Size = new System.Drawing.Size(180, 31);
            this.MutateNumberBox.TabIndex = 10;
            this.MutateNumberBox.ValueChanged += new System.EventHandler(this.MutateNumberBox_ValueChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1154, 450);
            this.Controls.Add(this.MutateNumberBox);
            this.Controls.Add(this.PlayButton);
            this.Controls.Add(this.ResetButton);
            this.Controls.Add(this.ClearCheckpointsButton);
            this.Controls.Add(this.ClearCourseButton);
            this.Controls.Add(this.RedoButton);
            this.Controls.Add(this.UndoButton);
            this.Controls.Add(this.NextGenButton);
            this.Controls.Add(this.LoadNetButton);
            this.Controls.Add(this.SaveNetButton);
            this.Controls.Add(this.SaveButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.Text = "AI";
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseUp);
            ((System.ComponentModel.ISupportInitialize)(this.MutateNumberBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button SaveButton;
        private System.Windows.Forms.Button SaveNetButton;
        private System.Windows.Forms.Button LoadNetButton;
        private System.Windows.Forms.Button NextGenButton;
        private System.Windows.Forms.Button UndoButton;
        private System.Windows.Forms.Button RedoButton;
        private System.Windows.Forms.Button ClearCourseButton;
        private System.Windows.Forms.Button ClearCheckpointsButton;
        private System.Windows.Forms.Button ResetButton;
        private System.Windows.Forms.Button PlayButton;
        private System.Windows.Forms.NumericUpDown MutateNumberBox;
    }
}

