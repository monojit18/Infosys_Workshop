using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using TestFormsApp.ViewModels;

namespace TestFormsApp.Views
{
    public partial class MainPage : ContentPage
    {
        void Handle_Clicked(object sender, EventArgs e)
        {

            var color = ImageControl.DisplayTextColor;
            var newRed = (color.R * 255.0 + 10.0) / 255.0;
            color = new Color(newRed, color.G, color.B);

            ImageControl.DisplayTextColor = color;
            ImageControl.DisplayText = ImageControl.DisplayTextColor.ToString();

        }

        public MainPage()
        {
            InitializeComponent();



            var tm = new MainViewModel();
            BindingContext = tm;

        }
    }
}
