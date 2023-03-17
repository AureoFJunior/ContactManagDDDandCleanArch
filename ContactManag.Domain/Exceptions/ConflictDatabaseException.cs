namespace ContactManag.Domain
{
    public class ConflictDatabaseException : Exception
    {
        public ConflictDatabaseException()
        {
        }

        public ConflictDatabaseException(string message)
            : base(message)
        {
        }

        public ConflictDatabaseException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
