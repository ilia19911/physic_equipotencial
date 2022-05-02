using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Linq;
using System.Reactive;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Input;
using Avalonia.Interactivity;
using LiveChartsCore.SkiaSharpView.Avalonia;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore;
using LiveChartsCore.Drawing;
using LiveChartsCore.Kernel;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Drawing;
using LiveChartsCore.SkiaSharpView.Drawing.Geometries;
using LiveChartsCore.SkiaSharpView.Drawing.Geometries.Segments;
using LiveChartsCore.SkiaSharpView.Painting;
using ReactiveUI;
using System.Reactive.Subjects;
using SkiaSharp;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Reactive.Linq;
using System.Text.RegularExpressions;
using LiveChartsCore.Defaults;
using physic_equipotencial.Models;

using System;
using System.Reactive.Linq;
using System.Reactive;
using System.IO;
using Avalonia.Controls.Shapes;
using Avalonia.Media;


namespace physic_equipotencial.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        
        public MainWindowViewModel()
        {
            Pressed = ReactiveCommand.Create<Point>(PressedCallback);
            //ChangeLine = ReactiveCommand.Create<RoutedEventArgs>(ChangeLineCallback);
            Series  = new ObservableCollection<ISeries>()
            {
                new LineSeries<ObservablePoint>
                {
                    Name = "l_edge",
                    Pivot = 0,
                    LegendShapeSize = 0,
                    GeometrySize = 0,
                    LineSmoothness = 0,
                    Values = createEdgeline(x_start, x_stop, 1, 0, new (double, double)[] {new(-1, 6.25),new(0, 12.5),new(1, 6.25),}),
                    Fill = new SolidColorPaint {Color = SKColors.SkyBlue},
                    GeometryFill = new SolidColorPaint {Color = SKColors.SkyBlue},
                    GeometryStroke = new SolidColorPaint {Color = SKColors.SkyBlue},
                    Stroke = new SolidColorPaint {Color = SKColors.SkyBlue, StrokeThickness = 6}
                },
                new LineSeries<ObservablePoint>
                {
                    Name = "h_edge",
                    Pivot = 0,
                    LegendShapeSize = 0,
                    GeometrySize = 0,
                    LineSmoothness = 0,
                    Values = createEdgeline(x_start, x_stop, 1, 15),
                    Fill = null,
                    GeometryFill = new SolidColorPaint {Color = SKColors.SkyBlue},
                    GeometryStroke = new SolidColorPaint {Color = SKColors.SkyBlue},
                    Stroke = new SolidColorPaint {Color = SKColors.SkyBlue, StrokeThickness = 6}
                },
            };
            for (double i = -10.0f; i <= 10; i += 2.0)
            {
                Series.Add(new LineSeries<ObservablePoint>
                {
                    Name = "Line" + i,
                    Pivot = 0,
                    LegendShapeSize = 0,
                    GeometrySize = 0,
                    LineSmoothness = 0,
                    Values = new  ObservableCollection<ObservablePoint>(){new ObservablePoint(i,0),new ObservablePoint(i,15)  },
                    Fill = new SolidColorPaint {Color = SKColors.Red},
                    GeometryFill = new SolidColorPaint {Color = SKColors.Red},
                    GeometryStroke = new SolidColorPaint {Color = SKColors.Red},
                    Stroke = new SolidColorPaint {Color = SKColors.SkyBlue, StrokeThickness = 1}
                });
                if (i > -5 && i < 4)
                {
                    i -= 1.0;
                }
            }

            for (double i = 2.5; i < 15; i += 2.5)
            {
                var hEdge = Series.Single(x => x.Name == "h_edge").Values;
                var lEdge = Series.Single(x => x.Name == "l_edge").Values;
                if (hEdge != null && lEdge != null)
                {
                   // var series = Potencial.GetSeries(i,0.01, (ObservableCollection<ObservablePoint>)hEdge,(ObservableCollection<ObservablePoint>)lEdge);
                   var series = GetSeries(i);
                    
                    Series.Add(getSerial(1, "field "+ i, SKColors.Red, series));
                    //Series.Add(getSerial(series.Count(), "field1_hide", SKColors.Transparent, series));
                }
                
            }

            UpdatePoint("2.5");
        }
        

        private double selected_serial = 2.5f;
        ObservableCollection<ObservablePoint> GetSeries(double val)
        {
            ObservableCollection<ObservablePoint> result = new ObservableCollection<ObservablePoint>();
            switch (@val)
            {
                case 2.5f:
                    result.Add((new(-10, 2.5)));
                    result.Add((new(-8, 2.55)));
                    result.Add((new(-6, 2.8)));
                    result.Add((new(-4, 4)));
                    result.Add((new(-3, 5.30)));
                    result.Add((new(-2, 8)));
                    result.Add((new(-1, 10.8)));
                    result.Add((new( 0, 12.8)));
                    result.Add((new( 1, 10.8)));
                    result.Add((new( 2, 8)));
                    result.Add((new( 3, 5.3)));
                    result.Add((new( 4, 4)));
                    result.Add((new( 6, 2.8)));
                    result.Add((new( 8, 2.55)));
                    result.Add((new( 10, 2.5)));
                    break;
                case 5.0f:
                    result.Add((new(-10, 5)));
                    result.Add((new(-8, 5.2)));
                    result.Add((new(-6, 5.8)));
                    result.Add((new(-4, 7.3)));
                    result.Add((new(-3, 8.7)));
                    result.Add((new(-2, 10.5)));
                    result.Add((new(-1, 12.1)));
                    result.Add((new( 0, 13.2)));
                    result.Add((new( 1, 12.1)));
                    result.Add((new( 2, 10.5)));
                    result.Add((new( 3, 8.7)));
                    result.Add((new( 4, 7.3)));
                    result.Add((new( 6, 5.8)));
                    result.Add((new( 8, 5.2)));
                    result.Add((new( 10, 5)));
                    break;
                case 7.5f:
                    result.Add((new(-10, 7.5)));
                    result.Add((new(-8, 7.7)));
                    result.Add((new(-6, 8.4)));
                    result.Add((new(-4, 10.0)));
                    result.Add((new(-3, 11.0)));
                    result.Add((new(-2, 12.0)));
                    result.Add((new(-1, 13.0)));
                    result.Add((new( 0, 13.6)));
                    result.Add((new( 1, 13.0)));
                    result.Add((new( 2, 12.0)));
                    result.Add((new( 3, 11.0)));
                    result.Add((new( 4, 10.0)));
                    result.Add((new( 6, 8.4)));
                    result.Add((new( 8, 7.7)));
                    result.Add((new( 10, 7.5)));
                    break;
                case 10.0f:
                    result.Add((new(-10, 10)));
                    result.Add((new(-8, 10.2)));
                    result.Add((new(-6, 10.6)));
                    result.Add((new(-4, 11.5)));
                    result.Add((new(-3, 12.1)));
                    result.Add((new(-2, 12.9)));
                    result.Add((new(-1, 13.6)));
                    result.Add((new( 0, 14.0)));
                    result.Add((new( 1, 13.6)));
                    result.Add((new( 2, 12.9)));
                    result.Add((new( 3, 12.1)));
                    result.Add((new( 4, 11.5)));
                    result.Add((new( 6, 10.6)));
                    result.Add((new( 8, 10.2)));
                    result.Add((new( 10, 10)));
                    break;
                case 12.5f:
                    result.Add((new(-10, 12.5)));
                    result.Add((new(-8, 12.7)));
                    result.Add((new(-6, 12.9)));
                    result.Add((new(-4, 13.3)));
                    result.Add((new(-3, 13.5)));
                    result.Add((new(-2, 13.8)));
                    result.Add((new(-1, 14.2)));
                    result.Add((new( 0, 14.5)));
                    result.Add((new( 1, 14.2)));
                    result.Add((new( 2, 13.8)));
                    result.Add((new( 3, 13.5)));
                    result.Add((new( 4, 13.3)));
                    result.Add((new( 6, 12.9)));
                    result.Add((new( 8, 12.7)));
                    result.Add((new( 10, 12.5)));
                    break;
                
            }

            return result;
        }
        private static readonly double x_start = -10;
        private static readonly double x_stop = 10;
        public ReactiveCommand<Point, Unit> Pressed { get; }
        //public ReactiveCommand<RoutedEventArgs, Unit> ChangeLine { get; }
        
        
        private ObservableCollection<ObservablePoint> createEdgeline(double start, double stop, double step, double default_value, (double, double)[] points = null)
        {
            ObservableCollection<ObservablePoint> result = new ObservableCollection<ObservablePoint>();
            int steps = (int) ((stop - start) / step);
            for (int i = 0; i <= steps; i++)
            {
                double x_value = start + step * i;
                if (points != null && points.Length > 0)
                {
                    var point = Array.Find(points, x => x.Item1 == x_value);
                    if (point != default((double, double)))
                    {
                        result.Add((new(x_value, point.Item2)));
                        continue;
                    }
                }

                result.Add((new(x_value, default_value)));
            }

            return result;
        }

        
        private LineSeries<ObservablePoint> getSerial(int length, string name, SKColor color, ObservableCollection<ObservablePoint> values)
        {
            var array = values.Take(length);
            LineSeries<ObservablePoint> result = new LineSeries<ObservablePoint>()
            {
                Name = name,
                Pivot = 0,
                LineSmoothness = 0.9,
                LegendShapeSize = 0,
                DataLabelsSize = 10,
                GeometrySize = 10,
                
                GeometryStroke = new SolidColorPaint {Color = color}, //SKColor.Empty
                Fill = null,
                GeometryFill = new RadialGradientPaint(color, color),
                //GeometryFill = new RadialGradientPaint(SKColor.Empty, SKColor.Empty),
                Stroke = new SolidColorPaint {Color = color, StrokeThickness = 4},
                Values = array
            };
            return result;
        }

        public ObservableCollection<ISeries> Series { get; set; } 

        public List<Axis> XAxes { get; set; } = new List<Axis>
        {
            new Axis
            {
                MinStep = 1,
                MinLimit = -10,
                MaxLimit = 10,
                Name = "См",
                NamePaint = new SolidColorPaint {Color = SKColors.Red},
                // Use the labels property for named or static labels // mark
                //UnitWidth = 0.1,
                //Labels = new string[] { "0", "0.1", "0.2" }, // mark
                LabelsRotation = 0
            }
        };

        public List<Axis> YAxes { get; set; } = new List<Axis>
        {
            new Axis
            {
                MinStep = 2.5,
                MinLimit = -1,
                MaxLimit = 16,
                Name = "x10В",
                NamePaint = new SolidColorPaint {Color = SKColors.Red},
                // Use the labels property for named or static labels // mark
                //UnitWidth = 0.1,
                //Labels = new string[] { "0", "0.1", "0.2" }, // mark
                LabelsRotation = 0
            }
        };

        public void PressedCallback(Point arg)
        {
            var series = Series.Single(n => n.Name == "field " + selected_serial);
            var line = GetSeries(selected_serial);
            foreach (var i in line)
            {
                if (Math.Abs((double)(arg.X - i.X)) < 0.5 && Math.Abs((double)(arg.Y - i.Y)) < 0.5)
                {
                    int index = line.IndexOf(i);
                    series.Values = GetSeries(selected_serial).Take(index+1);
                }
            }

            UpdatePoint(selected_serial.ToString("0.0"));
            // if (arg.AsTooltipString.Contains("field1_hide"))
            // {
            //     if (Series[2].Values is IEnumerable<ObservablePoint> serialArray && serialArray.Count() > 0)
            //     {
            //         if (arg.SecondaryValue > serialArray.Count() - 1)
            //         {
            //             int i = serialArray.Count() + 1; // Series[2].Values.
            //             //Series[2] = getSerial(i);
            //             OnPropertyChanged("Series");
            //         }
            //     }
            //     else
            //     {
            //         //Series[2] = getSerial(1);
            //     }
            // }
        }

        public void SelectedLine(RoutedEventArgs arg)
        {
            UpdatePoint(((Ellipse) arg.Source).Name);
        }

        void UpdatePoint(string name)
        {
                        int have_to_be_red = 99;
                        if (name.Contains("12.5"))
                        {
                            selected_serial = 12.5;
                            var s = Series.Single(x => x.Name == "field 12.5");
                            IEnumerable<ObservablePoint> i = s.Values as IEnumerable<ObservablePoint>;
                            have_to_be_red = (int)i.Last().X + 1;
                        }
            else if (name.Contains("2.5"))
            {
                selected_serial = 2.5;
                var s = Series.Single(x => x.Name == "field 2.5");
                IEnumerable<ObservablePoint> i = s.Values as IEnumerable<ObservablePoint>;
                have_to_be_red = (int)i.Last().X + 1;


            }
            else if (name.Contains("5.0"))
            {
                selected_serial = 5.0;
                var s = Series.Single(x => x.Name == "field 5");
                IEnumerable<ObservablePoint> i = s.Values as IEnumerable<ObservablePoint>;
                have_to_be_red = (int)i.Last().X + 1;
            }
            else if (name.Contains("7.5"))
            {
                selected_serial = 7.5;
                var s = Series.Single(x => x.Name == "field 7.5");
                IEnumerable<ObservablePoint> i = s.Values as IEnumerable<ObservablePoint>;
                have_to_be_red = (int)i.Last().X + 1;
            }
            else if (name.Contains("10.0"))
            {
                selected_serial = 10.0;
                var s = Series.Single(x => x.Name == "field 10");
                IEnumerable<ObservablePoint> i = s.Values as IEnumerable<ObservablePoint>;
                have_to_be_red = (int)i.Last().X + 1;
            }


            if (have_to_be_red > -10 && have_to_be_red < 10)
            {
                if (have_to_be_red == -9) have_to_be_red = -8;
                else if (have_to_be_red == -7) have_to_be_red = -6;
                else if (have_to_be_red == -5) have_to_be_red = -4;
                else if (have_to_be_red == 5) have_to_be_red = 6;
                else if (have_to_be_red == 7) have_to_be_red = 8;
                else if (have_to_be_red == 9) have_to_be_red = 10;
                foreach (var s in Series)
                {
                    if (((LineSeries<ObservablePoint>) s).Name.Contains("Line"))
                    {
                        ((SolidColorPaint) (((LineSeries<ObservablePoint>) s).Stroke)).Color = SKColors.SkyBlue;
                        ((SolidColorPaint) (((LineSeries<ObservablePoint>) s).Stroke)).StrokeThickness = 1;   
                    }
                }
                var s2 = Series.Single(x => x.Name == "Line"+have_to_be_red);
                ((SolidColorPaint) (((LineSeries<ObservablePoint>) s2).Stroke)).Color = SKColors.Red;
                ((SolidColorPaint) (((LineSeries<ObservablePoint>) s2).Stroke)).StrokeThickness = 3;
            }
            OnPropertyChanged("Series");
            OnPropertyChanged("GetColor2_5");
            OnPropertyChanged("GetColor5_0");
            OnPropertyChanged("GetColor7_5");
            OnPropertyChanged("GetColor10_0");
            OnPropertyChanged("GetColor12_5");
        }
        public Brush GetColor2_5
        {
            get
            {
                if (selected_serial == 2.5)
                {
                    return (SolidColorBrush) new BrushConverter().ConvertFromString("#FF6347");   
                }
                else
                {
                    return (SolidColorBrush) new BrushConverter().ConvertFromString("#87CEEB");
                }
            }
        }
        public Brush GetColor5_0
        {
            get
            {
                if (selected_serial == 5.0)
                {
                    return (SolidColorBrush) new BrushConverter().ConvertFromString("#FF6347");   
                }
                else
                {
                    return (SolidColorBrush) new BrushConverter().ConvertFromString("#87CEEB");
                }
            }
        }
        public Brush GetColor7_5
        {
            get
            {
                if (selected_serial == 7.5)
                {
                    return (SolidColorBrush) new BrushConverter().ConvertFromString("#FF6347");   
                }
                else
                {
                    return (SolidColorBrush) new BrushConverter().ConvertFromString("#87CEEB");
                }
            }
        }
        public Brush GetColor10_0
        {
            get
            {
                if (selected_serial == 10.0)
                {
                    return (SolidColorBrush) new BrushConverter().ConvertFromString("#FF6347");   
                }
                else
                {
                    return (SolidColorBrush) new BrushConverter().ConvertFromString("#87CEEB");
                }
            }
        }
        public Brush GetColor12_5
        {
            get
            {
                if (selected_serial == 12.5)
                {
                    return (SolidColorBrush) new BrushConverter().ConvertFromString("#FF6347");   
                }
                else
                {
                    return (SolidColorBrush) new BrushConverter().ConvertFromString("#87CEEB");
                }
            }
            
        }
    }
}