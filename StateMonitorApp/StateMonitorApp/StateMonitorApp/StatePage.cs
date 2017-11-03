using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace StateMonitorApp
{
	public class StatePage : ContentPage
	{
        public Label test;
		public StatePage ()
		{
            this.BackgroundColor = Color.White;
            this.Title = "Текущее состояние";

            test = new Label();
            test.Text = "Test";
            test.VerticalOptions = LayoutOptions.StartAndExpand;
            test.HorizontalOptions = LayoutOptions.CenterAndExpand;
            test.TextColor = Color.Black;

            Content = new StackLayout {Children = { test } };
		}
	}
}