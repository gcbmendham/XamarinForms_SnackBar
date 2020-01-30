using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Xamarin.FormsSnackBarDemo
{
    public partial class MainPage : ContentPage
    {
        private static int i = 0;

        public MainPage()
        {
            BindingContext = this;

            InitializeComponent();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            SnackB.Message=$"Instance #{++i}.";
            //await SnackB.Open();
            SnackB.IsShown = true;
        }

        public ICommand SnackBarButtonCommand => new Command(ButtonWasClicked);

        public void ButtonWasClicked()
        {
            //await SnackB.Close();
            SnackB.IsShown = false;
        }
    }
}
