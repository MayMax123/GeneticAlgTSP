using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgTSP
{
    //Singleton Class
    class GlobalRandom
    {
        //Singleton-Instance
        static private GlobalRandom instance;
        //Actual Random value
        private Random random;

        public GlobalRandom()
        {
            random = new Random();
        }

        //Create the instance if null, return it when it already exists
        static public GlobalRandom CreateInstance()
        {
            if (instance == null)
                instance = new GlobalRandom();
            return instance;
        }

        //Generate a random int between 2 bounds
        public int GetRandomInt(int lowerBound, int upperBound)
        {
            return random.Next(lowerBound, upperBound);
        }
        public int GetRandomInt(int upperBound)
        {
            return (GetRandomInt(0, upperBound));
        }


        //Generates a random double between 0.0 and 1.0
        public double GetRandomDouble()
        {
            return random.NextDouble();
        }

    }
}
