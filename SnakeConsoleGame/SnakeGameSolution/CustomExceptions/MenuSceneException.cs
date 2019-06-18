namespace SnakeGame.CustomExceptions
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    public class MenuSceneException : Exception
    {
        public MenuSceneException()
        {

        }

        public MenuSceneException(string message) 
            : base(message)
        {

        }

        public MenuSceneException(string message, Exception inner) 
            : base(message, inner)
        {

        }

        protected MenuSceneException(SerializationInfo info, StreamingContext context) 
            : base(info, context)
        {

        }
    }
}
