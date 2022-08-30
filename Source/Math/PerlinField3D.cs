namespace Wang.MathW
{

    public class PerlinField3D
    {
        /// <summary>
        /// Kerlin Perlin's 16 vectors.
        /// </summary>
        public static readonly float[] GradientArray = new float[48] 
        {
                 0.70710f, 0.70710f, 0f,
                -0.70710f, 0.70710f, 0f,
                 0.70710f,-0.70710f, 0f,
                -0.70710f,-0.70710f, 0f,
                 0.70710f, 0f, 0.70710f,
                -0.70710f, 0f, 0.70710f,
                 0.70710f, 0f,-0.70710f,
                -0.70710f, 0f,-0.70710f,
                 0f, 0.70710f, 0.70710f,
                 0f,-0.70710f, 0.70710f,
                 0f, 0.70710f,-0.70710f,
                 0f,-0.70710f,-0.70710f,

                 0.70710f, 0.70710f, 0f,
                -0.70710f, 0.70710f, 0f,
                 0f,-0.70710f, 0.70710f,
                 0f,-0.70710f,-0.70710f
            };

        private const int PrimeX = 501125321;
        private const int PrimeY = 1136930381;
        private const int PrimeZ = 1720413743;

        public int Seed;
        public float SamplingRate;   // Sampling frequency/

        public float Gain { get; private set; }
        public float Lacunarity;    // Control for the gap between successive noise frequencies (sucessive octaves).
        public int Octaves { get; private set; }  // The number of interations to perform.
        public float Offset;
        public float FirstOctaveWeight;

        public PerlinField3D(int seed = 1400, int samplingRate = 10, float gain = 0.5f,
                float lacunarity = 2.0f, int octaves = 3)
        {
            Seed = seed;
            SamplingRate = samplingRate;
            Gain = gain;
            Lacunarity = lacunarity;
            Octaves = octaves;

            CalculateFirstOctaveWeight();
        }

        public void SetGain(float gain)
        {
            Gain = gain;
            CalculateFirstOctaveWeight();
        }
        public void SetOctaves(int octaves)
        {
            Octaves = octaves;
            CalculateFirstOctaveWeight();
        }

        private void CalculateFirstOctaveWeight()
        {
            float CumulativeGain = 1.0f;
            float gain = Gain;
            for (int i = 1; i < Octaves; i++)
            {
                CumulativeGain += gain;
                gain *= Gain;
            }
            FirstOctaveWeight = 1 / CumulativeGain;
        }


        // Hash algorithm from FastNoiseLite by Auburn Jordan Peck
        // https://github.com/Auburn/FastNoiseLite/blob/eb070e75b16f50027185735316e5ae5b441707c8/CSharp/FastNoiseLite.cs#L587
        // Todo: Check correcness of it. Does it retun uniform distributed values
        // Todo: Improve this algorithm?
        public int Hash(int xPrimed, int yPrimed, int zPrimed)
        {
            int hash = Seed ^ xPrimed ^ yPrimed ^ zPrimed;

            hash *= 0x27d4eb2d;
            return hash;
        }

        public int GetGradIndex(int xPrimed, int yPrimed, int zPrimed)
        {
            int index = Hash(xPrimed, yPrimed, zPrimed);
            index ^= index >> 15;
            return index & 15;
        }

        public float GetNoise(float x, float y, float z)
        {
            float sum = 0;
            x += Offset;
            float weight = FirstOctaveWeight;
            for (int i = 0; i < Octaves; i++)
            {
                float noise = GetNoiseSingle(x, y, z);
                sum += noise * weight;

                x *= Lacunarity;
                y *= Lacunarity;
                z *= Lacunarity;
                weight *= Gain;
            }
            return sum;
        }

        public float GetNoiseSingle(float x, float y, float z)
        {
            x /= SamplingRate;
            y /= SamplingRate;
            z /= SamplingRate;

            int x0 = NoiseUtility.FastFloor(x);
            int y0 = NoiseUtility.FastFloor(y);
            int z0 = NoiseUtility.FastFloor(z);

            x = x - x0;
            y = y - y0;
            z = z - z0;

            x0 *= PrimeX;
            y0 *= PrimeY;
            z0 *= PrimeZ;
            int x1 = x0 + PrimeX;
            int y1 = y0 + PrimeY;
            int z1 = z0 + PrimeZ;

            int gi000 = GetGradIndex(x0, y0, z0);
            int gi001 = GetGradIndex(x0, y0, z1);
            int gi010 = GetGradIndex(x0, y1, z0);
            int gi011 = GetGradIndex(x0, y1, z1);

            int gi100 = GetGradIndex(x1, y0, z0);
            int gi101 = GetGradIndex(x1, y0, z1);
            int gi110 = GetGradIndex(x1, y1, z0);
            int gi111 = GetGradIndex(x1, y1, z1);

            // Calculate noise contributions from each of the eight corners.
            float n000 = DotHelper(gi000, x, y, z);
            float n001 = DotHelper(gi001, x, y, z - 1);
            float n010 = DotHelper(gi010, x, y - 1, z);
            float n011 = DotHelper(gi011, x, y - 1, z - 1);
            float n100 = DotHelper(gi100, x - 1, y, z);
            float n101 = DotHelper(gi101, x - 1, y, z - 1);
            float n110 = DotHelper(gi110, x - 1, y - 1, z);
            float n111 = DotHelper(gi111, x - 1, y - 1, z - 1);

            float xs = NoiseUtility.Fade(x);
            float ys = NoiseUtility.Fade(y);
            float zs = NoiseUtility.Fade(z);

            float nx00 = NoiseUtility.Lerp(n000, n100, xs);
            float nx01 = NoiseUtility.Lerp(n001, n101, xs);
            float nx10 = NoiseUtility.Lerp(n010, n110, xs);
            float nx11 = NoiseUtility.Lerp(n011, n111, xs);

            float nxy0 = NoiseUtility.Lerp(nx00, nx10, ys);
            float nxy1 = NoiseUtility.Lerp(nx01, nx11, ys);

            float nxyz = NoiseUtility.Lerp(nxy0, nxy1, zs);

            return nxyz * NoiseUtility.SQUARE_ROOT_2;
        }

        private float DotHelper(int id, float x, float y, float z)
        {
            float x0 = GradientArray[id];
            float y0 = GradientArray[id + 1];
            float z0 = GradientArray[id + 2];
            return NoiseUtility.Dot(x0, y0, z0, x, y, z);
        }
    }
}