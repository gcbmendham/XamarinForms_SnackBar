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
        private bool _isShown;

        public SnackBar()
        {
            InitializeComponent();

            // SnackBar is initially hidden
            ShowHide(false);

            // When the timer times out it should close the SnackBar
            _timer = new Timer( o => ShowHide(false) );
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

        public static readonly BindableProperty ButtonTextColorProperty = BindableProperty.Create(nameof(ButtonTextColor), typeof(Color), typeof(SnackBar), Color.Orange);
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

        #endregion

        #region Properties

        public uint AnimationDuration { get; set; } = 200;

        public uint TimeoutDuration { get; set; }

        public bool IsShown
        {
            get => _isShown;
            set {
                // Don't check to see if the value changed.  We may want to show the SnackBar
                // even while it is currently shown.  That way the timer (if set) will start
                // a fresh countdown every time that ShowHide(true) is called.
                _isShown = value;
                ShowHide(value);
            }
        }

        #endregion

        private void Button_Clicked(object sender, EventArgs e)
        {
            if (ButtonCommand == null) 
                return;

            if (ButtonCommand.CanExecute(null))
                ButtonCommand.Execute(null);
        }

        private void ShowHide(bool show)
        {
            int heightRequest = HeightRequest;

            if (show)
            {
                heightRequest = 0;
                if (TimeoutDuration > 0)
                    _timer.Change(TimeoutDuration, Timeout.Infinite);
            }

            this.TranslateTo(0, heightRequest, AnimationDuration);
        }
    }
}