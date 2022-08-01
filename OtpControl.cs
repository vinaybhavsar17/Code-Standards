using OfficerReports.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficerReports.Controls
{
    public class OtpControl : ContentView
    {
        private StackLayout _outerLayout;
        private StackLayout _layoutWrapper;
        private Microsoft.Maui.Controls.Entry _proxyEntry;

        public const int DefaultBoxWidth = 55;
        public const int DefaultBoxHeight = 60;

        public event EventHandler Completed;

        public static readonly BindableProperty OtpProperty = BindableProperty.Create(nameof(Otp), typeof(string), typeof(OtpControl), defaultBindingMode: BindingMode.TwoWay, propertyChanged: OtpPropertyChanged);

        private static void OtpPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            ((OtpControl)bindable).FillEntryBoxes();
        }

        public string Otp
        {
            get => (string)GetValue(OtpProperty);
            set => SetValue(OtpProperty, value);
        }

        private int _digits = 4;
        public int Digits
        {
            get { return _digits; }
            set
            {
                _digits = value;
                OnPropertyChanged(nameof(Digits));
            }
        }

        private int _spacing = 20;
        public int Spacing
        {
            get { return _spacing; }
            set
            {
                _spacing = value;
                OnPropertyChanged(nameof(Spacing));
            }
        }

        private double _otpBoxWidth = DefaultBoxWidth;
        public double OtpBoxWidth
        {
            get { return _otpBoxWidth; }
            set
            {
                _otpBoxWidth = value;
                OnPropertyChanged(nameof(OtpBoxWidth));
            }
        }

        private double _otpBoxHeight = DefaultBoxHeight;
        public double OtpBoxHeight
        {
            get { return _otpBoxHeight; }
            set
            {
                _otpBoxHeight = value;
                OnPropertyChanged(nameof(OtpBoxHeight));
            }
        }

        //Not fully implemented, in future we can utilize this if required
        private bool _secured = false;
        public bool Secured
        {
            get { return _secured; }
            set
            {
                _secured = value;
                OnPropertyChanged(nameof(Secured));
            }
        }

        public OtpControl()
        {
            _outerLayout = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                Spacing = 0
            };

            _layoutWrapper = InitLayoutWrapper();
            _outerLayout.Add(_layoutWrapper);

            _proxyEntry = InitProxyEntry();
            _outerLayout.Add(_proxyEntry);

            Content = _outerLayout;
        }

        protected override void OnParentSet()
        {
            base.OnParentSet();

            _layoutWrapper.Spacing = Spacing;
            _proxyEntry.MaxLength = Digits;
            RenderEntryBoxes();
        }

        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);

            if (width > -1 && height > -1)
                FocusProxyEntry();
        }

        private void RenderEntryBoxes()
        {
            _layoutWrapper.Clear();

            for (int i = 0; i < Digits; i++)
            {
                var singleDigitEntry = new SingleDigitEntry
                {
                    WidthRequest = OtpBoxWidth,
                    HeightRequest = OtpBoxHeight
                };

                _layoutWrapper.Add(singleDigitEntry);
            }
        }

        private void FillEntryBoxes()
        {
            for (int i = 0; i < _layoutWrapper.Count; i++)
            {
                var entryBox = (SingleDigitEntry)_layoutWrapper.Children[i];

                if(Otp.Length > i)
                {
                    var otpChar = Otp[i];
                    entryBox.Text = otpChar.ToString();
                }
                else
                {
                    entryBox.Text = String.Empty;
                }
            }
        }

        public void SetFocus()
        {
            FocusProxyEntry();
        }

        private void FocusProxyEntry()
        {
            _proxyEntry.Focus();
            _proxyEntry.CursorPosition = _proxyEntry.Text != null ? _proxyEntry.Text.Length : 0;
            PlatformServices.OpenKeyboard(_proxyEntry);
        }

        private Microsoft.Maui.Controls.Entry InitProxyEntry()
        {
            var entry = new Microsoft.Maui.Controls.Entry
            {
                HeightRequest = 1,
                Opacity = 0,
                MaxLength = Digits,
                Keyboard = Keyboard.Numeric
            };
            entry.SetBinding(Microsoft.Maui.Controls.Entry.TextProperty, nameof(Otp), mode: BindingMode.TwoWay);
            entry.Completed += (s, e) =>
            {
                PlatformServices.HideKeyboard(_proxyEntry);
            };
            entry.TextChanged += (s, e) =>
            {
                if (e.NewTextValue.Length == Digits)
                    Completed?.Invoke(this, new EventArgs());
            };

            return entry;
        }

        private StackLayout InitLayoutWrapper()
        {
            var lw = new StackLayout
            {
                Orientation = StackOrientation.Horizontal
            };
            var tapGesture = new TapGestureRecognizer
            {
                Command = new Command(FocusProxyEntry)
            };
            lw.GestureRecognizers.Add(tapGesture);

            return lw;
        }
    }
}
