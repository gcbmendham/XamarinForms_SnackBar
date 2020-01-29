using System;
using System.Threading;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Xamarin.FormsSnackBarDemo
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SnackBar : TemplatedView
    {
        private readonly Timer _timer;

        public SnackBar()
        {
            this.TranslateTo(0, 50, 0);  // ensure the snack bar is initially off-screen
            _timer = new Timer(CloseOnTimeout);
            InitializeComponent();
        }

        #region Bindable Properties
        public static readonly BindableProperty ButtonTextColorProperty = BindableProperty.Create("ButtonTextColor", typeof(Color), typeof(SnackBar), default(Color));
        public Color ButtonTextColor
        {
            get { return (Color)GetValue(ButtonTextColorProperty); }
            set { SetValue(ButtonTextColorProperty, value); }
        }

        public static readonly BindableProperty MessageProperty = BindableProperty.Create("Message", typeof(string), typeof(SnackBar), default(string));
        public string Message
        {
            get { return (string)GetValue(MessageProperty); }
            set { SetValue(MessageProperty, value); }
        }

        public static readonly BindableProperty CloseButtonTextProperty = BindableProperty.Create("CloseButtonText", typeof(string), typeof(SnackBar), "Close");
        public string CloseButtonText
        {
            get { return (string)GetValue(CloseButtonTextProperty); }
            set { SetValue(CloseButtonTextProperty, value); }
        }

        public static readonly BindableProperty FontSizeProperty = BindableProperty.Create("FontSize", typeof(float), typeof(SnackBar), default(float));
        public float FontSize
        {
            get { return (float)GetValue(FontSizeProperty); }
            set { SetValue(FontSizeProperty, value); }
        }

        public static readonly BindableProperty TextColorProperty = BindableProperty.Create("TextColor", typeof(Color), typeof(SnackBar), Color.White);
        public Color TextColor
        {
            get { return (Color)GetValue(TextColorProperty); }
            set { SetValue(TextColorProperty, value); }
        }

        public static readonly BindableProperty CloseButtonBackGroundColorProperty = BindableProperty.Create("CloseButtonBackGroundColor", typeof(Color), typeof(SnackBar), Color.Transparent);
        public Color CloseButtonBackGroundColor
        {
            get { return (Color)GetValue(CloseButtonBackGroundColorProperty); }
            set { SetValue(CloseButtonBackGroundColorProperty, value); }
        }

        public static readonly BindableProperty FontFamilyProperty = BindableProperty.Create("FontFamily", typeof(string), typeof(SnackBar), default(string));
        public string FontFamily
        {
            get { return (string)GetValue(FontFamilyProperty); }
            set { SetValue(FontFamilyProperty, value); }
        }

        public static readonly BindableProperty ButtonCommandProperty =
            BindableProperty.Create(nameof(ButtonCommand), typeof(ICommand), typeof(SnackBar), null);

        public ICommand ButtonCommand
        {
            get => (ICommand)GetValue(ButtonCommandProperty);
            set { SetValue(ButtonCommandProperty, value); }
        }

        static void OnButtonCommandPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is SnackBar snackBar && newValue is ICommand command)
            {
                snackBar.ButtonCommand = command;
            }
        }
        #endregion


        #region Properties
        public uint AnimationDuration { get; set; } = 200;

        public uint TimeoutDuration { get; set; }

        #endregion

        private void CloseOnTimeout(object state) => Device.BeginInvokeOnMainThread(() => Close());

        private void Button_Clicked(object sender, EventArgs e)
        {
            if (ButtonCommand == null) 
                return;

            if (ButtonCommand.CanExecute(null))
                ButtonCommand.Execute(null);
        }

        public async void Close()
        {
            Message = string.Empty;
            await this.TranslateTo(0, 50, AnimationDuration);
        }

        public async void Open(string message)
        {
            Message = message;
            if (TimeoutDuration > 0)
                _timer.Change(TimeoutDuration, Timeout.Infinite);

            await this.TranslateTo(0, 0, AnimationDuration);
        }
    }
}