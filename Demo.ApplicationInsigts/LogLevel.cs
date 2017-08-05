using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.ApplicationInsigts
{
    public enum LogLevel
    {
        Debug = 1,
        Verbose = 2,
        Information = 3,
        Warning = 4,
        Error = 5,
        Critical = 6,
        None = int.MaxValue
    }
}
