using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Text.RegularExpressions;

namespace ArgandDiagramPlotterV1
{
    public partial class Form1 : Form
    {
        private Pen[] pens = new Pen[6]
        {
            new Pen(Color.Black, 3),
            new Pen(Color.Red, 3),
            new Pen(Color.Blue, 3),
            new Pen(Color.Green, 3),
            new Pen(Color.Purple, 3),
            new Pen(Color.Orange, 3),
        };

        private Point[] points = new Point[6];

        private Region[] regions = new Region[6];
        

        public Form1()
        {
            InitializeComponent();
            foreach(Pen p in pens)
            {
                p.EndCap = LineCap.Round;
            }
            
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void picGraph_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            for (int i = 0; i < 6; i++)
            {
                if (points[i].IsEmpty)
                {
                    continue;
                }
                else
                {
                    e.Graphics.DrawLine(pens[i], new Point(250, 250), points[i]);
                }
            }
        }

        private void AddNewPoint(int index, double a, double b)
        {
            int A = (int)(250 + (25 * a));
            int B = (int)(250 + (25 * -b));

            points[index] = new Point(A, B);
            picGraph.Invalidate();
        }

        private void btnZ1_Click(object sender, EventArgs e)
        {
            if (Double.TryParse(txtZ1A.Text, out double A) && Double.TryParse(txtZ1B.Text, out double B))
            {
                AddNewPoint(0, A, B);
            }
        }

        private void btnZ2_Click(object sender, EventArgs e)
        {
            if (Double.TryParse(txtZ2A.Text, out double A) && Double.TryParse(txtZ2B.Text, out double B))
            {
                AddNewPoint(1, A, B);
            }
        }

        private void btnZ3_Click(object sender, EventArgs e)
        {
            if (Double.TryParse(txtZ3A.Text, out double A) && Double.TryParse(txtZ3B.Text, out double B))
            {
                AddNewPoint(2, A, B);
            }
        }

        private void btnZ4_Click(object sender, EventArgs e)
        {
            if (Double.TryParse(txtZ4A.Text, out double A) && Double.TryParse(txtZ4B.Text, out double B))
            {
                AddNewPoint(3, A, B);
            }
        }

        private void btnZ5_Click(object sender, EventArgs e)
        {
            if (Double.TryParse(txtZ5A.Text, out double A) && Double.TryParse(txtZ5B.Text, out double B))
            {
                AddNewPoint(4, A, B);
            }
        }

        private void btnModZ1_Click(object sender, EventArgs e)
        {
            if (Double.TryParse(txtZ1A.Text, out double A) && Double.TryParse(txtZ1B.Text, out double B))
            {
                ModArg(A, B);
            }
        }

        private void btnModZ2_Click(object sender, EventArgs e)
        {
            if (Double.TryParse(txtZ2A.Text, out double A) && Double.TryParse(txtZ2B.Text, out double B))
            {
                ModArg(A, B);
            }
        }

        private void btnModZ3_Click(object sender, EventArgs e)
        {
            if (Double.TryParse(txtZ3A.Text, out double A) && Double.TryParse(txtZ3B.Text, out double B))
            {
                ModArg(A, B);
            }
        }

        private void btnModZ4_Click(object sender, EventArgs e)
        {
            if (Double.TryParse(txtZ4A.Text, out double A) && Double.TryParse(txtZ4B.Text, out double B))
            {
                ModArg(A, B);
            }
        }

        private void btnModZ5_Click(object sender, EventArgs e)
        {
            if (Double.TryParse(txtZ5A.Text, out double A) && Double.TryParse(txtZ5B.Text, out double B))
            {
                ModArg(A, B);
            }
        }

        private void ModArg(double A, double B)
        {
            // Mod
            double Mod = (Math.Sqrt(Math.Pow(A, 2) + Math.Pow(B, 2)));

            double Arg;
            // Arg
            if (A == 0) // Re(z) = 0:
            {
                if (B > 0) { Arg = 90; }
                else if (B < 0) { Arg = -90; }
                else { Arg = 0; }
            }

            else if (A > 0 && B >= 0) // top right quadrant
            {
                Arg = ToDegrees(Math.Atan((B / A)));
            }

            else if (A < 0 && B >= 0) // top left quadrant
            {
                Arg = 180 - ToDegrees(Math.Atan((B / A)));
            }

            else if (A > 0 && B < 0) // bottom right quadrant
            {
                Arg = -ToDegrees(Math.Atan((Math.Abs(B) / A)));
            }

            else // bottom left quadrant
            {
                Arg = -(180 - ToDegrees(Math.Atan((Math.Abs(B) / A))));
            }

            string z = (B >= 0) ? String.Format("{0} + {1}i", A, B) : String.Format("{0} - {1}i", A, Math.Abs(B));

            lblModArg.Text = String.Format(
                z + " :: Mod: {0:0.000} :: Arg: {1:0.000}"
                , Mod, Arg, A, B);
        }

