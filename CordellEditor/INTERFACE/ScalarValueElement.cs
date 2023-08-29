using System.Windows;
using System.Windows.Controls;

namespace CordellEditor.INTERFACE;

public class ScalarValueElement : IElement {
    public ScalarValueElement(string @default) {
        Default = @default;
    }
    
    private string Default { get; }
    
    public Canvas GetBody(int position) {
        var body = new Canvas {
            Height = 50,
            Width = 200,
            VerticalAlignment = VerticalAlignment.Top,
            Margin = new Thickness(10, position * 50, 0, 0),
            Children = {
                new Label {
                    Content = $"{Default}"
                },
                new TextBox {
                    Width = 30,
                    Margin = new Thickness(0, 30, 0, 0),
                    Text = "0"
                }
            }
        };

        return body;
    }
    
    public static double GetScalarFromValues(Canvas canvas) =>
        double.Parse(((TextBox)canvas.Children[1]).Text);
}