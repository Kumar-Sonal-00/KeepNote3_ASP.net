using System;
using System.Runtime.Serialization;

namespace KeepNote.Controllers
{
    [Serializable]
    internal class ReminderAlreadyExistsException : Exception
    {
        public ReminderAlreadyExistsException()
        {
        }

        public ReminderAlreadyExistsException(string message) : base(message)
        {
        }

        public ReminderAlreadyExistsException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ReminderAlreadyExistsException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}