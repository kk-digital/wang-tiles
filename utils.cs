
namespace wang
{
    public static class Utils
    {
        public static int ToRGBA8(char r, char g, char b, char a)
        {
            return r << 24 + g << 16 + b << 8 + a;
        }
    }
}