using System;
using System.Collections.Generic;
using System.Windows.Controls;
using CordellEditor.INTERFACE;

using Engine3D.EXMPL.OBJECTS;

namespace CordellEditor.SCRIPTS;

public static class InterfaceScripts {
    public static Vector3 GetVectorFromValues(Canvas canvas) {
        return new Vector3(double.Parse(((TextBox)canvas.Children[1]).Text),
            double.Parse(((TextBox)canvas.Children[2]).Text), double.Parse(((TextBox)canvas.Children[3]).Text));
    }
    
    public static double GetScalarFromValues(Canvas canvas) {
        return double.Parse(((TextBox)canvas.Children[1]).Text);
    }

    public static ConsoleColor GetColorFromValues(Canvas canvas) {
        return ColorConvertor.NamedColors[
            ((ComboBoxItem)((ComboBox)canvas.Children[1]).Items[(((ComboBox)canvas.Children[1]).SelectedIndex)]).Content
            .ToString()!];
    }

    public static string GetNameFromValues(Canvas canvas) {
        return ((TextBox)canvas.Children[1]).Text;
    }
    
    public static string GetNameFromObjects(Canvas canvas) {
        return ((Label)canvas.Children[0]).ToString()!;
    }
    
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
        }}
    };
}