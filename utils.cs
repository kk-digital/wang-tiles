
namespace wang
{
    public static class Utils
    {
        public static int ToRGBA8(char r, char g, char b, char a)
        {
            return r << 24 + g << 16 + b << 8 + a;
        }

        public static Int64 GenerateID()
        {
            DateTimeOffset dto = new DateTimeOffset();
            Random random = new Random();

            Int32 timeInSeconds = (Int32)dto.ToUnixTimeSeconds();
            Int32 randomNumber = (Int32)random.NextInt64();

            return CreateID(timeInSeconds, randomNumber);
        }

        public static Int64 CreateID(Int32 timeInSeconds, Int32 randomNumber)
        {
            Int64 result = ((Int64)timeInSeconds << 32) + (Int64)randomNumber;

            return result;
        }

        public static Int32 GetTimeFromID(Int64 id)
        {
            return (Int32)(id >> 32);
        }

        public static Int32 GetRandomNumberFromID(Int64 id)
        {
            return (Int32)((id << 32) >> 32);
        }

        public static string IDToString(Int64 id)
        {
            //TODO(Mahdi): empty for now
            return "";
        }
    }
}