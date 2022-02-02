using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Reactive;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Interactivity;
using LiveChartsCore.SkiaSharpView.Avalonia;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using ReactiveUI;
using SkiaSharp;

namespace physic_equipotencial.ViewModels
{
    public class MainWindowViewModel : ViewModelBase 
    {
        public ReactiveCommand<object, Unit> Pressed { get; }
        public ISeries[] Series { get; set; }
            = {
                new LineSeries<double> {  GeometrySize = 0, LineSmoothness = 0, Values = new double[] { 0, 0, 0, 0, 0, 5, 0, 0, 0, 0 }, Fill = null, GeometryFill = new SolidColorPaint { Color = SKColors.DarkOliveGreen }, Stroke =  new SolidColorPaint { Color = SKColors.DarkOliveGreen } },
                new LineSeries<double> {  Values = new double[] { 1, 1, 1, 1, 2, 6, 2, 1, 1, 1 }, Fill = null },
                new LineSeries<double> {GeometrySize = 0,  LineSmoothness = 0, Values = new double[] { 10, 10, 10, 10, 10, 10, 10, 10, 10, 10 }, Fill = null , GeometryFill = new SolidColorPaint { Color = SKColors.DarkOliveGreen }, Stroke =  new SolidColorPaint { Color = SKColors.DarkOliveGreen } },
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
            Pressed = ReactiveCommand.Create<object>(PressedCallback);
        }
        public void PressedCallback(object d)
        {
            int i = 0;
        }
        public string Greeting => "Welcome to Avalonia!";
    }
}