        private double ToDegrees(double RadVal)
        {
            return ((RadVal / Math.PI) * 180);
        }

        private double ToRadians(double DegVal)
        {
            return ((DegVal / 180) * Math.PI);
        }

        private void btnModArgPlot_Click(object sender, EventArgs e)
        {
            if (double.TryParse(txtArg.Text, out double Arg) && double.TryParse(txtMod.Text, out double Mod))
            {
                double Re = Mod * (Math.Cos(ToRadians(Arg)));
                double Im = Mod * (Math.Sin(ToRadians(Arg)));
                AddNewPoint(5, Re, Im);

                lblModArgResult.Text = (Im >= 0) ? String.Format("Result: {0:0.000} + {1:0.000}i", Re, Im)
                    : String.Format("Result: {0:0.000} - {1:0.000}i", Re, Math.Abs(Im));
            }
        }

        // ==============================================================================
        // ==============================================================================
        // ==============================================================================


        private void picGraph2_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            for(int i = 0; i < 5; i++)
            {
                if (regions[i] != null)
                {
                    if (regions[i] is Circle)
                    {
                        e.Graphics.DrawEllipse(pens[i], (float)regions[i].X - ((float)(regions[i] as Circle).Radius)
                            , (float)regions[i].Y - ((float)(regions[i] as Circle).Radius),
                            (float)(regions[i] as Circle).Radius * 2, (float)(regions[i] as Circle).Radius * 2);
                    }
                    else if (regions[i] is PointToPoint)
                    {
                        pens[i].DashStyle = DashStyle.Dash;

                        e.Graphics.DrawLine(pens[i], (float)(regions[i].X), (float)(regions[i].Y),
                            (float)((regions[i] as PointToPoint).X2), (float)((regions[i] as PointToPoint).Y2));

                        pens[i].DashStyle = DashStyle.Solid;

                        double DX = (regions[i] as PointToPoint).X2 - (regions[i] as PointToPoint).X;
                        double DY = (regions[i] as PointToPoint).Y2 - (regions[i] as PointToPoint).Y;

                        double XM = (regions[i] as PointToPoint).XMid;
                        double YM = (regions[i] as PointToPoint).YMid;

                        e.Graphics.DrawLine(pens[i], (float)XM, (float)YM, (float)(XM - DY), (float)(YM + DX));
                        e.Graphics.DrawLine(pens[i], (float)XM, (float)YM, (float)(XM + DY), (float)(YM - DX));



                    }
                    else if (regions[i] is HalfLine)
                    {
                        double A = 0; double O = 0;

                        pens[i].DashStyle = DashStyle.Dash;
                        e.Graphics.DrawLine(pens[i], 0, (float)regions[i].Y, 500, (float)regions[i].Y);
                        pens[i].DashStyle = DashStyle.Solid;

                        if ((regions[i] as HalfLine).Arg == 0)
                        {
                            A = 600; O = 0;
                        }
                        else if ((regions[i] as HalfLine).Arg == 90)
                        {
                            A = 0; O = -600;
                        }
                        else if ((regions[i] as HalfLine).Arg == 180 || (regions[i] as HalfLine).Arg == -180)
                        {
                            A = -600; O = 0;
                        }


                        //else if ((regions[i] as HalfLine).Arg >= 0 && (regions[i] as HalfLine).Arg < 90)
                        else
                        {
                            A = 600 * Math.Cos(ToRadians((regions[i] as HalfLine).Arg));
                            O = -(600 * Math.Sin(ToRadians((regions[i] as HalfLine).Arg)));
                        }

                        e.Graphics.DrawLine(pens[i], (float)regions[i].X, (float)regions[i].Y,
                            (float)regions[i].X + (float)A, (float)regions[i].Y + (float)O);
                    }
                }
            }
        }

        private void btnCirc1_Click(object sender, EventArgs e)
        {
            if (double.TryParse(txtCirc1A.Text, out double A) && double.TryParse(txtCirc1B.Text, out double B)
                && double.TryParse(txtCirc1R.Text, out double R))
            {
                regions[0] = new Circle(A, B, R); picGraph2.Invalidate();
            }
        }

        private void btnPTP1_Click(object sender, EventArgs e)
        {
            if (double.TryParse(txtPTP1A1.Text, out double A1) && double.TryParse(txtPTP1B1.Text, out double B1)
                && double.TryParse(txtPTP1A2.Text, out double A2) && double.TryParse(txtPTP1B2.Text, out double B2))
            {
                regions[0] = new PointToPoint(A1, B1, A2, B2); picGraph2.Invalidate();
            }
        }

        private void btnHalf1_Click(object sender, EventArgs e)
        {
            if (double.TryParse(txtHalf1A.Text, out double A) && double.TryParse(txtHalf1B.Text, out double B)
                 && double.TryParse(txtHalf1Arg.Text, out double Arg))
            {
                regions[0] = new HalfLine(A, B, Arg); picGraph2.Invalidate();
            }
        }

