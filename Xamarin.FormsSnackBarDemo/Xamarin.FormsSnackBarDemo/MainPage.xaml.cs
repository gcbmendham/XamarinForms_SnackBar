﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Xamarin.FormsSnackBarDemo
{
    public partial class MainPage : ContentPage
    {
        private static int i = 0;

        public MainPage()
        {
            InitializeComponent();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            //SnackB.Message = "I'm a snack bar... I love showing my self.";
            //SnackB.IsOpen = !SnackB.IsOpen;
            SnackB.Open($"Instance #{++i}.");
        }
    }
}
