using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using CordellEditor.INTERFACE;
using CordellEditor.SCRIPTS;

using Engine3D.EXMPL._3D_OBJECTS.GEOMETRY.GEOMETRY_OBJECTS;
using Engine3D.EXMPL._3D_OBJECTS.GEOMETRY.LIGHT_OBJECTS;
using Engine3D.EXMPL._3D_OBJECTS.MATERIALS;
using Engine3D.EXMPL.ENGINE_OBJECTS;
using Engine3D.EXMPL.ENGINE_OBJECTS.CAMERA.CHROME_CAMERA;
using Engine3D.EXMPL.OBJECTS;

using Color = System.Drawing.Color;
using Object = Engine3D.EXMPL._3D_OBJECTS.GEOMETRY.Object;

namespace CordellEditor {
    public partial class MainWindow {
        public MainWindow() {
            InitializeComponent();
            
            Scene = new Space(new ChromeCamera(new Vector3(-4,0,0), new Vector3(0), 
                    false, 20, 200, 113), new List<Object>());

            VisualisationTimer = new DispatcherTimer {
                Interval = TimeSpan.FromSeconds(.0001)
            };
            
            VisualisationTimer.Tick += Visualisation;
            VisualisationTimer.Start();

            _movingTimer.Tick += Move;
        }
        
        private DispatcherTimer VisualisationTimer { get; }
        private Space Scene { get; }
        
