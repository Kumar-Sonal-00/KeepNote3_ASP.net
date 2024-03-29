﻿using System;
using System.Runtime.Serialization;

namespace KeepNote.Controllers
{
    [Serializable]
    internal class CategoryAlreadyExistsException : Exception
    {
        public CategoryAlreadyExistsException()
        {
        }

        public CategoryAlreadyExistsException(string message) : base(message)
        {
        }

        public CategoryAlreadyExistsException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected CategoryAlreadyExistsException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}