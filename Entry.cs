namespace wang_tiles 
{
    public struct Entry 
    {
        public Entry(int lVarientIndex, int lCount)
        {
            VarientIndex = lVarientIndex;
            Count = lCount;
        }
        public int VarientIndex;
        public int Count;
    };
}