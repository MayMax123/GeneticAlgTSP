using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgTSP
{
    class TSP
    {
        private GlobalRandom rnd { get; set; }
        public Config Config { get; set; }
        private List<Point> AllCities { get; set; }
        public List<Chromosome> Population { get; set; }
        public int CurrentGeneration { get; set; }

        public TSP(Config config, List<Point> allCities)
        {
            this.rnd = GlobalRandom.CreateInstance();
            this.Config = config;
            this.AllCities = allCities;
            this.Population = new List<Chromosome>();
            this.CurrentGeneration = 0;
        }

        public void StartAlgorithm()
        {
            for (int i = 0; i < Config.PopulationSize; i++)
            {
                //Population.Add(Chromosome.GetRandomChromosome(new List<Point>(AllCities)));
                Population.Add(new Chromosome(new List<Point>(AllCities)));
                Population[0].RandomizeChromosome();
            }

            NextGeneration();
        }

        public void NextGeneration()
        {
            this.CurrentGeneration++;

            SortPopulationByFitness();

            if (this.CurrentGeneration > this.Config.GenerationCount) return;

            Console.WriteLine($"Gen {this.CurrentGeneration}: {this.Config.FitnessFunction(Population[0])}");

            if (this.CurrentGeneration == this.Config.GenerationCount) Console.WriteLine($"\n\nFinal score after {this.CurrentGeneration} Generations: {this.Config.FitnessFunction(Population[0])}");

            int iStart = (int)(Math.Floor(this.Config.SurvivalThreshold * (double)Population.Count));

            for (int i = iStart; i < Population.Count; i++)
            {
                //Get a random double between 0.00 and 1.00
                double r = (double)this.rnd.GetRandomInt(0, 101) / 100;

                //if (r <= this.Config.CombinationProbability) Population[i] = Chromosome.GetCombinedChromosome(ChooseRandomChromosome(), ChooseRandomChromosome());
                //else if (r <= this.Config.CombinationProbability + this.Config.MutationProbability) Population[i] = Chromosome.GetMutatedChromosome(ChooseRandomChromosome());
                //else Population[i].RandomizeChromosome();

                //if (r <= this.Config.CombinationProbability) Population[i] = ChooseRandomChromosome().GetCombinedVersion(ChooseRandomChromosome());
                //else if (r <= this.Config.CombinationProbability + this.Config.MutationProbability) Population[i] = ChooseRandomChromosome().GetMutatedVersion();
                //else Population[i].RandomizeChromosome();

                if (r <= this.Config.CombinationProbability) Population[i].SetToCombinedVersion(ChooseRandomChromosome(), ChooseRandomChromosome());
                else if (r <= this.Config.CombinationProbability + this.Config.MutationProbability) Population[i].SetToMutatedVersion(ChooseRandomChromosome());
                else Population[i].RandomizeChromosome();
            }

            NextGeneration();
        }

        public void SortPopulationByFitness()
        {
            Population = Population.OrderBy(o => this.Config.FitnessFunction(o)).ToList();
        }

        //Chooses a random chromosome from the surviving chromosomes
        public Chromosome ChooseRandomChromosome()
        {
            int j = this.rnd.GetRandomInt((int)(Math.Floor(this.Config.SurvivalThreshold * (double)Population.Count)) + 1);

            return Population[j];
        }
    }
}
