// using System.Diagnostics;

// // Uses improved perlin algorithm from https://dl.acm.org/doi/10.1145/566654.566636
// // Original algorithm: https://mrl.cs.nyu.edu/~perlin/noise/
// // Compreensive explanetion : https://eev.ee/blog/2016/05/29/perlin-noise/#extending-to-2-d and http://adrianb.io/2014/08/09/perlinnoise.html

// namespace Wang.MathW
// {
//     public class PerlinField2D 
//     {
//         /// <summary>
//         /// Unit vector with angle from 0 to 360.
//         /// </summary>
//         public static readonly float[] GradientArray = new float[16] {
//                  1f, 0f,                // 0
//                  0.70710f, 0.70710f,    // 45
//                  0f, 1f,                // 90
//                 -0.70710f, 0.70710f,    // 135
//                 -1f, 0f,                // 180
//                  0.70710f,-0.70710f,    // 225
//                  0f,-1f,                // 270
//                 -0.70710f,-0.70710f     // 315
//             };

//         private const int PrimeX = 501125321;
//         private const int PrimeY = 1136930381;

//         public int Seed;
//         public float SamplingRate;   // Sampling frequency/
//         public float Gain { get; private set; }
//         public float Lacunarity;    // Control for the gap between successive noise frequencies (sucessive octaves).
//         public int Octaves { get; private set; } // The number of interations to perform.
//         public float Offset;
//         public float FirstOctaveWeight;


//         public PerlinField2D(int seed = 1400, int samplingRate = 10, float gain = 0.5f,
//             float lacunarity = 2.0f, int octaves = 3)
//         {
//             Seed = seed;
//             SamplingRate = samplingRate;
//             Gain = gain;
//             Lacunarity = lacunarity;
//             Octaves = octaves;

//             CalculateFirstOctaveWeight();
//        }

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
//         private int Hash(int xPrimed, int yPrimed)
//         {
//             int hash = Seed ^ xPrimed ^ yPrimed;

//             hash *= 0x27d4eb2d;
//             return hash;
//         }

//         private int GetGradIndex(int xPrimed, int yPrimed)
//         {
//             int index = Hash(xPrimed, yPrimed);
//             index ^= index >> 15;
//             return index & 7;
//         }

//         // Get values from min a bit smaller than -1 and max value a bit bigger than 1.
//         public float[] GetNoiseArray(float[] samples, int sizeX, int sizeY)
//         {
//             if (samples.Length < (sizeX * sizeY))
//                 Debug.Assert(false);

//             for (int y = 0, index = 0; y < sizeY; y++)
//             {
//                 for (int x = 0; x < sizeX; x++)
//                 {
//                     samples[index] = GetNoise(x, y);
//                     index++;
//                 }
//             }

//             return samples;
//         }

//         public float GetNoise(float x, float y)
//         {
//             float sum = 0;
//             x += Offset;
//             float weight = FirstOctaveWeight;
//             for (int i = 0; i < Octaves; i++)
//             {
//                 float noise = GetNoiseSingle(x, y);
//                 sum += noise * weight;

//                 x *= Lacunarity;
//                 y *= Lacunarity;
//                 weight *= Gain;
//             }
//             return sum;
//         }

//         public float GetNoiseSingle(float x, float y) 
//         {
//             x /= SamplingRate;
//             y /= SamplingRate;

//             int x0 = NoiseUtility.FastFloor(x);
//             int y0 = NoiseUtility.FastFloor(y);

//             x = x - x0;
//             y = y - y0;

//             x0 *= PrimeX;
//             y0 *= PrimeY;
//             int x1 = x0 + PrimeX;
//             int y1 = y0 + PrimeY;
//             int gi00 = GetGradIndex(x0, y0);
//             int gi01 = GetGradIndex(x0, y1);
//             int gi10 = GetGradIndex(x1, y0);
//             int gi11 = GetGradIndex(x1, y1);

//             // Calculate noise contributions from each of the eight corners.

//             float n00 = NoiseUtility.Dot(GradientArray[gi00], GradientArray[gi00 + 1], x, y);
//             float n01 = NoiseUtility.Dot(GradientArray[gi01], GradientArray[gi01 + 1], x, y - 1);
//             float n10 = NoiseUtility.Dot(GradientArray[gi10], GradientArray[gi10 + 1], x - 1, y);
//             float n11 = NoiseUtility.Dot(GradientArray[gi11], GradientArray[gi11 + 1], x - 1, y - 1);

//             float u = NoiseUtility.Fade(x);
//             float v = NoiseUtility.Fade(y);

//             float nx00 = NoiseUtility.Lerp(n00, n10, u);
//             float nx10 = NoiseUtility.Lerp(n01, n11, u);
//             float nxy = NoiseUtility.Lerp(nx00, nx10, v);

//             return nxy * NoiseUtility.SQUARE_ROOT_2;
//         }
//     }
// }
