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
            InitializeComponent();

            _timer = new Timer(CloseOnTimeout);
            this.TranslateTo(0, HeightRequest, 0);  // ensure the snack bar is initially off-screen
        }

        #region Bindable Properties

        public new static readonly BindableProperty HeightRequestProperty = BindableProperty.Create(nameof(HeightRequest), typeof(int), typeof(SnackBar), 50, propertyChanged: OnHeightRequestChanged);

        private static void OnHeightRequestChanged(BindableObject bindable, object oldValue, object newValue)
        {
            (bindable as SnackBar).TranslateTo(0, (int)newValue, 0);
        }

        public new int HeightRequest
        {
            get => (int)GetValue(HeightRequestProperty);
            set { SetValue(HeightRequestProperty, value); }
        }

        public static readonly BindableProperty ButtonTextColorProperty = BindableProperty.Create(nameof(ButtonTextColor), typeof(Color), typeof(SnackBar), default(Color));
        public Color ButtonTextColor
        {
            get => (Color)GetValue(ButtonTextColorProperty);
            set { SetValue(ButtonTextColorProperty, value); }
        }

        public static readonly BindableProperty MessageProperty = BindableProperty.Create(nameof(Message), typeof(string), typeof(SnackBar), default(string));
        public string Message
        {
            get => (string)GetValue(MessageProperty);
            set { SetValue(MessageProperty, value); }
        }

        public static readonly BindableProperty ButtonTextProperty = BindableProperty.Create(nameof(ButtonText), typeof(string), typeof(SnackBar), "Close");
        public string ButtonText
        {
            get =>  (string)GetValue(ButtonTextProperty); 
            set { SetValue(ButtonTextProperty, value); }
        }

        public static readonly BindableProperty FontSizeProperty = BindableProperty.Create(nameof(FontSize), typeof(float), typeof(SnackBar), default(float));
        public float FontSize
        {
            get =>  (float)GetValue(FontSizeProperty); 
            set { SetValue(FontSizeProperty, value); }
        }

        public static readonly BindableProperty TextColorProperty = BindableProperty.Create(nameof(TextColor), typeof(Color), typeof(SnackBar), Color.White);
        public Color TextColor
        {
            get =>  (Color)GetValue(TextColorProperty); 
            set { SetValue(TextColorProperty, value); }
        }

        public static readonly BindableProperty ButtonBackGroundColorProperty = BindableProperty.Create(nameof(ButtonBackGroundColor), typeof(Color), typeof(SnackBar), Color.Transparent);
        public Color ButtonBackGroundColor
        {
            get =>  (Color)GetValue(ButtonBackGroundColorProperty); 
            set { SetValue(ButtonBackGroundColorProperty, value); }
        }

        public static readonly BindableProperty FontFamilyProperty = BindableProperty.Create(nameof(FontFamily), typeof(string), typeof(SnackBar), default(string));
        public string FontFamily
        {
            get =>  (string)GetValue(FontFamilyProperty); 
            set { SetValue(FontFamilyProperty, value); }
        }

        public static readonly BindableProperty ButtonCommandProperty =
            BindableProperty.Create(nameof(ButtonCommand), typeof(ICommand), typeof(SnackBar), null);

        public ICommand ButtonCommand
        {
            get => (ICommand)GetValue(ButtonCommandProperty);
            set { SetValue(ButtonCommandProperty, value); }
        }

        //static void OnButtonCommandPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        //{
        //    if (bindable is SnackBar snackBar && newValue is ICommand command)
        //    {
        //        snackBar.ButtonCommand = command;
        //    }
        //}
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
            await this.TranslateTo(0, HeightRequest, AnimationDuration);
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