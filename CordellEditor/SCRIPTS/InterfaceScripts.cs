using System.Collections.Generic;
using CordellEditor.INTERFACE;

namespace CordellEditor.SCRIPTS;

public static class InterfaceScripts {
    public static readonly Dictionary<string, List<IElement>> InterfaceValues = new() {
        {"Sphere", new List<IElement> {
            new NameValueElement(),
            new VectorValueElement("Position"),
            new ScalarValueElement("Radius"),
            new ColorValueElement()
        }},
        {"Cube", new List<IElement> {
            new NameValueElement(),
            new VectorValueElement("Position"),
            new VectorValueElement("Size"),
            new ColorValueElement()
        }},
        {"Line", new List<IElement> {
            new NameValueElement(),
            new VectorValueElement("First point"),
            new VectorValueElement("Second point"),
            new ScalarValueElement("Thickness"),
            new ColorValueElement()
        }},
        {"Polygon", new List<IElement> {
            new NameValueElement(),
            new VectorValueElement("First point"),
            new VectorValueElement("Second point"),
            new VectorValueElement("Third point"),
            new ColorValueElement()
        }},
        {"Light", new List<IElement> {
            new NameValueElement(),
            new VectorValueElement("Position"),
            new ScalarValueElement("Strength")
        }},
        {"Collection", new List<IElement> {
            new NameValueElement()
        }}
    };
}