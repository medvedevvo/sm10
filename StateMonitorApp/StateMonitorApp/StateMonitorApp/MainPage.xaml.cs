using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StateMonitorApp
{
    public partial class MainPage : ContentPage
    {
        public Label lblTest;
        public Button btnAbout;
        public Button btnCurrentState;
        private int press_cnt = 0;

        public MainPage()
        {
            InitializeComponent();

            lblTest = this.FindByName<Label>("_lblTest");
            btnAbout = this.FindByName<Button>("_btnAbout");
            btnCurrentState = this.FindByName<Button>("_btnCurrentState");

            btnAbout.Clicked += ToAboutPage;
            btnCurrentState.Clicked += ToStatePage;
        }

        private void btnCurrentState_Clicked(object sender, EventArgs e)
        {
            lblTest.Text = "1";

        }
        private void btnStatistics_Clicked(object sender, EventArgs e)
        {
            press_cnt++;
            lblTest.Text = "Заработало!!! Уже " + press_cnt.ToString() + " нажати";
            if (((press_cnt % 10) >= 5) || ((press_cnt % 10) == 0) || ((press_cnt >= 10) && (press_cnt <= 20)))
                lblTest.Text += "й";
            else if ((press_cnt % 10) == 1)
                lblTest.Text += "е";
            else
                lblTest.Text += "я";

            lblTest.Text += "!";
        }

        private void btnGraphics_Clicked(object sender, EventArgs e)
        {
            lblTest.Text = "3";
        }

        private void btnSettings_Clicked(object sender, EventArgs e)
        {
            lblTest.Text = "4";
        }


        private async void ToAboutPage(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new AboutPage()));
        }

        private async void ToStatePage(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new StatePage()));
        }
    }
}
