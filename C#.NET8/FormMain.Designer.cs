namespace CollatzPE
{
    partial class CollatzPE
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
            btPlot = new Button();
            tbHsm = new TextBox();
            lbHsm = new Label();
            lbHsc = new Label();
            tbHsc = new TextBox();
            tbVmin = new TextBox();
            tbVmax = new TextBox();
            pnHyst = new Panel();
            trPcur = new TrackBar();
            tbPmax = new TextBox();
            lbPmax = new Label();
            lbPmin = new Label();
            tbPmin = new TextBox();
            btSave = new Button();
            tbPcur = new TextBox();
            lbHrc = new Label();
            tbHrm = new TextBox();
            lbHc = new Label();
            lbHe = new Label();
            lbPcur = new Label();
            tbHcur = new TextBox();
            tbHevn = new TextBox();
            tbHcm = new TextBox();
            lbHcm = new Label();
            ((System.ComponentModel.ISupportInitialize)trPcur).BeginInit();
            SuspendLayout();
            // 
            // btPlot
            // 
            btPlot.Location = new Point(823, 325);
            btPlot.Name = "btPlot";
            btPlot.Size = new Size(102, 23);
            btPlot.TabIndex = 0;
            btPlot.Text = "Plot";
            btPlot.UseVisualStyleBackColor = true;
            btPlot.Click += btPlot_Click;
            // 
            // tbHsm
            // 
            tbHsm.Location = new Point(823, 245);
            tbHsm.Name = "tbHsm";
            tbHsm.Size = new Size(100, 23);
            tbHsm.TabIndex = 1;
            tbHsm.Leave += tbHsm_Leave;
            // 
            // lbHsm
            // 
            lbHsm.AutoSize = true;
            lbHsm.Location = new Point(823, 227);
            lbHsm.Name = "lbHsm";
            lbHsm.Size = new Size(49, 15);
            lbHsm.TabIndex = 2;
            lbHsm.Text = "Hs-max";
            // 
            // lbHsc
            // 
            lbHsc.AutoSize = true;
            lbHsc.Location = new Point(825, 85);
            lbHsc.Name = "lbHsc";
            lbHsc.Size = new Size(43, 15);
            lbHsc.TabIndex = 3;
            lbHsc.Text = "Hs-cur";
            // 
            // tbHsc
            // 
            tbHsc.Location = new Point(825, 59);
            tbHsc.Name = "tbHsc";
            tbHsc.ReadOnly = true;
            tbHsc.Size = new Size(100, 23);
            tbHsc.TabIndex = 4;
            // 
            // tbVmin
            // 
            tbVmin.Location = new Point(12, 325);
            tbVmin.Name = "tbVmin";
            tbVmin.ReadOnly = true;
            tbVmin.Size = new Size(100, 23);
            tbVmin.TabIndex = 7;
            tbVmin.DoubleClick += tbVmin_DoubleClick;
            // 
            // tbVmax
            // 
            tbVmax.Location = new Point(712, 325);
            tbVmax.Name = "tbVmax";
            tbVmax.ReadOnly = true;
            tbVmax.Size = new Size(100, 23);
            tbVmax.TabIndex = 10;
            tbVmax.DoubleClick += tbVmax_DoubleClick;
            // 
            // pnHyst
            // 
            pnHyst.BackgroundImageLayout = ImageLayout.None;
            pnHyst.BorderStyle = BorderStyle.FixedSingle;
            pnHyst.Location = new Point(12, 12);
            pnHyst.Name = "pnHyst";
            pnHyst.Size = new Size(800, 300);
            pnHyst.TabIndex = 11;
            pnHyst.MouseClick += pnHyst_MouseClick;
            // 
            // trPcur
            // 
            trPcur.AccessibleDescription = "position";
            trPcur.AccessibleName = "position";
            trPcur.LargeChange = 10;
            trPcur.Location = new Point(118, 313);
            trPcur.Maximum = 100;
            trPcur.Name = "trPcur";
            trPcur.Size = new Size(588, 45);
            trPcur.TabIndex = 12;
            trPcur.TickStyle = TickStyle.TopLeft;
            trPcur.ValueChanged += trPcur_ValueChanged;
            // 
            // tbPmax
            // 
            tbPmax.Location = new Point(712, 370);
            tbPmax.Name = "tbPmax";
            tbPmax.Size = new Size(100, 23);
            tbPmax.TabIndex = 15;
            tbPmax.Leave += tbPmax_Leave;
            // 
            // lbPmax
            // 
            lbPmax.AutoSize = true;
            lbPmax.Location = new Point(742, 351);
            lbPmax.Name = "lbPmax";
            lbPmax.Size = new Size(40, 15);
            lbPmax.TabIndex = 14;
            lbPmax.Text = "P max";
            // 
            // lbPmin
            // 
            lbPmin.AutoSize = true;
            lbPmin.Location = new Point(43, 351);
            lbPmin.Name = "lbPmin";
            lbPmin.Size = new Size(38, 15);
            lbPmin.TabIndex = 17;
            lbPmin.Text = "P min";
            // 
            // tbPmin
            // 
            tbPmin.Location = new Point(12, 370);
            tbPmin.Name = "tbPmin";
            tbPmin.Size = new Size(100, 23);
            tbPmin.TabIndex = 16;
            tbPmin.Leave += tbPmin_Leave;
            // 
            // btSave
            // 
            btSave.Location = new Point(823, 370);
            btSave.Name = "btSave";
            btSave.Size = new Size(102, 23);
            btSave.TabIndex = 18;
            btSave.Text = "Unload";
            btSave.UseVisualStyleBackColor = true;
            // 
            // tbPcur
            // 
            tbPcur.Location = new Point(380, 370);
            tbPcur.Name = "tbPcur";
            tbPcur.Size = new Size(100, 23);
            tbPcur.TabIndex = 19;
            tbPcur.Leave += tbPcur_Leave;
            // 
            // lbHrc
            // 
            lbHrc.AutoSize = true;
            lbHrc.Location = new Point(823, 271);
            lbHrc.Name = "lbHrc";
            lbHrc.Size = new Size(48, 15);
            lbHrc.TabIndex = 21;
            lbHrc.Text = "Hr-max";
            // 
            // tbHrm
            // 
            tbHrm.Location = new Point(823, 289);
            tbHrm.Name = "tbHrm";
            tbHrm.Size = new Size(100, 23);
            tbHrm.TabIndex = 20;
            tbHrm.DoubleClick += tbHrc_DoubleClick;
            tbHrm.Leave += tbHrm_Leave;
            // 
            // lbHc
            // 
            lbHc.AutoSize = true;
            lbHc.Location = new Point(261, 351);
            lbHc.Name = "lbHc";
            lbHc.Size = new Size(22, 15);
            lbHc.TabIndex = 22;
            lbHc.Text = "Hc";
            // 
            // lbHe
            // 
            lbHe.AutoSize = true;
            lbHe.Location = new Point(556, 351);
            lbHe.Name = "lbHe";
            lbHe.Size = new Size(22, 15);
            lbHe.TabIndex = 23;
            lbHe.Text = "He";
            // 
            // lbPcur
            // 
            lbPcur.AutoSize = true;
            lbPcur.Location = new Point(411, 351);
            lbPcur.Name = "lbPcur";
            lbPcur.Size = new Size(34, 15);
            lbPcur.TabIndex = 24;
            lbPcur.Text = "P cur";
            // 
            // tbHcur
            // 
            tbHcur.Location = new Point(227, 370);
            tbHcur.Name = "tbHcur";
            tbHcur.ReadOnly = true;
            tbHcur.Size = new Size(100, 23);
            tbHcur.TabIndex = 25;
            // 
            // tbHevn
            // 
            tbHevn.Location = new Point(518, 370);
            tbHevn.Name = "tbHevn";
            tbHevn.ReadOnly = true;
            tbHevn.Size = new Size(100, 23);
            tbHevn.TabIndex = 26;
            // 
            // tbHcm
            // 
            tbHcm.Location = new Point(823, 12);
            tbHcm.Name = "tbHcm";
            tbHcm.ReadOnly = true;
            tbHcm.Size = new Size(100, 23);
            tbHcm.TabIndex = 28;
            // 
            // lbHcm
            // 
            lbHcm.AutoSize = true;
            lbHcm.Location = new Point(823, 38);
            lbHcm.Name = "lbHcm";
            lbHcm.Size = new Size(48, 15);
            lbHcm.TabIndex = 27;
            lbHcm.Text = "Hc max";
            // 
            // CollatzPE
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(937, 402);
            Controls.Add(tbHcm);
            Controls.Add(lbHcm);
            Controls.Add(tbHevn);
            Controls.Add(tbHcur);
            Controls.Add(lbPcur);
            Controls.Add(lbHe);
            Controls.Add(lbHc);
            Controls.Add(lbHrc);
            Controls.Add(tbHrm);
            Controls.Add(tbPcur);
            Controls.Add(btSave);
            Controls.Add(lbPmin);
            Controls.Add(tbPmin);
            Controls.Add(tbPmax);
            Controls.Add(lbPmax);
            Controls.Add(trPcur);
            Controls.Add(pnHyst);
            Controls.Add(tbVmax);
            Controls.Add(tbVmin);
            Controls.Add(tbHsc);
            Controls.Add(lbHsc);
            Controls.Add(lbHsm);
            Controls.Add(tbHsm);
            Controls.Add(btPlot);
            DoubleBuffered = true;
            Name = "CollatzPE";
            Text = "Collatz PE";
            Load += CollatzPE_Load;
            ((System.ComponentModel.ISupportInitialize)trPcur).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btPlot;
        private TextBox tbHsm;
        private Label lbHsm;
        private Label lbHsc;
        private TextBox tbHsc;
        private TextBox tbVmin;
        private Label lbHc;
        private TextBox tbVmax;
        private TrackBar trPcur;
        private TextBox tbPmax;
        private Label lbPmax;
        private Label lbPmin;
        private TextBox tbPmin;
        private Button btSave;
        private TextBox tbPcur;
        private Label lbHrc;
        private TextBox tbHrm;
        internal Panel pnHyst;
        private Label lbHe;
        private Label lbPcur;
        private TextBox tbHcur;
        private TextBox tbHevn;
        private TextBox tbHcm;
        private Label lbHcm;
    }
}
