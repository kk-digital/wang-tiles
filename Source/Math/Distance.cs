
namespace Wang.MathW
{
    static class Distance
    {
        public static float EuclidianDistance(int firstX, int firstY, int secondX, int secondY)
        {
            int diffX = firstX - secondX;
            int diffY = firstY - secondY;

            return MathF.Sqrt(diffX * diffX + diffY * diffY);
        }

        public static int ManhatamDistance(int firstX, int firstY, int secondX, int secondY)
        { 
            return Math.Abs(firstX - secondX) + System.Math.Abs(firstY - secondY);
        }

        // Minkowski Distance?
        // Hamming Distance?
    }
}
