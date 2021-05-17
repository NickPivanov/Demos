using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleReadText
{
   public static class VisualSugar
    {
        public static void DecorateTitle(string title, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine($"\n\t{title}");
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
