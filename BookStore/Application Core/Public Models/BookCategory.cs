namespace Application_Core.Public_Models
{
    [Flags]
    public enum BookCategory
    {
        None = 0,
        Fantasy = 1,
        ScienceFiction = 2,
        Mystery = 4,
        Romance = 8,
        HistoricalFiction = 16,
        Horror = 32,
        YoungAdult = 64,
        Thriller = 128,
        Biography = 256,
        MemoirNone = 512
    }
}
