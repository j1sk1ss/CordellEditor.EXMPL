using System.Windows;
using System.Windows.Controls;

namespace CordellEditor.INTERFACE;

public class NameValueElement : IElement {
    public Canvas GetBody(int position) {
        var body = new Canvas {
            Height = 50,
            Width = 200,
            VerticalAlignment = VerticalAlignment.Top,
            Margin = new Thickness(10, position * 50, 0, 0),
            Children = {
                new Label {
                    Content = $"Name {position}"
                },
                new TextBox {
                    Width = 100,
                    Margin = new Thickness(0, 30, 0, 0),
                    Text = "Default"
                }
            }
        };

        return body;
    }
    
    public static string GetNameFromValues(Canvas canvas) =>
        ((TextBox)canvas.Children[1]).Text;
}