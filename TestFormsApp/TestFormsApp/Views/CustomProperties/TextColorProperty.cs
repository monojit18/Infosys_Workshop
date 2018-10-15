using System;
using Xamarin.Forms;
using TestFormsApp.Views.CustomControls;

namespace TestFormsApp.Views.CustomControls.CustomProperties
{
    public class TextColorProperty
    {

        private static void OnTextColorChanged(BindableObject bindable, object oldValue,
                                                  object newValue)
        {

            var imageUserControl = (ImageUserControl)bindable;
            imageUserControl.DisplayTextColor = (Color)newValue;

        }


        public static readonly BindableProperty DisplayTextColorProperty =
            BindableProperty.CreateAttached("DisplayTextColor", typeof(Color),
                                            typeof(TextColorProperty), Color.Blue,
                                            propertyChanged:OnTextColorChanged);

        public static Color GetDisplayTextColor(BindableObject view)
        {

            return ((Color)(view.GetValue(DisplayTextColorProperty)));

        }

        public static void SetDisplayTextColor(BindableObject view, bool value)
        {

            view.SetValue(DisplayTextColorProperty, value);

        }

        public Color DisplayTextColor { get; set; }


        public TextColorProperty()
        {



        }
    }
}
