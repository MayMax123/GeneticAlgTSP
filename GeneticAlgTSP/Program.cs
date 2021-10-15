using System;
using System.Collections.Generic;
using System.IO;

namespace GeneticAlgTSP
{
    class Program
    {
        static void Main(string[] args)
        {
            //Create new config and set the parameter
            Config config = new Config();
            config.setConfig(1000, 0.4, 0.4, 0.3, 5000, Chromosome.CalculateFitnessDistance);

            //Create the Genetic Algorithm with the given points and start it
            TSP tsp = new TSP(config, TextReader.getBerlinPoints());
            tsp.StartAlgorithm();
        }
    }
}
