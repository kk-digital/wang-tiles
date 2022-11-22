

// namespace Wang.MathW
// {
//     public class Perlin1D
//     {
//         public static readonly float[] GradientArray = new float[16] 
//         {
//             0.0f, 
//             0.0666666666666667f, 
//             0.1333333333333334f, 
//             0.20000000000000007f, 
//             0.2666666666666668f, 
//             0.3333333333333335f, 
//             0.40000000000000013f, 
//             0.46666666666666684f, 
//             0.5333333333333335f, 
//             0.6000000000000002f, 
//             0.666666666666667f, 
//             0.7333333333333336f, 
//             0.8000000000000003f, 
//             0.866666666666667f, 
//             0.9333333333333337f,
//             1f
//         };

//         private const int PrimeX = 501125321;
//         public int Seed;
//         public float SamplingRate;   // Sampling frequency/
//         public float Gain { get; private set; }
//         public float Lacunarity;    // Control for the gap between successive noise frequencies (sucessive octaves).

//         // The number of interations to perform.
//         public int Octaves { get; private set; }
//         public float Offset;
//         public float FirstOctaveWeight;

//         public Perlin1D(int seed = 1400, int samplingRate = 10, float gain = 0.5f, 
//             float lacunarity = 2.0f, int octaves = 3)
//         {
//             Seed = seed;
//             SamplingRate = samplingRate;
//             Gain = gain;
//             Lacunarity = lacunarity;
//             Octaves = octaves;

//             CalculateFirstOctaveWeight();
//         }

//         public void SetGain(float gain)
//         {
//             Gain = gain;
//             CalculateFirstOctaveWeight();
//         }
//         public void SetOctaves(int octaves)
//         {
//             Octaves = octaves;
//             CalculateFirstOctaveWeight();
//         }

//         private void CalculateFirstOctaveWeight()
//         {
//             float CumulativeGain = 1.0f;
//             float gain = Gain;
//             for (int i = 1; i < Octaves; i++)
//             {
//                 CumulativeGain += gain;
//                 gain *= Gain;
//             }
//             FirstOctaveWeight = 1 / CumulativeGain;
//         }

//         // Hash algorithm from FastNoiseLite by Auburn Jordan Peck
//         // https://github.com/Auburn/FastNoiseLite/blob/eb070e75b16f50027185735316e5ae5b441707c8/CSharp/FastNoiseLite.cs#L587
//         // Todo: Check correcness of it. Does it retun uniform distributed values
//         // Todo: Improve this algorithm?
//         private int Hash(int xPrimed)
//         {
//             int hash = Seed ^ xPrimed;

//             hash *= 0x27d4eb2d;
//             return hash;
//         }

//         private int GetGradIndex(int xPrimed)
//         {
//             int index = Hash(xPrimed);
//             index ^= index >> 15;
//             return index & 15;
//         }

//         public float GetNoise(float x)
//         {
//             float sum = 0;
//             float weight = FirstOctaveWeight;
//             x += Offset;
//             for (int i = 0; i < Octaves; i++)
//             {
//                 float noise = GetNoiseSingle(x);
//                 sum += noise * weight;

//                 x *= Lacunarity;
//                 weight *= Gain;
//             }
//             return sum;
//         }

//         public float GetNoiseSingle(float x)
//         {
//             x /= SamplingRate;

//             int x0 = NoiseUtility.FastFloor(x);

//             x = x - x0;

//             x0 *= PrimeX;
//             int x1 = x0 + PrimeX;
//             int gi0 = GetGradIndex(x0);
//             int gi1 = GetGradIndex(x1);

//             float u = NoiseUtility.Fade(x);

//             float nx00 = NoiseUtility.Lerp(GradientArray[gi0], GradientArray[gi1], u);

//             return nx00;
//         }

//     }
// }
