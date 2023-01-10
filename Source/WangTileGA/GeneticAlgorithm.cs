using System;
using System.Collections.Generic;
using System.Linq;
using WangTile;

namespace GeneticAlgorithm
{
    public class TiledGeneticAlgorithm
    {
        private Random random = new Random();

        public Board GenerateBoard(TiledSimulatedAnnealingBoardConfig tiledBoardConfig)
        {   
            bool saveImage = false;
            WangTile.Generator boardGenerator = new WangTile.Generator();
            return boardGenerator.Tiled_V1_Simulated_Annealing_UsingJSONTiles(tiledBoardConfig.Width, tiledBoardConfig.Height, tiledBoardConfig.OutputName, tiledBoardConfig.ColorMatching, tiledBoardConfig.Iterations, tiledBoardConfig.Temperature, tiledBoardConfig.LIteration, tiledBoardConfig.Alpha, tiledBoardConfig.MapJsonDirectory, tiledBoardConfig.MapJsonFilename, saveImage);
        }

        public Board Select(IEnumerable<Board> population, IEnumerable<double> fitnesses, double sum = 0.0)
        {
            // fitness proportionate selection.

            var fitArr = fitnesses.ToArray();
            if (sum == 0.0)
            {
                foreach (var fit in fitnesses)
                {
                    sum += fit;
                }
            }

            // normalize.
            for (int i = 0; i < fitArr.Length; ++i)
            {
                fitArr[i] /= sum;
            }

            var popArr = population.ToArray();

            Array.Sort(fitArr, popArr);

            sum = 0.0;

            var accumFitness = new double[fitArr.Length];

            // calculate accumulated normalized fitness values.
            for (int i = 0; i < accumFitness.Length; ++i)
            {
                sum += fitArr[i];
                accumFitness[i] = sum;
            }

            var val = random.NextDouble();

            for (int i = 0; i < accumFitness.Length; ++i)
            {
                if (accumFitness[i] > val)
                {
                    return popArr[i];
                }
            }

            return popArr[popArr.Length-1];
        }

        public Board Mutate(Board chromosome, double probability)
        {
            // Maybe for each tileslot, probability of mutation?
            // Change to another compatible tile?

            return chromosome;
        }

        public IEnumerable<Board> Crossover(Board chromosome1, Board chromosome2)
        {   
            // Crossover from parent chromosome1 and chromosome2
            // How do we crossover two boards?
            return new Board[]{};
        }

        public Board Run(TiledSimulatedAnnealingBoardConfig tiledBoardConfig, Func<Board, double> fitness, double crossoverProbability, double mutationProbability, int populationSize = 500, int iterations = 100)
        {
            // run population is population being generated.
            // test population is the population from which samples are taken.
            List<Board> testPopulation = new List<Board>();
            List<Board> runPopulation = new List<Board>();
            Board one; 
            Board two;
            var randDouble = 0.0;

            // construct initial population.
            while (testPopulation.Count < populationSize)
            {
                testPopulation.Add(GenerateBoard(tiledBoardConfig));
            }

            var fitnesses = new double[testPopulation.Count];

            double sum = 0.0;

            // continuously generate populations until number of iterations is met.
            for (int iter = 0; iter < iterations; ++iter)
            {
                runPopulation = new List<Board>();

                // calculate fitness for test population.
                sum = 0.0;
                fitnesses = new double[testPopulation.Count];
                for (int i = 0; i < fitnesses.Length; ++i)
                {
                    fitnesses[i] = fitness(testPopulation[i]);
                    sum += fitnesses[i];
                }

                // a population doesn't need to be generated for last iteration.
                // (using test population)
                if (iter == iterations - 1) break;

                while (runPopulation.Count < testPopulation.Count)
                {

                    one = Select(testPopulation, fitnesses, sum);
                    two = Select(testPopulation, fitnesses, sum);

                    // determine if crossover occurs.
                    randDouble = random.NextDouble();
                    if (randDouble <= crossoverProbability)
                    {
                        var stringArr = Crossover(one, two).ToList();
                        one = stringArr[0];
                        two = stringArr[1];
                    }

                    one = Mutate(one, mutationProbability);
                    two = Mutate(two, mutationProbability);

                    runPopulation.Add(one);
                    runPopulation.Add(two);
                }

                testPopulation = runPopulation;
            }

            // find best-fitting string.
            var testSort = testPopulation.ToArray();
            var fitSort = fitnesses.ToArray();

            Array.Sort(fitSort, testSort);

            return testSort[testSort.Length - 1];
        }
    }

    public class FitnessHelper
    {
        private Dictionary<int,int> solution;
        
        public FitnessHelper(Dictionary<int,int> targetSolution)
        {
            solution = targetSolution;
        }

        public double Fitness(Board chromosome)
        {
            int totalDiff = 0;

            // Use tileset 0 for now
            int tileSetID = 0;

            // Use tile frequency to score
            Dictionary<int,int> chromosomeTileFrequency = Utils.GetTileFrequency(chromosome.TileSet, chromosome.TileSlots);
            

            for (int i=0; i<chromosome.TileSet[tileSetID].Tiles.Length;i++){
                totalDiff += Math.Abs(this.solution[i]-chromosomeTileFrequency[i]);
            }
            return totalDiff;
        }

  
        
    }
    
}