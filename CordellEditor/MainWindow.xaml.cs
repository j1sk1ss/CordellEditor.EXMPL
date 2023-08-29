using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
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

using Microsoft.Win32;

using Newtonsoft.Json;

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
        private Space Scene { get; set; }

        private void Visualisation(object? sender, EventArgs e) {
            var data = Scene.GetView();
            var bitmap = new Bitmap(200, 113);

            Parallel.For(0, data.Item1.GetLength(1), i => {
                Parallel.For(0, data.Item1.GetLength(0), j => {
                    var color = ColorConvertor.Colors[data.color[j, i]];
                    
                    var red   = Math.Clamp((int)(color.R * data.light[j, i]), 0, 255);
                    var green = Math.Clamp((int)(color.G * data.light[j, i]), 0, 255);
                    var blue  = Math.Clamp((int)(color.B * data.light[j, i]), 0, 255);

                    if (data.light[j, i] < 0) {
                        red   = color.R;
                        green = color.G;
                        blue  = color.B;
                    }
                    
                    lock (bitmap) bitmap.SetPixel(i, j, data.Item1[j, i] switch {
                        ' ' => Color.Black,
                        _ => Color.FromArgb(red, green, blue)
                    });
                });
            });
            
            Main.Source =  Imaging.CreateBitmapSourceFromHBitmap(bitmap.GetHbitmap(), nint.Zero,
                Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
        }

        private void CreateObject(object sender, RoutedEventArgs e) {
            var values = Values.Children.Cast<Canvas>().ToList();

            switch (ObjectType.Text) {
                case "Sphere":
                    Scene.CreateObject(new Sphere(
                        VectorValueElement.GetVectorFromValues(values[1]),
                        new Vector3(ScalarValueElement.GetScalarFromValues(values[2])),
                        new Material(ColorValueElement.GetColorFromValues(values[3]),
                            ColorValueElement.GetReflectionFromValues(values[3])),
                        NameValueElement.GetNameFromValues(values[0])
                        ));
                    break;
                case "Cube":
                    Scene.CreateObject(new Cube(
                        VectorValueElement.GetVectorFromValues(values[1]),
                        VectorValueElement.GetVectorFromValues(values[2]),
                        new Material(ColorValueElement.GetColorFromValues(values[3]),
                            ColorValueElement.GetReflectionFromValues(values[3])),
                        NameValueElement.GetNameFromValues(values[0])
                    ));
                    break;
                case "Line":
                    Scene.CreateObject(new Line(
                        VectorValueElement.GetVectorFromValues(values[1]),
                        VectorValueElement.GetVectorFromValues(values[2]),
                        ScalarValueElement.GetScalarFromValues(values[3]),
                        new Material(ColorValueElement.GetColorFromValues(values[4]),
                            ColorValueElement.GetReflectionFromValues(values[4])),
                        NameValueElement.GetNameFromValues(values[0])
                    ));
                    break;
                case "Polygon":
                    Scene.CreateObject(new Polygon(
                        new [] {
                            VectorValueElement.GetVectorFromValues(values[1]),
                            VectorValueElement.GetVectorFromValues(values[2]),
                            VectorValueElement.GetVectorFromValues(values[3])
                        },
                        new Material(ColorValueElement.GetColorFromValues(values[4]),
                            ColorValueElement.GetReflectionFromValues(values[4])),
                        NameValueElement.GetNameFromValues(values[0])
                    ));
                    break;
                case "Light":
                    Scene.CreateObject(new Light(
                        VectorValueElement.GetVectorFromValues(values[1]),
                        ScalarValueElement.GetScalarFromValues(values[2]),
                        NameValueElement.GetNameFromValues(values[0])
                    ));
                    break;
                case "Collection":
                    var objects = (from object? obj in ObjectsMenu.Children where ReferenceEquals(((Button)((Canvas)obj).Children[^1]).Content, "Disel.") 
                        select ((Label)((Canvas)obj).Children[0]).Content.ToString()!).ToList();
                    var collectionObjects = Scene.Objects.Where(obj => objects.Contains(obj.GetName())).ToList();

                    foreach (var obj in collectionObjects) 
                        DeleteObject(new Button {
                            Name = $"button_{obj.GetName()}"
                        }, null!);
                    
                    Scene.CreateObject(new Collection(new Vector3(0), new Vector3(0), collectionObjects,
                        Material.DefaultMaterial, NameValueElement.GetNameFromValues(values[0])));
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

            foreach (var row in _corrRows) 
                Scene.DeleteObject(row);  
            
            for (var i = 0; i < ObjectsMenu.Children.Count; i++)
                if (((Label)((Canvas)ObjectsMenu.Children[i]).Children[0]).Content.ToString() == name) 
                    ObjectsMenu.Children.RemoveAt(i);
            
            Scene.DeleteObject(name);

            UpdateMenu();
        }

        public void SelectObject(object sender, RoutedEventArgs e) {
            _selectedObjectName = ((Button)sender).Name.Split("_")[1];
                foreach (var row in _corrRows) 
                    Scene.DeleteObject(row);

            if (ReferenceEquals(((Button)sender).Content, "Disel.")) {
                ((Button)sender).Content = "Select";
                return;
            }
            
            if (!Keyboard.IsKeyDown(Key.LeftShift) && !Keyboard.IsKeyDown(Key.RightShift))
                foreach (var obj in ObjectsMenu.Children) 
                    ((Button)((Canvas)obj).Children[^1]).Content = "Select";
            
            ((Button)sender).Content = "Disel.";
            
            var clickedObject = Scene.GetObject(_selectedObjectName);
            Scene.CreateObject(new Line(clickedObject.GetPosition(), clickedObject.GetPosition() + new Vector3(2,0,0), .05, new Material(ConsoleColor.Red), _corrRows[0]));
            Scene.CreateObject(new Line(clickedObject.GetPosition(), clickedObject.GetPosition() + new Vector3(0,-2,0), .05, new Material(ConsoleColor.Green), _corrRows[1]));
            Scene.CreateObject(new Line(clickedObject.GetPosition(), clickedObject.GetPosition() + new Vector3(0,0,2), .05, new Material(ConsoleColor.Blue), _corrRows[2]));
        }
        
        private void UpdateMenu() {
            var rowsCount = 0;
            
            ObjectsMenu.Children.Clear();
            foreach (var obj in Scene.Objects) {
                if (_corrRows.Contains(obj.GetName())) {
                    rowsCount++;
                    continue;
                }
                
                ObjectsMenu.Children.Add(ObjectElement.GetBody(obj.GetName(),
                    ObjectsMenu.Children.Count, this));
            }

            ObjectsMenu.Height = (ObjectsMenu.Children.Count - rowsCount) * 50;
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
                MessageBox.Show("Wait for transformation end!");
                return;
            }
            
            foreach (var row in _corrRows) 
                Scene.DeleteObject(row);  
            
            var coordinates = new Vector3(double.Parse(MoveX.Text),
                double.Parse(MoveY.Text), double.Parse(MoveZ.Text));
            
            var rotation = new Vector3(double.Parse(RotateX.Text),
                double.Parse(RotateY.Text), double.Parse(RotateZ.Text));

            var steps = 100 - (int)SpeedSlider.Value;
            _moveStep = coordinates / new Vector3(steps);
            _rotateStep = rotation / new Vector3(steps);
            
            _temp = steps;
            _movingTimer.Start();
        }

        private string _selectedObjectName = "";
        private Vector3 _moveStep = new (0);
        private Vector3 _rotateStep = new (0);
        
        private int _temp;
        private void Move(object? sender, EventArgs e) {
            _temp--;
            
            Scene.GetObject(_selectedObjectName).Move(_moveStep);
            Scene.GetObject(_selectedObjectName).Rotate(_rotateStep);

            if (_temp > 0) return;
            _temp = 100 - (int)SpeedSlider.Value;
            _movingTimer.Stop();
        }

        private void NewScene(object sender, RoutedEventArgs e) {
            Scene.Objects.Clear();
            UpdateMenu();
        }

        private void SaveScene(object sender, RoutedEventArgs e) {
            try {
                var saveWindow = new SaveFileDialog {
                    Filter = "json files (*.txt)|*.json|All files (*.*)|*.*"
                };
                
                if (saveWindow.ShowDialog() != true) return;
                File.WriteAllText(saveWindow.SafeFileName, JsonConvert.SerializeObject(Scene));
            }
            catch (Exception) {
                MessageBox.Show("Error with saving");
            }
        }

        private void LoadScene(object sender, RoutedEventArgs e) {
            try {
                var saveWindow = new OpenFileDialog {
                    Filter = "json files (*.txt)|*.json|All files (*.*)|*.*"
                };
                
                if (saveWindow.ShowDialog() != true) return;
                Scene = JsonConvert.DeserializeObject<Space>(File.ReadAllText(saveWindow.FileName))!;
            }
            catch (Exception) {
                MessageBox.Show("Error with saving");
            }
        }

        private readonly List<string> _corrRows = new() {
            "corrRow1", "corrRow2", "corrRow3"
        };
    }
}