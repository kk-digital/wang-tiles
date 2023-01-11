using System.Text;

namespace WangTile{
    public class GeneticAlgorithmConfig {
        public double CrossoverProbability;
        public double MutationProbability;
        public int PopulationSize;
        public int Generations;

        public GeneticAlgorithmConfig(double crossoverProbability, double mutationProbability, int populationSize = 500, int generations = 100){
            this.CrossoverProbability = crossoverProbability;
            this.MutationProbability = mutationProbability;
            this.PopulationSize = populationSize;
            this.Generations = generations;
        }
    }

    public class TiledGeneticAlgorithm
    {
        private Random random = new Random();

        public Board GenerateBoard(BoardConfig boardConfig, WangTileSet tileSet)
        {   
            Board newBoard = new Board(boardConfig.Height, boardConfig.Width, boardConfig.ColorMatching);
            newBoard.AddTileSet(tileSet);
            // Fill in board with tiles
            newBoard.FillBoardTileSlots(this.random);

            return newBoard;
        }

        public Board Mutate(Board board, double probability, WangTileSet tileSet, int tileSetID=0)
        {
            // Select random tile 
            int tileIndex = this.random.Next(0, tileSet.Tiles.Length);

            // place the random tile on the board 
            (int col, int row) pos = Utils.GetRandomPosition(board.Width, board.Height, this.random);
            board.PlaceTile(tileSetID, tileIndex, pos.col, pos.row);

            return board;
        }

        public IEnumerable<Board> Crossover(Board parent1, Board parent2)
        {   
            Board child1 = parent1;
            Board child2 = parent2;
            
            for (int i=0;i<parent1.TileSlots.Length/2;i++){
                child2.TileSlots[i] = parent1.TileSlots[i];
            }

            for (int i=0;i<parent2.TileSlots.Length/2;i++){
                child1.TileSlots[i] = parent2.TileSlots[i];
            }

            return new Board[]{child1, child2};
        }

        public Board Run(BoardConfig boardConfig, GeneticAlgorithmConfig geneticAlgoConfig, WangTileSet tileSet, Func<Board, double> fitness, Func <List<Board>, IEnumerable<double> , Random, int, Board> select)
        {   
            string[] mismatchStr = new string[geneticAlgoConfig.Generations];

            // run population is population being generated.
            // test population is the population from which samples are taken.
            List<Board> testPopulation = new List<Board>();
            List<Board> runPopulation = new List<Board>();
            Board one; 
            Board two;
            var randDouble = 0.0;

            // construct initial population.
            while (testPopulation.Count < geneticAlgoConfig.PopulationSize)
            {
                testPopulation.Add(GenerateBoard(boardConfig, tileSet));
            }

            var fitnesses = new double[testPopulation.Count];

            // continuously generate populations until number of iterations is met.
            for (int generation = 0; generation < geneticAlgoConfig.Generations; ++generation)
            {
                runPopulation = new List<Board>();

                // calculate fitness for test population.
                fitnesses = new double[testPopulation.Count];
                for (int i = 0; i < fitnesses.Length; ++i)
                {
                    fitnesses[i] = fitness(testPopulation[i]);
                }

                // a population doesn't need to be generated for last iteration.
                // (using test population)
                if (generation == geneticAlgoConfig.Generations - 1) break;

                while (runPopulation.Count < testPopulation.Count)
                {
                    one = select(testPopulation, fitnesses, this.random, 10);
                    two = select(testPopulation, fitnesses, this.random, 10);

                    // determine if crossover occurs.
                    randDouble = random.NextDouble();
                    if (randDouble <= geneticAlgoConfig.CrossoverProbability)
                    {
                        var boardArr = Crossover(one, two).ToList();
                        one = boardArr[0];
                        two = boardArr[1];
                    }

                    one = Mutate(one, geneticAlgoConfig.MutationProbability, tileSet);
                    two = Mutate(two, geneticAlgoConfig.MutationProbability, tileSet);

                    runPopulation.Add(one);
                    runPopulation.Add(two);
                }

                testPopulation = runPopulation;

                // find best-fitting board.
                var testSort1 = testPopulation.ToArray();
                var fitSort1 = fitnesses.ToArray();
                Array.Sort(fitSort1, testSort1);
                mismatchStr[generation]= string.Format("{0},{1}", generation, fitSort1[0]);
            }
            
            // find best-fitting board.
            var testSort = testPopulation.ToArray();
            var fitSort = fitnesses.ToArray();
            Array.Sort(fitSort, testSort);
            mismatchStr[geneticAlgoConfig.Generations-1]= string.Format("{0},{1}", geneticAlgoConfig.Generations-1, fitSort[0]);

            // Save CSV
            var mismatchCSV = new StringBuilder();
            var temperatureCSV = new StringBuilder();
            foreach (string line in mismatchStr){
                mismatchCSV.AppendLine(line);  
            }

            File.WriteAllText("./data/Tetris_16x16_Mismatches.csv", mismatchCSV.ToString());
            return testSort[0];
        }
    }

    public class FitnessHelper
    {
        public double BoardEnergyFitness(Board chromosome)
        {
            int energy = MismatchCalculator.GetBoardTotalMismatch(chromosome);
            
            return energy;
        }
    }

    public class SelectionHelper
    {
        public Board TournamentSelection(List<Board> population, IEnumerable<double> fitnesses, Random random, int numberOfIndividuals=10)
        {   
            var fitArr = fitnesses.ToArray();
            var popArr = population.ToArray();

            // Choose individuals for the tournament
            int[] chosenIndividualIDs = new int[numberOfIndividuals];
            for (int i=0; i<numberOfIndividuals;i++){
                int randIndividual = random.Next(fitArr.Length);
                chosenIndividualIDs[i] = randIndividual;
            }

            // Get the fittest individual
            double bestFitness = int.MaxValue;
            int fittestID = 0;
            foreach (int ID in chosenIndividualIDs){
                if (fitArr[ID]<bestFitness){
                    fittestID = ID;
                    bestFitness = fitArr[ID];
                }
            }
            
            return popArr[fittestID];
        }
    }
}