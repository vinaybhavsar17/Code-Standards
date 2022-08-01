namespace OfficerReports.Controls;

public partial class SingleDigitEntry : ContentView
{
    public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(SingleDigitEntry), defaultBindingMode: BindingMode.TwoWay);

    public string Text
    {
        get => (string)GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }

    public SingleDigitEntry()
	{
		InitializeComponent();

		WidthRequest = OtpControl.DefaultBoxWidth;
		HeightRequest = OtpControl.DefaultBoxHeight;
	}
}