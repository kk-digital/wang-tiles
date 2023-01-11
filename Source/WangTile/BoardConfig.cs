namespace WangTile{
    
    public class BoardConfig {
        public int Width;
        public int Height;
        public string OutputName;
        public ColorMatching ColorMatching; 
        public string MapJsonDirectory;
        public string MapJsonFilename;
        public bool SaveImage;

        public BoardConfig(int width, int height, string outputName, ColorMatching colorMatching, string mapJsonDirectory, string mapJsonFilename, bool saveImage = true) {
            this.Width=width;
            this.Height=height;
            this.OutputName=outputName;
            this.ColorMatching=colorMatching; 
            this.MapJsonDirectory=mapJsonDirectory;
            this.MapJsonFilename=mapJsonFilename;
            this.SaveImage=saveImage;
        }
    }

    public class TiledSimulatedAnnealingBoardConfig:BoardConfig {
        public int Iterations; 
        public float Temperature;
        public int LIteration;
        public float Alpha;

        public TiledSimulatedAnnealingBoardConfig(int width, int height, string outputName, ColorMatching colorMatching, int iterations, float temperature, int lIteration, float alpha, string mapJsonDirectory, string mapJsonFilename, bool saveImage = true): base(width, height, outputName, colorMatching, mapJsonDirectory, mapJsonFilename, saveImage)
        {
            this.Iterations=iterations; 
            this.Temperature=temperature;
            this.LIteration=lIteration;
            this.Alpha=alpha;
        }
    }
}