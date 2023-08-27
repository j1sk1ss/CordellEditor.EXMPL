using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace CordellEditor.INTERFACE;

public static class ObjectElement {
    public static Canvas GetBody(string name, int position, MainWindow mainWindow) {
        var body = new Canvas {
            Height = 50,
            Width = 200,
            VerticalAlignment = VerticalAlignment.Top,
            Margin = new Thickness(10, position * 50, 0, 0),
            Children = {
                new Label {
                    Content = $"{name}"
                },
                new Line {
                    X1 = 0,
                    Y1 = 0,
                    X2 = 200,
                    Y2 = 0,
                    Stroke = Brushes.Black
                },
                new Line {
                    X1 = 0,
                    Y1 = 50,
                    X2 = 200,
                    Y2 = 50,
                    Stroke = Brushes.Black
                },
                new Line {
                    X1 = 0,
                    Y1 = 0,
                    X2 = 0,
                    Y2 = 50,
                    Stroke = Brushes.Black
                },
                new Line {
                    X1 = 200,
                    Y1 = 0,
                    X2 = 200,
                    Y2 = 50,
                    Stroke = Brushes.Black
                },
            }
        };

        var firstButton = new Button {
            Height = 25,
            Width = 25,
            Margin = new Thickness(155, 25, 0, 0),
            Content = "-",
            Name = $"Delete_{name}"
        };
        firstButton.Click += mainWindow.DeleteObject;
        
        var secondButton = new Button {
            Height = 25,
            Width = 40,
            Margin = new Thickness(110,25,0,0),
            Content = "Select",
            Name = $"Select_{name}"
        };
        secondButton.Click += mainWindow.SelectObject;

        body.Children.Add(firstButton);
        body.Children.Add(secondButton);
        
        return body;
    }
}