namespace Serdiuk.Rabbit.Core.Exceptions
{
    public class ForumException : Exception
    {
        public ForumException() : base()
        {

        }
        public ForumException(string message) : base(message)
        {

        }
        public ForumException(string message, Exception inner) : base(message, inner)
        {

        }
    }
}