        private void btnCirc2_Click(object sender, EventArgs e)
        {
            if (double.TryParse(txtCirc2A.Text, out double A) && double.TryParse(txtCirc2B.Text, out double B)
                && double.TryParse(txtCirc2R.Text, out double R))
            {
                regions[1] = new Circle(A, B, R); picGraph2.Invalidate();
            }
        }

        private void btnPTP2_Click(object sender, EventArgs e)
        {
            if (double.TryParse(txtPTP2A1.Text, out double A1) && double.TryParse(txtPTP2B1.Text, out double B1)
                    && double.TryParse(txtPTP2A2.Text, out double A2) && double.TryParse(txtPTP2B2.Text, out double B2))
            {
                regions[1] = new PointToPoint(A1, B1, A2, B2); picGraph2.Invalidate();
            }
        }

        private void btnHalf2_Click(object sender, EventArgs e)
        {
            if (double.TryParse(txtHalf2A.Text, out double A) && double.TryParse(txtHalf2B.Text, out double B)
                    && double.TryParse(txtHalf2Arg.Text, out double Arg))
            {
                regions[1] = new HalfLine(A, B, Arg); picGraph2.Invalidate();
            }
        }

        private void btnCirc3_Click(object sender, EventArgs e)
        {
            if (double.TryParse(txtCirc3A.Text, out double A) && double.TryParse(txtCirc3B.Text, out double B)
                    && double.TryParse(txtCirc3R.Text, out double R))
            {
                regions[2] = new Circle(A, B, R); picGraph2.Invalidate();
            }
        }

        private void btnPTP3_Click(object sender, EventArgs e)
        {
            if (double.TryParse(txtPTP3A1.Text, out double A1) && double.TryParse(txtPTP3B1.Text, out double B1)
                    && double.TryParse(txtPTP3A2.Text, out double A2) && double.TryParse(txtPTP3B2.Text, out double B2))
            {
                regions[2] = new PointToPoint(A1, B1, A2, B2); picGraph2.Invalidate();
            }
        }

        private void btnHalf3_Click(object sender, EventArgs e)
        {
            if (double.TryParse(txtHalf3A.Text, out double A) && double.TryParse(txtHalf3B.Text, out double B)
                    && double.TryParse(txtHalf3Arg.Text, out double Arg))
            {
                regions[2] = new HalfLine(A, B, Arg); picGraph2.Invalidate();
            }
        }

        private void btnCirc4_Click(object sender, EventArgs e)
        {
            if (double.TryParse(txtCirc4A.Text, out double A) && double.TryParse(txtCirc4B.Text, out double B)
                    && double.TryParse(txtCirc4R.Text, out double R))
            {
                regions[3] = new Circle(A, B, R); picGraph2.Invalidate();
            }
        }

        private void btnPTP4_Click(object sender, EventArgs e)
        {
            if (double.TryParse(txtPTP4A1.Text, out double A1) && double.TryParse(txtPTP4B1.Text, out double B1)
                    && double.TryParse(txtPTP4A2.Text, out double A2) && double.TryParse(txtPTP4B2.Text, out double B2))
            {
                regions[3] = new PointToPoint(A1, B1, A2, B2); picGraph2.Invalidate();
            }
        }

        private void btnHalf4_Click(object sender, EventArgs e)
        {
            if (double.TryParse(txtHalf4A.Text, out double A) && double.TryParse(txtHalf4B.Text, out double B)
                    && double.TryParse(txtHalf4Arg.Text, out double Arg))
            {
                regions[3] = new HalfLine(A, B, Arg); picGraph2.Invalidate();
            }
        }

        private void btnCirc5_Click(object sender, EventArgs e)
        {
            if (double.TryParse(txtCirc5A.Text, out double A) && double.TryParse(txtCirc5B.Text, out double B)
                    && double.TryParse(txtCirc5R.Text, out double R))
            {
                regions[4] = new Circle(A, B, R); picGraph2.Invalidate();
            }
        }

        private void btnPTP5_Click(object sender, EventArgs e)
        {
            if (double.TryParse(txtPTP5A1.Text, out double A1) && double.TryParse(txtPTP5B1.Text, out double B1)
                    && double.TryParse(txtPTP5A2.Text, out double A2) && double.TryParse(txtPTP5B2.Text, out double B2))
            {
                regions[4] = new PointToPoint(A1, B1, A2, B2); picGraph2.Invalidate();
            }
        }

        private void btnHalf5_Click(object sender, EventArgs e)
        {
            if (double.TryParse(txtHalf5A.Text, out double A) && double.TryParse(txtHalf5B.Text, out double B)
                    && double.TryParse(txtHalf4Arg.Text, out double Arg))
            {
                regions[4] = new HalfLine(A, B, Arg); picGraph2.Invalidate();
            }
        }
    }
}
