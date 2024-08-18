﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Galaxias.Util;
public class Log
{
    public static void Info(string message)
    {
        Console.WriteLine($"[{Thread.CurrentThread.Name}]:" + message);
    }

    public static void Error(string error)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"[{Thread.CurrentThread.Name}]:" + error);
        Console.ForegroundColor = ConsoleColor.White;
    }
}
