using System;
using System.Collections.Generic;
using System.Text;

namespace Exceptions
{
    public class CategoryNotFoundException:ApplicationException
    {
        public CategoryNotFoundException() { }
        public CategoryNotFoundException(string message) : base(message) { }
    }
}
