using System;
using System.Runtime.Serialization;

namespace KeepNote.Controllers
{
    [Serializable]
    internal class NoteAlreadyExistsException : Exception
    {
        public NoteAlreadyExistsException()
        {
        }

        public NoteAlreadyExistsException(string message) : base(message)
        {
        }

        public NoteAlreadyExistsException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected NoteAlreadyExistsException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}