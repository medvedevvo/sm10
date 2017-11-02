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
        private Label lblTest;
        private int press_cnt = 0;

        public MainPage()
        {
            InitializeComponent();

            lblTest = this.FindByName<Label>("_lblTest");

        }

        private void btnCurrentState_Clicked(object sender, EventArgs e)
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
    }
}
