using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgTSP
{
    class Point
    {
        //Coordinates
        public double X { get; set; }
        public double Y { get; set; }

        public Point(double x, double y)
        {
            this.X = x;
            this.Y = y;
        }

        //Calculates the distance to another point
        public double DistanceTo(Point point)
        {
            return Math.Sqrt(Math.Pow(this.X - point.X, 2) + Math.Pow(this.Y - point.Y, 2));
        }

        //Overrides the toString Method so it can be logged in the Console
        public override string ToString()
        {
            return $"{this.X}, {this.Y}";
        }
    }
}
