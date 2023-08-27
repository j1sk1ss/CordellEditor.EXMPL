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
                            Content = "Чёрный"
                        },
                        new ComboBoxItem {
                            Content = "Синий"
                        },
                        new ComboBoxItem {
                            Content = "Циан"
                        },
                        new ComboBoxItem {
                            Content = "Серый"
                        },
                        new ComboBoxItem {
                            Content = "Зелёный"
                        },
                        new ComboBoxItem {
                            Content = "Пурпурный"
                        },
                        new ComboBoxItem {
                            Content = "Красный"
                        },
                        new ComboBoxItem {
                            Content = "Белый"
                        },
                        new ComboBoxItem {
                            Content = "Жёлтый"
                        },
                        new ComboBoxItem {
                            Content = "Тёмно-синий"
                        },
                        new ComboBoxItem {
                            Content = "Тёмно-циановый"
                        },
                        new ComboBoxItem {
                            Content = "Тёмно-серый"
                        },
                        new ComboBoxItem {
                            Content = "Тёмно-зелёный"
                        },
                        new ComboBoxItem {
                            Content = "Тёмно-пурпурный"
                        },
                        new ComboBoxItem {
                            Content = "Тёмно-красный"
                        },
                        new ComboBoxItem {
                            Content = "Тёмно-жёлтый"
                        }
                    }
                }
            }
        };

        return body;
    }
}