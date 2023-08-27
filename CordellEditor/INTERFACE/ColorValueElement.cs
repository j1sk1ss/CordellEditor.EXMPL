using System.Windows;
using System.Windows.Controls;

namespace CordellEditor.INTERFACE;

public class ColorValueElement : IElement {
    public Canvas GetBody(int position) {
        var body = new Canvas {
            Height = 50,
            Width = 200,
            VerticalAlignment = VerticalAlignment.Top,
            Margin = new Thickness(10, position * 50, 0, 0),
            Children = {
                new Label {
                    Content = $"Color {position}"
                },
                new ComboBox {
                    Margin = new Thickness(0, 30, 0, 0),
                    Width = 70,
                    SelectedIndex = 0,
                    Items = {
                        new ComboBoxItem {
                            Content = "Black"
                        },
                        new ComboBoxItem {
                            Content = "Blue"
                        },
                        new ComboBoxItem {
                            Content = "Cyan"
                        },
                        new ComboBoxItem {
                            Content = "Gray"
                        },
                        new ComboBoxItem {
                            Content = "Green"
                        },
                        new ComboBoxItem {
                            Content = "Magenta"
                        },
                        new ComboBoxItem {
                            Content = "Red"
                        },
                        new ComboBoxItem {
                            Content = "White"
                        },
                        new ComboBoxItem {
                            Content = "Yellow"
                        },
                        new ComboBoxItem {
                            Content = "Dark Blue"
                        },
                        new ComboBoxItem {
                            Content = "Dark Cyan"
                        },
                        new ComboBoxItem {
                            Content = "Dark Gray"
                        },
                        new ComboBoxItem {
                            Content = "Dark Green"
                        },
                        new ComboBoxItem {
                            Content = "Dark Magenta"
                        },
                        new ComboBoxItem {
                            Content = "Dark Red"
                        },
                        new ComboBoxItem {
                            Content = "Dark Yellow"
                        }
                    }
                }
            }
        };

        return body;
    }
}