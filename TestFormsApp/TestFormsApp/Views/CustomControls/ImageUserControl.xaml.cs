using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace TestFormsApp.Views.CustomControls
{
    public partial class ImageUserControl : ContentView
    {

        private static void OnTextPropertyChanged(BindableObject bindable, object oldValue,
                                                  object newValue)
        {

            var imageUserControl = (ImageUserControl)bindable;
            imageUserControl.DisplayLabel.Text = newValue.ToString();

        }

        private static void OnTextColorPropertyChanged(BindableObject bindable, object oldValue,
                                                  object newValue)
        {

            var imageUserControl = (ImageUserControl)bindable;
            imageUserControl.DisplayLabel.TextColor = (Color)newValue;

        }


        public static readonly BindableProperty DisplayTextProperty =
            BindableProperty.Create("DisplayText", typeof(string),
                                    typeof(ImageUserControl),
                                    propertyChanged: OnTextPropertyChanged);

        public static readonly BindableProperty DisplayTextColorProperty =
            BindableProperty.Create("DisplayTextColor", typeof(Color),
                                    typeof(ImageUserControl),
                                    defaultValue:Color.Blue,
                                    propertyChanged: OnTextColorPropertyChanged);
        
        public string DisplayText
        {

            get
            {

                return GetValue(DisplayTextProperty) as string;

            }

            set
            {

                SetValue(DisplayTextProperty, value);

            }


        }

        public Color DisplayTextColor
        {

            get
            {

                return (Color)GetValue(DisplayTextColorProperty);

            }

            set
            {

                SetValue(DisplayTextColorProperty, value);

            }


        }






        public ImageUserControl()
        {
            InitializeComponent();
        }
    }
}