        private void Visualisation(object? sender, EventArgs e) {
            var data = Scene.GetView();
            var bitmap = new Bitmap(200, 113);
                
            for (var i = 0; i < data.Item1.GetLength(1); i++)
                for (var j = 0; j < data.Item1.GetLength(0); j++) {
                    var color = ColorConvertor.Colors[data.color[j, i]];
                    
                    var red   = Math.Clamp((int)(color.R * data.light[j, i]), 0, 255);
                    var green = Math.Clamp((int)(color.G * data.light[j, i]), 0, 255);
                    var blue  = Math.Clamp((int)(color.B * data.light[j, i]), 0, 255);

                    if (data.light[j, i] < 0) {
                        red   = color.R;
                        green = color.G;
                        blue  = color.B;
                    }
                    
                    bitmap.SetPixel(i, j, data.Item1[j, i] switch {
                        ' ' => Color.Black,
                        _ => Color.FromArgb(red, green, blue)
                    });
                }
            
            Main.Source =  Imaging.CreateBitmapSourceFromHBitmap(bitmap.GetHbitmap(), nint.Zero,
                Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
        }

        private void CreateObject(object sender, RoutedEventArgs e) {
            var values = Values.Children.Cast<Canvas>().ToList();

            switch (ObjectType.Text) {
                case "Шар":
                    Scene.CreateObject(new Sphere(
                        InterfaceScripts.GetVectorFromValues(values[1]),
                        new Vector3(InterfaceScripts.GetScalarFromValues(values[2])),
                        new Material(InterfaceScripts.GetColorFromValues(values[3])),
                        InterfaceScripts.GetNameFromValues(values[0])
                        ));
                    break;
                case "Куб":
                    Scene.CreateObject(new Cube(
                        InterfaceScripts.GetVectorFromValues(values[1]),
                        InterfaceScripts.GetVectorFromValues(values[2]),
                        new Material(InterfaceScripts.GetColorFromValues(values[3])),
                        InterfaceScripts.GetNameFromValues(values[0])
                    ));
                    break;
                case "Линия":
                    Scene.CreateObject(new Line(
                        InterfaceScripts.GetVectorFromValues(values[1]),
                        InterfaceScripts.GetVectorFromValues(values[2]),
                        InterfaceScripts.GetScalarFromValues(values[3]),
                        new Material(InterfaceScripts.GetColorFromValues(values[4])),
                        InterfaceScripts.GetNameFromValues(values[0])
                    ));
                    break;
                case "Полигон":
                    Scene.CreateObject(new Polygon(
                        new [] {
                            InterfaceScripts.GetVectorFromValues(values[1]),
                            InterfaceScripts.GetVectorFromValues(values[2]),
                            InterfaceScripts.GetVectorFromValues(values[3])
                        },
                        new Material(InterfaceScripts.GetColorFromValues(values[4])),
                        InterfaceScripts.GetNameFromValues(values[0])
                    ));
                    break;
                case "Свет":
                    Scene.CreateObject(new Light(
                        InterfaceScripts.GetVectorFromValues(values[1]),
                        InterfaceScripts.GetScalarFromValues(values[2]),
                        InterfaceScripts.GetNameFromValues(values[0])
                    ));
                    break;
            }

            UpdateMenu();
        }
        
        private void CreatingTypeChanged(object sender, SelectionChangedEventArgs e) {
            var option = ((ComboBoxItem)ObjectType.Items[((ComboBox)sender).SelectedIndex]).Content.ToString();
            Values.Children.Clear();

            Values.Height = InterfaceScripts.InterfaceValues[option!].Count * 60;
            
            for (var i = 0; i < InterfaceScripts.InterfaceValues[option!].Count; i++) 
                Values.Children.Add(InterfaceScripts.InterfaceValues[option!][i].GetBody(i));
        }

        public void DeleteObject(object sender, RoutedEventArgs e) {
            var name = ((Button)sender).Name.Split("_")[1];

            for (var i = 0; i < ObjectsMenu.Children.Count; i++)
                if (((Label)((Canvas)ObjectsMenu.Children[i]).Children[0]).Content.ToString() == name) 
                    ObjectsMenu.Children.RemoveAt(i);
            
            Scene.DeleteObject(name);

            UpdateMenu();
        }

        public void SelectObject(object sender, RoutedEventArgs e) {
            _movingObjectName = ((Button)sender).Name.Split("_")[1];
            foreach (var obj in ObjectsMenu.Children) 
                ((Button)((Canvas)obj).Children[^1]).Content = "Select";

            ((Button)sender).Content = "Disel.";
        }
        
        private void UpdateMenu() {
            ObjectsMenu.Children.Clear();
            foreach (var obj in Scene.Objects) 
                ObjectsMenu.Children.Add(ObjectElement.GetBody(obj.GetName(),
                    ObjectsMenu.Children.Count, this));
            
            ObjectsMenu.Height = ObjectsMenu.Children.Count * 50;
        }

        private void ShowCreation(object sender, RoutedEventArgs e) {
            Creator.Visibility = Visibility.Visible;
            Editor.Visibility = Visibility.Hidden;
        }

        private void ShowEditor(object sender, RoutedEventArgs e) {
            Creator.Visibility = Visibility.Hidden;
            Editor.Visibility = Visibility.Visible;
        }

        private readonly DispatcherTimer _movingTimer = new () {
            Interval = TimeSpan.FromSeconds(.0001)
        };
        
        private void SetPosition(object sender, RoutedEventArgs e) {
            if (_movingTimer.IsEnabled) {
                MessageBox.Show("Дождитесь конца передвижения!");
                return;
            }
            
            var coordinates = new Vector3(double.Parse(MoveX.Text),
                double.Parse(MoveY.Text), double.Parse(MoveZ.Text));
            
            var rotation = new Vector3(double.Parse(RotateX.Text),
                double.Parse(RotateY.Text), double.Parse(RotateZ.Text));

            _moveStep = coordinates / new Vector3(Steps);
            _rotateStep = rotation / new Vector3(Steps);
            
            _movingTimer.Start();
        }

        private string _movingObjectName = "";
        private Vector3 _moveStep = new (0);
        private Vector3 _rotateStep = new (0);
        private const int Steps = 100;
        
        private int _temp = Steps;
        private void Move(object? sender, EventArgs e) {
            _temp--;
            
            Scene.GetObject(_movingObjectName).Move(_moveStep);
            Scene.GetObject(_movingObjectName).Rotate(_rotateStep);

            if (_temp > 0) return;
            _temp = Steps;
            _movingTimer.Stop();
        }
    }
}