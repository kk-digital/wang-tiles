
namespace Wang.MathW
{
    static class Distance
    {
        public static float EuclidianDistance(float firstX, float firstY, float secondX, float secondY)
        {
            float diffX = firstX - secondX;
            float diffY = firstY - secondY;

            return MathF.Sqrt(diffX * diffX + diffY * diffY);
        }

        public static float ManhatamDistance(float firstX, float firstY, float secondX, float secondY)
        { 
            return Math.Abs(firstX - secondX) + System.Math.Abs(firstY - secondY);
        }

        // Minkowski Distance?
        // Hamming Distance?
    }
}
