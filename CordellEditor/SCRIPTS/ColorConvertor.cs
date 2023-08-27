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
        { "Чёрный" , ConsoleColor.Black},
        { "Синий" , ConsoleColor.Blue},
        { "Циан" , ConsoleColor.Cyan},
        { "Серый" , ConsoleColor.Gray},
        { "Зелёный" , ConsoleColor.Green},
        { "Пурпурный" , ConsoleColor.Magenta},
        { "Красный" , ConsoleColor.Red},
        { "Белый" , ConsoleColor.White},
        { "Жёлтый" , ConsoleColor.Yellow},
        { "Тёмно-синий" , ConsoleColor.DarkBlue},
        { "Тёмно-циановый" , ConsoleColor.DarkCyan},
        { "Тёмно-серый" , ConsoleColor.DarkGray},
        { "Тёмно-зелёный" , ConsoleColor.DarkGreen},
        { "Тёмно-пурпурный" , ConsoleColor.DarkMagenta},
        { "Тёмно-красный" , ConsoleColor.DarkRed},
        { "Тёмно-жёлтый" , ConsoleColor.DarkYellow}
    };
}