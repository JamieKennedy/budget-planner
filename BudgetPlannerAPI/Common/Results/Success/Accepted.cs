namespace Common.Results.Success
{
    public class Accepted : FluentResults.Success
    {
    }

    public class Accepted<T> : FluentResults.Success
    {
        public T Item { get; init; }

        public Accepted(T item) : base()
        {
            Item = item;
        }
    }
}
