using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgTSP
{
    class Config
    {
        public int PopulationSize { get; set; }
        public double MutationProbability { get; set; }
        public double CombinationProbability { get; set; }
        public double SurvivalThreshold { get; set; }
        public double GenerationCount { get; set; }

        //Delegate
        public Func<Chromosome, double> FitnessFunction { get; set; }


        //Default values
        public Config()
        {
            PopulationSize = 1000;
            MutationProbability = 0.1;
            CombinationProbability = 0.4;
            SurvivalThreshold = 0.1;
            GenerationCount = 500;
            FitnessFunction = Chromosome.CalculateFitnessDistance;
        }


        //Set the config with given values
        public void setConfig(int populationSize, double mutationProbability, double combinationProbability, double survivalThreshold, double generationCount, Func<Chromosome, double> fitnessFunction)
        {
            PopulationSize = populationSize;
            MutationProbability = mutationProbability;
            CombinationProbability = combinationProbability;
            SurvivalThreshold = survivalThreshold;
            GenerationCount = generationCount;
            FitnessFunction = fitnessFunction;
        }
    }
}
