using System.Windows.Controls;

namespace CordellEditor.INTERFACE;

public interface IElement {
    public Canvas GetBody(int position);
}