using System.Windows;
using System.Windows.Controls;
using Engine3D.EXMPL.OBJECTS;

namespace CordellEditor.INTERFACE;

public class VectorValueElement : IElement {
    public VectorValueElement(string @default) {
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
                },
                new TextBox {
                    Width = 30,
                    Margin = new Thickness(35, 30, 0, 0),
                    Text = "0"
                },                
                new TextBox {
                    Width = 30,
                    Margin = new Thickness(70, 30, 0, 0),
                    Text = "0"
                }
            }
        };

        return body;
    }
    
    public static Vector3 GetVectorFromValues(Canvas canvas) =>
        new (double.Parse(((TextBox)canvas.Children[1]).Text),
            double.Parse(((TextBox)canvas.Children[2]).Text), double.Parse(((TextBox)canvas.Children[3]).Text));
}