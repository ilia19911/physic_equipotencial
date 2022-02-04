using System;
using System.Collections.Generic;
using System.Text;
using ReactiveUI;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace physic_equipotencial.ViewModels
{
    public class ViewModelBase : ReactiveObject, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
