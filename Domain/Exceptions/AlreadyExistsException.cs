﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public abstract class AlreadyExistsException : Exception
    {
        protected AlreadyExistsException(string message) : base(message) { }
    }
}
