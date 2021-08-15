using System;
using System.Threading;

namespace HelperProject.Logging
{
    public static class Logger
    {
        public static void Error(string message) =>
            Console.WriteLine($"[ERROR][{Thread.CurrentThread.ManagedThreadId}][{DateTime.Now}] {message}");

        public static void Warning(string message) =>
            Console.WriteLine($"[WARNING][{Thread.CurrentThread.ManagedThreadId}][{DateTime.Now}] {message}");

        public static void Info(string message) =>
            Console.WriteLine($"[INFO][{Thread.CurrentThread.ManagedThreadId}][{DateTime.Now}] {message}");

        public static void Step(string message) =>
            Console.WriteLine($"     [STEP][{Thread.CurrentThread.ManagedThreadId}][{DateTime.Now}] {message}");
    }
}