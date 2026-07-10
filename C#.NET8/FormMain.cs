using System.Globalization;

namespace CollatzPE
{
    public partial class CollatzPE : Form
    {
        internal double Pmin = 0.5D;
        internal double Vmin;
        internal double Pmax = 1.0D;
        internal double Vmax;
        internal double Pcur = 0.6D;
               
        internal long Hstm = 8192;
        internal long Nmin = 100L;
        internal long Nmax = long.MaxValue / 4L;

        internal long[]      Hval;
        internal long        Hmax;
        internal long        Hsts;
        internal long        Hres; // bmW
        internal CultureInfo ceng;
        internal CultureInfo ccur;

        //optimised globals
        internal Random rnd;
        internal Bitmap bmp;
        internal Bitmap bxp;
        internal int bmW;
        internal int bmw;
        internal int bmH;
        internal Graphics bmg;
        internal Graphics bxg;
        internal Pen pg;
        internal Pen po;
        internal Pen pv;
        internal Pen pc;
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
            bmH = pnHyst.Height;
            bmp = new Bitmap(bmW, bmH);
            bxp = new Bitmap(bmW, bmH);
            bmg = Graphics.FromImage(bmp); bmg.Clear(Color.White);
            bxg = Graphics.FromImage(bxp); bxg.Clear(Color.White);

            ceng = new CultureInfo("en-US");
            ccur = Thread.CurrentThread.CurrentCulture;

            trPcur.Maximum = bmW - 1; trPcur.Minimum = 0;
            trpr = trPcur.Maximum - trPcur.Minimum;
            Vmin = Pmin;
            Vmax = Pmax;
            Vrev = 1D / (Vmax - Vmin);
            Hres = bmW; Hsts = 1L;
            Hval = new long[Hres]; for (c = 0; c < Hres; c++) Hval[c] = 0L;

            pg = new Pen(Color.DarkGray, 1F);
            po = new Pen(Color.Orange, 1F);
            pv = new Pen(Color.DarkViolet, 1F);
        }
        //---------------------------------------------------------------------
        private void CollatzPE_Load(object sender, EventArgs e)
        {
            tbVmin.Text = tbPmin.Text = Pmin.ToString("F3", ccur);
            tbVmax.Text = tbPmax.Text = Pmax.ToString("F3", ccur);

            tbHrm.Text = Hres.ToString("N0", ccur);
            tbHsc.Text = Hsts.ToString("N0", ccur);
            tbHsm.Text = Hstm.ToString("N0", ccur);
            PvalRefresh(); btPlot.Select();
        }
        //---------------------------------------------------------------------
        private void PvalRefresh()
        {
            trPcur.Value = trPcur.Minimum + (int)(trpr * (Pcur - Pmin) / (Pmax - Pmin));
        }// udates the trackbar value based on the current Pcur, Pmin, and Pmax values
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
            // draw hystogram
            bmg.Clear(Color.White); 

            if (Hres >= bmW)
            {
                pc = Hres == bmW ? po : pg; 
                for (c = 0; c < bmW; c++) // units - count
                {
                    Y = (int)(Hval[c * Hres / bmW] * bmH / Hmax);
                    bmg.DrawLine(pc, c, bmH, c, bmH - Y);
                }
            }
            else
            {
                pc = pv;
                for (c = 0; c < Hres; c++) // units - count
                {
                    Y = (int)(Hval[c] * bmH / Hmax);
                    X = (int)(c * bmW / Hres);
                    bmg.DrawLine(pc, X, bmH, X, bmH - Y);
                }
            }
        }
        //---------------------------------------------------------------------
        internal void ColStepPE()
        {
            s = 0L; e = 0L; // n initialised in ColHystPE() with random value

            while (n != 1L) // reduced to Nmax, Collatz sequence generation
            {
                p =(n % 2L == 1L); if(p & n > Nmax) { e = 0L; break; }
                e+= p ? 0L :  1L ; s++;
                n = p ? 3L *  n  + 1L : n / 2L; 
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
                Pcur = Pmin + (Pmax - Pmin) * trPcur.Value / trpr;
            }
            X = (int)(Hval[trPcur.Value * Hres / bmW]); // units - count
            tbHcur.Text = X.ToString("N0", ccur);
            X = (int)((long)X * Hres / Hsts); // units - relative even
            tbHevn.Text = X.ToString("N0", ccur);
            tbPcur.Text = Pcur.ToString("F3", ccur);
            tbPmin.Text = Pmin.ToString("F3", ccur);
            tbPmax.Text = Pmax.ToString("F3", ccur); DrowMode();
        }

        private void tbPcur_Leave(object sender, EventArgs e)
        {
            if (double.TryParse(tbPcur.Text, NumberStyles.Float, ccur, out Pval))
            {
                Pcur = Pval;
                if (Pcur < Pmin) if (Pcur > Vmin) Pmin = (Vmin + Pcur) / 2D; else Pmin = Vmin;
                if (Pcur > Pmax) if (Pcur < Vmax) Pmax = (Vmax + Pcur) / 2D; else Pmax = Vmax;
                PvalRefresh();
            }
            else tbPcur.Text = Pcur.ToString("F3", ccur);
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
            else tbPmin.Text = Pmin.ToString("F3", ccur);
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
            else tbPmax.Text = Pmax.ToString("F3", ccur);
        }
        private void tbHrm_Leave(object sender, EventArgs e)
        {
            if (long.TryParse(tbHrm.Text, NumberStyles.Integer, ccur, out n))
            {
                if (n < 1L) n = 1L; if (n > 1000000L) n = 1000000L;
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
            Pmin = Vmin = 0.5D; PvalRefresh();
        }
        private void tbVmax_DoubleClick(object sender, EventArgs e)
        {
            Pmax = Vmax = 1.0D; PvalRefresh();
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
                Pcur = Vmin + e.X * (Vmax - Vmin) / bmW;
                if (Pcur < Pmin) { Pmin = (Vmin + Pcur) / 2D; }
                if (Pcur > Pmax) { Pmax = (Vmax + Pcur) / 2D; }
            }
            if (ActiveControl == tbPmin)
            {
                Pmin = Vmin + e.X * (Vmax - Vmin) / bmW;
                if (Pmin > Pmax) if (Pmin < Vmax) Pmax = Vmax; else Pmin = Vmin;
                if (Pcur < Pmin) { Pcur = (Pmin + Pmax) / 2D; }
            }
            if (ActiveControl == tbPmax)
            {
                Pmax = Vmin + e.X * (Vmax - Vmin) / bmW;
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
            tbVmin.Text = tbPmin.Text = Pmin.ToString("F3", ccur);
            tbVmax.Text = tbPmax.Text = Pmax.ToString("F3", ccur);
            DrowHyst();
            DrowMode();
        }
        //---------------------------------------------------------------------
    }
}
