using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Globalization;

namespace GeneticAlgTSP
{
    class TextReader
    {

        //Get the points from the test text file
        static public List<Point> getTestPoints()
        {
            return getPointsFromFile(@"C:\Schule\5. Jahrgang\SWP\GeneticAlgTSP\test.txt");
        }


        //Get the points from the berlin text file
        static public List<Point> getBerlinPoints()
        {
            return getPointsFromFile(@"C:\Schule\5. Jahrgang\SWP\GeneticAlgTSP\berlin.txt");
        }


        //Get the points from the finland text file
        static public List<Point> getFinlandPoints()
        {
            return getPointsFromFile(@"C:\Schule\5. Jahrgang\SWP\GeneticAlgTSP\finland.txt");
        }


        //Get Points from given file (-path)
        static public List<Point> getPointsFromFile(string path)
        {
            //Get all the lines from the .txt file and create a string-array
            string[] textLines = File.ReadAllLines(path);
            List<Point> points = new List<Point>();

            double x, y;

            //Loop through all lines
            foreach (string line in textLines)
            {
                //If Line begins with a digit:
                if (Char.IsDigit(line[0]))
                {
                    //Split the line into: Index, X-Coordinate and Y-Coordinate
                    string[] lineSegments = line.Split(" ");

                    //Save the coordinates into variables, create a Point and add to list
                    x = Double.Parse(lineSegments[1], CultureInfo.InvariantCulture);
                    y = Double.Parse(lineSegments[2], CultureInfo.InvariantCulture);
                    points.Add(new Point(x, y));
                }
            }

            return points;
        }
    }
}
