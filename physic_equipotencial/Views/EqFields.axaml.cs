using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Interactivity;
using ReactiveUI;
using System.Reactive;
using System.Windows.Input;
using Avalonia.Data;
using Avalonia.Input;

namespace physic_equipotencial.Views
{
    public class EqFields : UserControl
    {
        public ReactiveCommand<Avalonia.Input.PointerEventArgs, Unit> Pressed { get; set; }
        public static readonly AttachedProperty<ICommand> CommandProperty = AvaloniaProperty.RegisterAttached<EqFields, Interactive, ICommand>(
            "Command", default(ICommand), false, BindingMode.OneTime);
        public static readonly AttachedProperty<object> CommandParameterProperty = AvaloniaProperty.RegisterAttached<EqFields, Interactive, object>(
            "CommandParameter", default(object), false, BindingMode.OneWay, null);
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
                    object commandParameter = interactElem.GetValue(CommandParameterProperty);
                    if (commandValue?.CanExecute(commandParameter) == true)
                    {
                        commandValue.Execute(commandParameter);
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
            CommandProperty.Changed.Subscribe(x => HandleCommandChanged(x.Sender, x.NewValue.GetValueOrDefault<ICommand>()));
            Pressed = ReactiveCommand.Create<Avalonia.Input.PointerEventArgs>(PressedCallback);
            AvaloniaXamlLoader.Load(this);
        }
        public void PressedCallback(Avalonia.Input.PointerEventArgs d)
        {
            //int i = 0;
            //var i = element.GetValue(CommandProperty);
        }
    }
}