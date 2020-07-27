using System;
using System.Threading;

namespace MagicWindBall
{
    class Program
    {
        static void Main(string[] args)
        {
            WriteWrappedHeader("Magic WindBall");

            Console.WriteLine();

            Write("thinking.", ConsoleColor.Cyan);
            Wait(500);
            Write(".", ConsoleColor.Cyan);
            Wait(500);
            Write(".", ConsoleColor.Cyan);
            Wait(500);

            Write(" consulting the stars.", ConsoleColor.Yellow);
            Wait(1000);
            Write(".", ConsoleColor.Yellow);
            Wait(500);
            Write(".", ConsoleColor.Yellow);
            Wait(500);            

            Write(" guessing some more.", ConsoleColor.Cyan);
            Wait(1000);
            Write(".", ConsoleColor.Cyan);
            Wait(500);
            Write(".", ConsoleColor.Cyan);
            Wait(1000);
            Console.WriteLine('\n');

            var rnd = new Random(DateTime.UtcNow.Millisecond);
            var val = rnd.Next(6);
            

            Console.WriteLine("Wind today be:");

            switch (val)
            {

                case 0:
                    WriteLine("No Wind - Skunked again.", ConsoleColor.DarkYellow);
                    break;
                case 1:
                    WriteLine("Light Wind - Take a nap, pray for wind.", ConsoleColor.DarkCyan);
                    break;
                case 2:
                    WriteLine("Light Breeze - Sucker wind.", ConsoleColor.DarkGreen);
                    break;
                case 3:
                    WriteLine("Breezy- Wake up and get ready.", ConsoleColor.Green);
                    break;
                case 4:
                    WriteLine("Windy- What the hell are you doing at the keyboard?", ConsoleColor.Yellow);
                    break;
                case 5:
                    WriteLine("Super Nuker- Get out and rig your Hankie.", ConsoleColor.Red);
                    break;
                default:
                    WriteLine("Roll the Dice - It's coming in waves.");
                    break;
            }

            Console.WriteLine();
        }

        static void Wait(int ms)
        {
            Thread.Sleep(ms);
        }

        #region Console Helpers

        static void WriteLine(string text, ConsoleColor? color = null)
        {
            if (color.HasValue)
            {
                var oldColor = System.Console.ForegroundColor;
                if (color == oldColor)
                    Console.WriteLine(text);
                else
                {
                    Console.ForegroundColor = color.Value;
                    Console.WriteLine(text);
                    Console.ForegroundColor = oldColor;
                }
            }
            else
                Console.WriteLine(text);
        }

        static void Write(string text, ConsoleColor? color = null)
        {
            if (color.HasValue)
            {
                var oldColor = System.Console.ForegroundColor;
                if (color == oldColor)
                    Console.Write(text);
                else
                {
                    Console.ForegroundColor = color.Value;
                    Console.Write(text);
                    Console.ForegroundColor = oldColor;
                }
            }
            else
                Console.Write(text);
        }

        /// <summary>
        /// Writes a line of header text wrapped in a in a pair of lines of dashes:
        /// -----------
        /// Header Text
        /// -----------
        /// and allows you to specify a color for the header. The dashes are colored
        /// </summary>
        /// <param name="headerText">Header text to display</param>
        /// <param name="wrapperChar">wrapper character (-)</param>
        /// <param name="headerColor">Color for header text (yellow)</param>
        /// <param name="dashColor">Color for dashes (gray)</param>
        static void WriteWrappedHeader(string headerText,
                                                char wrapperChar = '-',
                                                ConsoleColor headerColor = ConsoleColor.Yellow,
                                                ConsoleColor dashColor = ConsoleColor.DarkGray)
        {
            if (string.IsNullOrEmpty(headerText))
                return;

            string line = new string(wrapperChar, headerText.Length);

            WriteLine(line, dashColor);
            WriteLine(headerText, headerColor);
            WriteLine(line, dashColor);
        }

        #endregion
    }
}
