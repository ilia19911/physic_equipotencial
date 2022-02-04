using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Interactivity;
using ReactiveUI;
using System.Reactive;
using System.Reactive.Linq;
using System.Windows.Input;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data;
using Avalonia.Input;
using Avalonia.VisualTree;
using LiveChartsCore.Drawing;
using LiveChartsCore.Kernel;
using LiveChartsCore.Kernel.Events;
using LiveChartsCore.Kernel.Sketches;
using LiveChartsCore.Measure;
using LiveChartsCore.SkiaSharpView.Drawing;

namespace physic_equipotencial.Views
{
    public class EqFields : UserControl
    {
        private IEnumerable<ChartPoint> MyCharts;
        public ReactiveCommand<Avalonia.Input.PointerEventArgs, Unit> Pressed { get; set; }
        private ICommand command;
        public ReactiveCommand<object, Unit> DoTheThing { get; set; }
        public static readonly AttachedProperty<ICommand> CommandProperty = AvaloniaProperty.RegisterAttached<EqFields, Interactive, ICommand>(
            "Command", default(ICommand), false, BindingMode.OneTime);
        public static void SetCommand(AvaloniaObject element, ICommand commandValue)
        {
            element.SetValue(CommandProperty, commandValue);
        }
        public static ICommand GetCommand(AvaloniaObject element)
        {
            return element.GetValue(CommandProperty);
        }
        private static void HandleCommandChanged(IAvaloniaObject element, ICommand commandValue)
        {
            Interactive interactElem = element as Interactive;
            if (interactElem != null )
            {
                Action<object, RoutedEventArgs> Handler = (object s, RoutedEventArgs e) =>
                {
                    Avalonia.Input.PointerEventArgs LastPoint = (e as Avalonia.Input.PointerEventArgs);
                        // object commandParameter = interactElem.GetValue(CommandParameterProperty);
                    if (commandValue?.CanExecute(e) == true)
                    {
                        commandValue.Execute(e);
                    }
                };
                if (commandValue != null)
                {
                    // Add non-null value
                    interactElem.AddHandler(InputElement.PointerPressedEvent, Handler);
                }
                else
                {
                    // remove prev value
                    interactElem.RemoveHandler(InputElement.PointerPressedEvent, Handler);
                }
            }


        }
        public EqFields()
        {
            InitializeComponent();
        }
        private void InitializeComponent()
        {
            DoTheThing = ReactiveCommand.Create<object>(RunTheThing);
            Pressed = ReactiveCommand.Create<Avalonia.Input.PointerEventArgs>(PressedCallback);
            //CommandProperty.Changed.Subscribe(x => HandleCommandChanged(x.Sender, x.NewValue.GetValueOrDefault<ICommand>()));
            CommandProperty.Changed.Subscribe(x =>
            {
                command = x.NewValue.GetValueOrDefault<ICommand>();
            });
            AvaloniaXamlLoader.Load(this);
        }
        public void PressedCallback(Avalonia.Input.PointerEventArgs d)
        {
            PointerEventArgs LastPoint = d;
            Point pos = new Point(0,0);
            if (Avalonia.Application.Current.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                pos = d.GetPosition(desktop.MainWindow);
            }
            
            foreach (ChartPoint i in MyCharts)
            {
                try
                {
                    var  pressed = i.Context.HoverArea.IsPointerOver(new LvcPoint((float)pos.X, (float)pos.Y), TooltipFindingStrategy.CompareAll);
                    if(pressed)
                    {
                        if (command.CanExecute(i))
                        {
                            command.Execute(i);
                        }
                    }
                }
                catch (Exception e)
                {
                    
                }
            }
        }
        void RunTheThing(object parameter)
        {
            MyCharts = (IEnumerable<ChartPoint>)parameter ;
        }
    }
}