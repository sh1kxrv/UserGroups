using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserGroups.Code.IO
{
    /// <summary>
    /// Этот класс отвечает за вывод
    /// </summary>
    public static class NotifyIO
    {
        private static void OutWithColor(string text, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.Write(text);
            Console.ResetColor();
        }
        public static void PrintLn(string txt) => Print($"{txt}\n");
        public static void Print(string txt) => OutWithColor($"{txt}", ConsoleColor.White);
        public static void ErrorLn(string error) => OutWithColor($"{error}\n", ConsoleColor.DarkRed);
    }
}
