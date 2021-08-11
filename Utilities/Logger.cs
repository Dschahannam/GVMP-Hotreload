using System;
using System.Collections.Generic;
using System.Text;

namespace GVMP_HotReload.Utilities
{
    public enum ConsoleLogType
    {
        SUCCESS,
        ERROR
    }

    public class Logger
    {
        public void Console(string Category, string Log)
        {
            System.Console.ResetColor();

            System.Console.Write("[");
            System.Console.ForegroundColor = ConsoleColor.Green;
            System.Console.Write("+");

            System.Console.ResetColor();

            System.Console.WriteLine($"] {Category} >> {Log}");
        }
    }
}
