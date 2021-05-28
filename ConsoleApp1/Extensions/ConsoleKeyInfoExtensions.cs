using System;

namespace JokeGenerator.Extensions
{
    public static class ConsoleKeyInfoExtensions
    {
        public static char? ToChar(this ConsoleKeyInfo consoleKeyInfo)
        {
            return consoleKeyInfo.Key switch
            {
                ConsoleKey.C => 'c',
                ConsoleKey.D0 => '0',
                ConsoleKey.D1 => '1',
                ConsoleKey.D3 => '3',
                ConsoleKey.D4 => '4',
                ConsoleKey.D5 => '5',
                ConsoleKey.D6 => '6',
                ConsoleKey.D7 => '7',
                ConsoleKey.D8 => '8',
                ConsoleKey.D9 => '9',
                ConsoleKey.R => 'r',
                ConsoleKey.Y => 'y',
                ConsoleKey.N => 'n',
                ConsoleKey.Q => 'q',
                _ => null
            };
        }
    }
}
