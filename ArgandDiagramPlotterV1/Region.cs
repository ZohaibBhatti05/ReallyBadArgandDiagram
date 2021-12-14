using System;
using System.Collections.Generic;
using System.Text;

namespace ArgandDiagramPlotterV1
{
    public class Region
    {
        protected double xCoord;
        protected double yCoord;

        public double X
        {
            get { return xCoord; }
        }

        public double Y
        {
            get { return yCoord; }
        }

        public Region(double A, double B)
        {
            xCoord = 250 + (25 * -A); yCoord = 250 + (25 * B);
        }
    }

    public class Circle : Region
    {
        private double radius;
        public double Radius
        {
            get { return radius; }
        }

        public Circle(double A, double B, double R) : base(A, B)
        {
            radius = 25 * R;
        }
    }

    public class PointToPoint : Region
    {
        private double xCoord2;
        private double yCoord2;
        private double xMid;
        private double yMid;

        public double X2
        {
            get { return xCoord2; }
        }

        public double Y2
        {
            get { return yCoord2; }
        }

        public double XMid
        {
            get { return xMid; }
        }

        public double YMid
        {
            get { return yMid; }
        }

        public PointToPoint(double A1, double B1, double A2, double B2) : base(A1, B1)
        {
            xCoord2 = 250 + (25 * -A2); yCoord2 = 250 + (25 * B2);

            xMid = (xCoord + xCoord2) / 2;
            yMid = (yCoord + yCoord2) / 2;
        }
    }

    public class HalfLine : Region
    {
        private double arg;
        public double Arg
        {
            get { return arg; }
        }

        public HalfLine(double A, double B, double Arg) : base (A, B)
        {
            arg = Arg;
        }
    }
}
