using System;
using System.Collections.Generic;
using System.Text;

namespace Exceptions
{
    public class ReminderNotFoundException:ApplicationException
    {
        public ReminderNotFoundException() { }
        public ReminderNotFoundException(string message) : base(message) { }
    }
}
