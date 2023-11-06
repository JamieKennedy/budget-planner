namespace Common.Results.Success
{
    public class Created<T> : FluentResults.Success
    {
        public string RouteName { get; init; }
        public object Values { get; init; }
        public T Item { get; init; }

        public Created(string routeName, object values, T item) : base()
        {
            RouteName = routeName;
            Values = values;
            Item = item;
        }

    }
}
