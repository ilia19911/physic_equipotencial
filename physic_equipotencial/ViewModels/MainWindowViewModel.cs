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
using SkiaSharp;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace physic_equipotencial.ViewModels
{
    public class MainWindowViewModel : ViewModelBase, INotifyPropertyChanged
    {
        public ReactiveCommand< ChartPoint , Unit> Pressed { get; }
        private static double[] eqfield = {1, 1, 1, 1, 2, 6, 2, 1, 1, 1};

        private static LineSeries<double> getSerial(int length)
        {
            var array = eqfield.Take(length);
            LineSeries<double> result = new LineSeries<double>(){
                Name = "field1", Pivot = 1, LegendShapeSize = 0, DataLabelsSize = 10, GeometrySize = 20,
                GeometryStroke = new SolidColorPaint {Color = SKColor.Empty}, Fill = null,
                GeometryFill = new RadialGradientPaint(SKColor.Empty, SKColor.Empty), Stroke = new SolidColorPaint {Color = SKColors.Red, StrokeThickness = 6},
                Values =array 
            };
            return result;
        }

        public ObservableCollection<ISeries>  Series { get; set; } = new ObservableCollection<ISeries>()
            {
                new LineSeries<double> { Pivot = 0,  LegendShapeSize=0, GeometrySize = 0,  LineSmoothness = 0, Values = new double[] { 0, 0, 0, 0, 0, 5, 0, 0, 0, 0 }, Fill = null, GeometryFill = new SolidColorPaint { Color = SKColors.DarkOliveGreen }, Stroke =  new SolidColorPaint { Color = SKColors.DarkOliveGreen } },
                new LineSeries<double> { Pivot = 0,  LegendShapeSize=0, GeometrySize = 0,  LineSmoothness = 0, Values = new double[] { 10, 10, 10, 10, 10, 10, 10, 10, 10, 10 }, Fill = null , GeometryFill = new SolidColorPaint { Color = SKColors.DarkOliveGreen }, Stroke =  new SolidColorPaint { Color = SKColors.DarkOliveGreen } },
                
                getSerial(2),
                new LineSeries<double> {Name  = "field1_hide",   Pivot = 1,  LegendShapeSize  = 0, DataLabelsSize  = 10,GeometrySize = 20, 
                    GeometryStroke  =  new SolidColorPaint { Color = SKColors.Transparent }, Fill = null, GeometryFill = new RadialGradientPaint(SKColors.Transparent, SKColors.Transparent), 
                    Stroke =  new SolidColorPaint { Color = SKColors.Transparent },
                    Values = (double[])eqfield.Clone() },
            };

        public List<Axis> XAxes { get; set; }= new List<Axis>
        {
            new Axis
            {
                
                Name = "field",
                NamePaint = new SolidColorPaint { Color = SKColors.Red },
                // Use the labels property for named or static labels // mark
                UnitWidth = 0.1,
                //Labels = new string[] { "0", "0.1", "0.2" }, // mark
                LabelsRotation = 15
            }
        };


        public MainWindowViewModel()
        {
            Pressed = ReactiveCommand.Create< ChartPoint >(PressedCallback);
        }
        public void PressedCallback( ChartPoint  arg)
        {
            if (arg.AsTooltipString.Contains("field1_hide"))
            {
                
                if (Series[2].Values is IEnumerable<double> serialArray && serialArray.Count()>0)
                {
                    if (arg.SecondaryValue > serialArray.Count() - 1 )
                    {
                        int i = serialArray.Count() + 1;// Series[2].Values.
                        Series[2] = getSerial(i);
                        OnPropertyChanged("Series");    
                    }   
                }
                else
                {
                    Series[2] = getSerial(1);
                }
            }
        }
        public string Greeting => "Welcome to Avalonia!";
    }
}
