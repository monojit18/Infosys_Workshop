using System;
using System.ComponentModel;

namespace TestFormsApp.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {

        private void OnPropertyChanged(string propertyNameString)
        {

            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyNameString));


        }

        public event PropertyChangedEventHandler PropertyChanged;
        public string ModelText { get; set; }

        public MainViewModel()
        {

            ModelText = "This is Sample Text from MainViewModel";

        }
    }
}
