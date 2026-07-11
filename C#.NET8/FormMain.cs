using System;
using System.Drawing;
using System.Threading;
using System.Globalization;
using System.Windows.Forms;

namespace CollatzPE
{
    public partial class CollatzPE : Form
    {
        internal double Rmin = 0.5D;
        internal double Pmin;
        internal double Vmin;
        internal double Rmax = 1.0D;
        internal double Pmax;
        internal double Vmax;
        internal double Pcur = 0.7D;

        internal long Hstm = 8192;
        internal long Nmin = 100L;
        internal long Nmax = long.MaxValue / 4L;

        internal long[] Hval;
        internal long Hmax;
        internal long Hsts;
        internal long Hres; // bmW, image width resolution
        internal CultureInfo ceng;
        internal CultureInfo ccur;

        //optimised globals
        internal Random rnd;
        internal Bitmap bmp;
        internal Bitmap bxp;
        internal int bmW;
        internal int bmw;
        internal int bmH;
        internal int bmh;
        internal Graphics bmg;
        internal Graphics bxg;
        internal Pen? pg = null;
        internal Pen? po = null;
        internal Pen? pv = null;
        internal Pen? pc = null;
        //optimised globals
        internal int X, Y, trpr;
        internal double Vrev, Pval, Prev;
        internal long n, s, e, c;
        internal bool p;

