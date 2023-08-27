using System;
using System.Collections.Generic;
using System.Drawing;

namespace CordellEditor.SCRIPTS;

public static class ColorConvertor {
    public static readonly Dictionary<ConsoleColor, Color> Colors = new() {
        { ConsoleColor.Black , Color.Black},
        { ConsoleColor.Blue , Color.Blue},
        { ConsoleColor.Cyan , Color.Aquamarine},
        { ConsoleColor.Gray , Color.Gray},
        { ConsoleColor.Green , Color.Green},
        { ConsoleColor.Magenta , Color.Magenta},
        { ConsoleColor.Red , Color.Red},
        { ConsoleColor.White , Color.White},
        { ConsoleColor.Yellow , Color.Yellow},
        { ConsoleColor.DarkBlue , Color.DarkBlue},
        { ConsoleColor.DarkCyan , Color.DarkCyan},
        { ConsoleColor.DarkGray , Color.DarkGray},
        { ConsoleColor.DarkGreen , Color.DarkGreen},
        { ConsoleColor.DarkMagenta , Color.DarkMagenta},
        { ConsoleColor.DarkRed , Color.DarkRed},
        { ConsoleColor.DarkYellow , Color.YellowGreen}
    };
    
    public static readonly Dictionary<string, ConsoleColor> NamedColors = new() {
        { "Black", ConsoleColor.Black },
        { "Blue", ConsoleColor.Blue },
        { "Cyan", ConsoleColor.Cyan },
        { "Gray", ConsoleColor.Gray },
        { "Green", ConsoleColor.Green },
        { "Magenta", ConsoleColor.Magenta },
        { "Red", ConsoleColor.Red },
        { "White", ConsoleColor.White },
        { "Yellow", ConsoleColor.Yellow },
        { "Dark Blue", ConsoleColor.DarkBlue },
        { "Dark Cyan", ConsoleColor.DarkCyan },
        { "Dark Gray", ConsoleColor.DarkGray },
        { "Dark Green", ConsoleColor.DarkGreen },
        { "Dark Magenta", ConsoleColor.DarkMagenta },
        { "Dark Red", ConsoleColor.DarkRed },
        { "Dark Yellow", ConsoleColor.DarkYellow }
    };
}