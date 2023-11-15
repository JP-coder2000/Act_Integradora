public static class BulletCounter
{
    public static int Count { get; private set; } = 0;

    public static void Increment()
    {
        Count++;
    }

    public static void Decrement()
    {
        if (Count > 0) 
        {
            Count--;
        }
    }
}
