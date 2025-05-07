namespace Exceptions
{
    public class QueryFailedException : Exception
    {
        public QueryFailedException(string message, Exception inner = null) : base(message, inner) { }
    }
}
