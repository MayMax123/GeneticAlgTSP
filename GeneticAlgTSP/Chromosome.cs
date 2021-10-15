using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgTSP
{
    class Chromosome
    {
        private GlobalRandom rnd { get; set; }
        public List<Point> Cities { get; set; }

        public Chromosome(List<Point> points)
        {
            //if(points.Count < 1)
            //{
            //    throw new ArgumentOutOfRangeException("The list points must have at least 1 element!");
            //}
            rnd = GlobalRandom.CreateInstance();
            Cities = points;
        }

        //Randomize this Chromosome using Linq
        public void RandomizeChromosome()
        {
            this.Cities = this.Cities.OrderBy((city) => this.rnd.GetRandomDouble()).ToList();
            if (this.Cities.Count < 1)
            {
                throw new ArgumentOutOfRangeException("The list of cities must have at least 1 element!");
            }
        }

        //Return a random chromosome if given a list of points
        static public Chromosome GetRandomChromosome(List<Point> points)
        {
            GlobalRandom rnd = GlobalRandom.CreateInstance();

            for (int i = 0; i < points.Count; i++)
            {
                int j = rnd.GetRandomInt(i, points.Count);
                (points[i], points[j]) = (points[j], points[i]);
            }

            if (points.Count < 1)
            {
                throw new ArgumentOutOfRangeException("The list points must have at least 1 element!");
            }

            return new Chromosome(points);
        }


        //Mutate a Chromosome and return it
        static public Chromosome GetMutatedChromosome(Chromosome chromosome)
        {
            List<Point> cities = new List<Point>(chromosome.Cities);

            //Get 2 random indexes
            GlobalRandom rnd = GlobalRandom.CreateInstance();
            int i = rnd.GetRandomInt(cities.Count);
            int j = rnd.GetRandomInt(cities.Count);

            //Switch the values at those 2 indexes
            (cities[i], cities[j]) = (cities[j], cities[i]);

            return new Chromosome(cities);
        }

        //Mutates the Chromosome and return it
        public Chromosome GetMutatedVersion()
        {
            List<Point> cities = new List<Point>(this.Cities);

            //Get 2 random indexes
            int i = this.rnd.GetRandomInt(cities.Count);
            int j = this.rnd.GetRandomInt(cities.Count);

            //Switch the values at those 2 indexes
            (cities[i], cities[j]) = (cities[j], cities[i]);

            return new Chromosome(cities);
        }

        //Change the chromosome to a mutated version of another one
        public void SetToMutatedVersion(Chromosome chromosome)
        {
            if (this.Cities.Count < 1 || chromosome.Cities.Count <1)
            {
                throw new ArgumentOutOfRangeException("The list of cities must have at least 1 element!");
            }

            this.Cities.Clear();
            this.Cities.AddRange(chromosome.Cities);

            int i, j;
            //Get 2 random indexes
            i = this.rnd.GetRandomInt(this.Cities.Count);
            while (true)
            {
                j = this.rnd.GetRandomInt(this.Cities.Count);
                if(i != j)
                {
                    break;
                }
            }

            //Switch the values at those 2 indexes
            (this.Cities[i], this.Cities[j]) = (this.Cities[j], this.Cities[i]);
        }


        //Combine 2 Chromosomes and return the result
        static public Chromosome GetCombinedChromosome(Chromosome chromosome1, Chromosome chromosome2)
        {
            Chromosome combinedChromosome = new Chromosome(new List<Point>());
            List<Point> cities1 = new List<Point>(chromosome1.Cities);
            List<Point> cities2 = new List<Point>(chromosome2.Cities);

            GlobalRandom rnd = GlobalRandom.CreateInstance();
            int j = rnd.GetRandomInt(cities1.Count);

            //Add all the points from the 1st list until the chosen index
            for (int i = 0; i < j; i++) 
                combinedChromosome.Cities.Add(cities1[i]);

            //If the current points doesn't contain a point from 2nd list, add it
            for (int i = 0; i < cities2.Count; i++) 
                if (!combinedChromosome.ContainsPoint(cities2[i])) combinedChromosome.Cities.Add(cities2[i]);

            if (combinedChromosome.Cities.Count < 1)
            {
                throw new ArgumentOutOfRangeException("The list combinedChromosome.Cities must have at least 1 element!");
            }

            return combinedChromosome;
        }

        //Combines the Chromosome with another one and returns the result
        public Chromosome GetCombinedVersion(Chromosome chromosome)
        {
            Chromosome combinedChromosome = new Chromosome(new List<Point>());
            List<Point> cities = new List<Point>(chromosome.Cities);

            int j = this.rnd.GetRandomInt(this.Cities.Count);

            //Add all the points from the 1st list until the chosen index
            for (int i = 0; i < j; i++)
                combinedChromosome.Cities.Add(this.Cities[i]);

            //If the current points doesn't contain a point from 2nd list, add it
            for (int i = 0; i < cities.Count; i++)
                if (!combinedChromosome.ContainsPoint(cities[i])) combinedChromosome.Cities.Add(cities[i]);

            if (combinedChromosome.Cities.Count < 1)
            {
                throw new ArgumentOutOfRangeException("The list combinedChromosome.Cities must have at least 1 element!");
            }

            return combinedChromosome;
        }

        //Change the chromosome to a combined version of another 2
        public void SetToCombinedVersion(Chromosome chromosome1, Chromosome chromosome2)
        {
            int j = this.rnd.GetRandomInt(chromosome1.Cities.Count);
            this.Cities.Clear();

            ////Add all the points from the 1st list until the chosen index
            //for (int i = 0; i < j; i++)
            //    this.Cities.Add(chromosome1.Cities[i]);
            this.Cities.AddRange(chromosome1.Cities.GetRange(0, j));

            //If the current points doesn't contain a point from 2nd list, add it
            for (int i = 0; i < chromosome2.Cities.Count; i++)
                if (!ContainsPoint(chromosome2.Cities[i])) this.Cities.Add(chromosome2.Cities[i]);

            if (this.Cities.Count < 1)
            {
                throw new ArgumentOutOfRangeException("The list this.Cities must have at least 1 element!");
            }
        }

        //Calculates the fitness based on the distance between cities
        static public double CalculateFitnessDistance(Chromosome chromosome)
        {
            double fitness = 0;
            List<Point> points = chromosome.Cities;

            //Get the distance from one point to the next one and add it to the fitness-sum
            for (int i = 0; i < points.Count; i++)
                fitness += points[i].DistanceTo(points[(i + 1) % points.Count]);

            return fitness;
        }


        //Overrides the toString Method so it can be logged in the Console
        public override string ToString()
        {
            string chromosomeString = "";

            foreach (Point point in this.Cities)
                chromosomeString += $"{point}\n";

            return chromosomeString;
        }


        //Checks if a Chromosome contains a certain Point
        public bool ContainsPoint(Point point)
        {
            foreach (Point p in this.Cities)
                if ((p.X, p.Y) == (point.X, point.Y)) return true;

            return false;
        }
    }
}
