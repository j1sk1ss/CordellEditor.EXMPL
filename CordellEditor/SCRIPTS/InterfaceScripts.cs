using System.Collections.Generic;
using CordellEditor.INTERFACE;

namespace CordellEditor.SCRIPTS;

public static class InterfaceScripts {
    public static readonly Dictionary<string, List<IElement>> InterfaceValues = new() {
        {"Sphere", new List<IElement> {
            new NameValueElement(),
            new VectorValueElement(),
            new ScalarValueElement(),
            new ColorValueElement()
        }},
        {"Cube", new List<IElement> {
            new NameValueElement(),
            new VectorValueElement(),
            new VectorValueElement(),
            new ColorValueElement()
        }},
        {"Line", new List<IElement> {
            new NameValueElement(),
            new VectorValueElement(),
            new VectorValueElement(),
            new ScalarValueElement(),
            new ColorValueElement()
        }},
        {"Polygon", new List<IElement> {
            new NameValueElement(),
            new VectorValueElement(),
            new VectorValueElement(),
            new VectorValueElement(),
            new ColorValueElement()
        }},
        {"Light", new List<IElement> {
            new NameValueElement(),
            new VectorValueElement(),
            new ScalarValueElement()
        }},
        {"Collection", new List<IElement> {
            new NameValueElement()
        }}
    };
}