        public CollatzPE()
        {
            InitializeComponent();

            rnd = new Random();
            bmW = pnHyst.Width; bmw = bmW - 1;
            bmH = pnHyst.Height; bmh = bmH - 1;
            bmp = new Bitmap(bmW, bmH);
            bxp = new Bitmap(bmW, bmH);
            bmg = Graphics.FromImage(bmp); bmg.Clear(Color.White);
            bxg = Graphics.FromImage(bxp); bxg.Clear(Color.White);

            ceng = new CultureInfo("en-US");
            ccur = Thread.CurrentThread.CurrentCulture;

            trPcur.Maximum = bmw; trPcur.Minimum = 0;
            trpr = trPcur.Maximum - trPcur.Minimum;
            Vmin = Pmin = Rmin;
            Vmax = Pmax = Rmax;
            Vrev = 1D / (Vmax - Vmin);
            Hres = bmW; Hsts = 1L;
            Hval = new long[Hres]; for (c = 0; c < Hres; c++) Hval[c] = 0L;

            pg = new Pen(Color.DarkGray, 1F);
            po = new Pen(Color.Orange, 1F);
            pv = new Pen(Color.DarkViolet, 1F);

            ColHystPE(); DrowHyst();
        }
        //---------------------------------------------------------------------
        private void CollatzPE_Load(object sender, EventArgs e)
        {
            tbVmin.Text = Vmin.ToString("F5", ccur);
            tbVmax.Text = Vmax.ToString("F5", ccur);

            tbHrm.Text = Hres.ToString("N0", ccur);
            tbHsc.Text = Hsts.ToString("N0", ccur);
            tbHsm.Text = Hstm.ToString("N0", ccur);
            PvalRefresh(); btPlot.Select();
        }
        //---------------------------------------------------------------------
        private void PvalRefresh()
        {
            X = trPcur.Minimum + (int)(trpr * (Pcur - Pmin) / (Pmax - Pmin));
            if (X != trPcur.Value)
            {
                trPcur.Value = X;
            }
            else
            {
                PvalUpdate();
            }
        }// udates the trackbar value based on the current Pcur, Pmin, and Pmax values
        //---------------------------------------------------------------------
        private void PvalUpdate()
        {
            X = (Pcur < Vmax) ? (int)(Hres * (Pcur - Vmin) * Vrev) : (int)Hres - 1;
            X = (int)Hval[X];
            tbHcur.Text = X.ToString("N0", ccur); // units - count
            X = (int)((Hsts == 0L) ? 0L : (long)X * Hres / Hsts);
            tbHevn.Text = X.ToString("N0", ccur); // units - relative even
            tbPcur.Text = Pcur.ToString("F5", ccur);
            tbPmin.Text = Pmin.ToString("F5", ccur);
            tbPmax.Text = Pmax.ToString("F5", ccur); DrowMode();
        }// udates the probability filelds based on the current Pcur, Pmin, and Pmax values
        //---------------------------------------------------------------------
        private void DrowMode()
        {   // Set the hystogram as background image for view
            pnHyst.BackgroundImage = bmp; bxg.DrawImage(bmp, 0, 0);
            // Drow Mode line, update view background image
            X = (int)((bmw) * (Pcur - Vmin) * Vrev);
            bxg.DrawLine(Pens.Red, X, 0, X, bmH); //mode line
            X = (int)((bmw) * (Pmin - Vmin) * Vrev);
            bxg.DrawLine(Pens.Blue, X, 0, X, bmH); //min range line
            X = (int)((bmw) * (Pmax - Vmin) * Vrev);
            bxg.DrawLine(Pens.Blue, X, 0, X, bmH); //max range line
            pnHyst.BackgroundImage = bxp;
        }
        //---------------------------------------------------------------------
        private void DrowHyst()
        {   // update text values
            tbHcm.Text = Hmax.ToString("N0", ccur);
            tbHsc.Text = Hsts.ToString("N0", ccur);
            // drow hystogram
            bmg.Clear(Color.White); n = Hmax == 0 ? 1L : Hmax;

            if (Hres >= bmW)
            {
                pc = Hres == bmW ? po : pg;
                for (c = 0; c < bmW; c++) // units - count
                {
                    Y = (int)(Hval[c * (Hres - 1L) / bmw] * bmh / n);
                    bmg.DrawLine(pc, c, bmh, c, bmh - Y);
                }
            }
            else
            {
                pc = pv;
                for (c = 0; c < Hres; c++) // units - count
                {
                    Y = (int)(Hval[c] * bmh / n);
                    X = (int)((2L * c + 1L) * bmW / (2L * Hres));
                    bmg.DrawLine(pc, X, bmh, X, bmh - Y);
                }
            }
        }
        //---------------------------------------------------------------------
        internal void ColStepPE()
        {
            s = 0L; e = 0L; // n initialised in ColHystPE() with random value

            while (n != 1L) // reduced to Nmax, Collatz sequence generation
            {
                p = (n % 2L == 1L); if (p & n > Nmax) { e = 0L; break; }
                e += p ? 0L : 1L; s++;
                n = p ? 3L * n + 1L : n / 2L;
            }
        }// generate Collatz sequence and set even probability components e,s
        //---------------------------------------------------------------------     
        internal void ColHystPE()
        {
            Prev = (double)Hres / (Pmax - Pmin); Hmax = 0L; Hsts = 0L;
            for (c = 0; c < Hres; c++) Hval[c] = 0L;
            for (c = 0; c < Hstm; c++)
            {   // genetrate sequence for random n
                n = rnd.NextInt64(Nmin, Nmax);
                ColStepPE();
                Pval = (double)e; Pval /= (double)s;
                // calculate hystogram for range
                if ((Pmin <= Pval) && (Pval < Pmax))
                {
                    Hval[(long)((Pval - Pmin) * Prev)]++; Hsts++;
                }
            }
            for (c = 0; c < Hres; c++) if ((e = Hval[c]) > Hmax) Hmax = e;
        }// calculate hystogram for probability data
        //---------------------------------------------------------------------
        private void trPcur_ValueChanged(object sender, EventArgs e)
        {
            if (ActiveControl == trPcur)
            {
                Pcur = Pmin + (Pmax - Pmin) * (trPcur.Value - trPcur.Minimum) / trpr;

            }
            PvalUpdate();
        }
        //---------------------------------------------------------------------
        private void tbPcur_Leave(object sender, EventArgs e)
        {
            if (double.TryParse(tbPcur.Text, NumberStyles.Float, ccur, out Pval))
            {
                Pcur = Pval;
                if (Pcur < Pmin) if (Pcur > Vmin) Pmin = (Vmin + Pcur) / 2D; else Pmin = Vmin;
                if (Pcur > Pmax) if (Pcur < Vmax) Pmax = (Vmax + Pcur) / 2D; else Pmax = Vmax;
                PvalRefresh();
            }
            else tbPcur.Text = Pcur.ToString("F5", ccur);
        }
        private void tbPmin_Leave(object sender, EventArgs e)
        {
            if (double.TryParse(tbPmin.Text, NumberStyles.Float, ccur, out Pval))
            {
                Pmin = Pval; if (Pmin < Vmin) Pmin = Vmin;
                if (Pmin > Pmax) if (Pmin < Vmax) Pmax = Vmax; else Pmin = Vmin;
                if (Pcur < Pmin) Pcur = (Pmin + Pmax) / 2D;
                PvalRefresh();
            }
            else tbPmin.Text = Pmin.ToString("F5", ccur);
        }
        private void tbPmax_Leave(object sender, EventArgs e)
        {
            if (double.TryParse(tbPmax.Text, NumberStyles.Float, ccur, out Pval))
            {
                Pmax = Pval; if (Pmax > Vmax) Pmax = Vmax;
                if (Pmax < Pmin) if (Pmax > Vmin) Pmin = Vmin; else Pmax = Vmax;
                if (Pcur > Pmax) { Pcur = (Pmin + Pmax) / 2D; }
                PvalRefresh();
            }
            else tbPmax.Text = Pmax.ToString("F5", ccur);
        }
        private void tbHrm_Leave(object sender, EventArgs e)
        {
            if (long.TryParse(tbHrm.Text, NumberStyles.Integer, ccur, out n))
            {
                if (n < 1L) n = 1L; if (n > 100000L) n = 100000L;
                if (n > Hres) Hval = new long[n]; Hres = n;
            }
            tbHrm.Text = Hres.ToString("N0", ccur);
        }
        //---------------------------------------------------------------------
        private void tbHsm_Leave(object sender, EventArgs e)
        {
            if (long.TryParse(tbHsm.Text, NumberStyles.Integer, ccur, out n))
            {
                Hstm = n; if (Hstm < 1L) Hstm = 1L; if (Hstm > 1000000L) Hstm = 1000000L;
            }
            tbHsm.Text = Hstm.ToString("N0", ccur);
        }
        //---------------------------------------------------------------------
        private void tbVmin_DoubleClick(object sender, EventArgs e)
        {
            Pmin = Vmin = Rmin; PvalRefresh();
        }
        private void tbVmax_DoubleClick(object sender, EventArgs e)
        {
            Pmax = Vmax = Rmax; PvalRefresh();
        }
        private void tbPmin_DoubleClick(object sender, EventArgs e)
        {
            Pmin = Vmin; PvalRefresh();
        }
        private void tbPmax_DoubleClick(object sender, EventArgs e)
        {
            Pmax = Vmax; PvalRefresh();
        }
        private void tbHrc_DoubleClick(object sender, EventArgs e)
        {
            Hres = bmW; tbHrm.Text = Hres.ToString("N0", ccur);
        }
        //---------------------------------------------------------------------
        private void pnHyst_MouseClick(object sender, MouseEventArgs e)
        {
            if (ActiveControl == tbPcur)
            {
                Pcur = Vmin + e.X * (Vmax - Vmin) / bmw;
                if (Pcur < Pmin) { Pmin = (Vmin + Pcur) / 2D; }
                if (Pcur > Pmax) { Pmax = (Vmax + Pcur) / 2D; }
            }
            if (ActiveControl == tbPmin)
            {
                Pmin = Vmin + e.X * (Vmax - Vmin) / bmw;
                if (Pmin > Pmax) if (Pmin < Vmax) Pmax = Vmax; else Pmin = Vmin;
                if (Pcur < Pmin) { Pcur = (Pmin + Pmax) / 2D; }
            }
            if (ActiveControl == tbPmax)
            {
                Pmax = Vmin + e.X * (Vmax - Vmin) / bmw;
                if (Pmax < Pmin) if (Pmax > Vmin) Pmin = Vmin; else Pmax = Vmax;
                if (Pcur > Pmax) { Pcur = (Pmin + Pmax) / 2D; }
            }
            PvalRefresh();
        }
        //---------------------------------------------------------------------
        private void btPlot_Click(object sender, EventArgs e)
        {
            ColHystPE();
            Vmax = Pmax; Vmin = Pmin; Vrev = 1D / (Vmax - Vmin);
            tbVmin.Text = tbPmin.Text = Pmin.ToString("F5", ccur);
            tbVmax.Text = tbPmax.Text = Pmax.ToString("F5", ccur);
            DrowHyst();
            DrowMode();
        }// plot hystorgam for selected range
        //---------------------------------------------------------------------
        private void btStepS_Click(object sender, EventArgs e)
        {
            if (Vmin == Rmin) return;
            if (Vmin - (Pval = Vmax - Vmin) < Rmin)
            {
                Vrev = 1D / (Vmin - Rmin);
                Vmin = Rmin; Vmax = Vmin; Pcur = (Vmax + Vmin) / 2D;
            }
            else
            {
                Vmax -= Pval; Vmin -= Pval; Pcur -= Pval;
            }

            tbVmin.Text = Vmin.ToString("F5", ccur);
            tbVmax.Text = Vmax.ToString("F5", ccur);
            Pmax = Vmax; Pmin = Vmin; ColHystPE();
            DrowHyst(); PvalRefresh();
        }// step up range for smaller values, << 
        //---------------------------------------------------------------------
        private void btStepG_Click(object sender, EventArgs e)
        {
            if (Vmax == Rmax) return;
            if (Vmax + (Pval = Vmax - Vmin) > Rmax)
            {
                Vrev = 1D / (Rmax - Vmax);
                Vmin = Vmax; Vmax = Rmax; Pcur = (Vmax + Vmin) / 2D;
            }
            else
            {
                Vmin += Pval; Vmax += Pval; Pcur += Pval;
            }

            tbVmin.Text = Vmin.ToString("F5", ccur);
            tbVmax.Text = Vmax.ToString("F5", ccur);
            Pmax = Vmax; Pmin = Vmin; ColHystPE();
            DrowHyst(); PvalRefresh();
        }// step up range for grater values, >>      
        //---------------------------------------------------------------------
        private void btSnap_Click(object sender, EventArgs e)
        {
            tbSnap.Text += 
                tbPcur.Text + "\t" + tbHcur.Text + "\t" +
                tbVmax.Text + "\t" + tbVmin.Text + "\t" +
                Hsts.ToString() + Environment.NewLine;
        }
        private void btClear_Click(object sender, EventArgs e)
        {
            tbSnap.Clear();
        }
        private void btCopy_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(tbSnap.Text))
                Clipboard.SetText(tbSnap.Text);
        }
        //---------------------------------------------------------------------
    }
}
