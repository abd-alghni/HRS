﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRS.Core.Exceptions
{
    public class InvalidDateException : Exception 
    {
        public InvalidDateException():base("Invalid Date")
        {

        }
    }
}
