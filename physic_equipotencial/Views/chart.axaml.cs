using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace physic_equipotencial.Views
{
    public class Chart : UserControl
    {
        public Chart()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}

