using System;
using System.Collections.Generic;
using System.Text;

namespace Exceptions
{
    public class NoteNotFoundException:ApplicationException
    {
        public NoteNotFoundException() { }
        public NoteNotFoundException(string message) : base(message) { }
    }
}
