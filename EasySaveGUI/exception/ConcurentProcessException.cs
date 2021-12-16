using System;
using System.Runtime.Serialization;

namespace EasySaveGUI.exception
{
    [Serializable]
    internal class ConcurentProcessException : Exception
    {
        public ConcurentProcessException()
        {
        }

        public ConcurentProcessException(string message) : base(message)
        {
        }

        public ConcurentProcessException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ConcurentProcessException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}