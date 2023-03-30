namespace CSAutoexecGenerator.Components;

public partial class SliderSetting : ContentView
{
    public SliderSetting()
    {
        InitializeComponent();
    }

    // Title Property and BindableProperty definitions
    public static readonly BindableProperty TitleProperty = BindableProperty.Create(
        nameof(Title), typeof(string), typeof(SliderSetting), "Default Title");

	public string Title 
	{
		get => (string)GetValue(TitleProperty); 
		set => SetValue(TitleProperty, value);
	}


    // Description Property and BindableProperty definitions
    public static readonly BindableProperty DescriptionProperty = BindableProperty.Create(
        nameof(Description), typeof(string), typeof(SliderSetting), "Default description.");

    public string Description
    {
        get => (string)GetValue(DescriptionProperty);
        set => SetValue(DescriptionProperty, value);
    }


    // Maximum Property and BindableProperty definitions
    public static readonly BindableProperty MaximumProperty = BindableProperty.Create(
        nameof(Maximum), typeof(double), typeof(SliderSetting), 1.0);


    public double Maximum
    {
        get => (double)GetValue(MaximumProperty);
        set => SetValue(MaximumProperty, value);
    }


    // Minimum Property and BindableProperty definitions
    public static readonly BindableProperty MinimumProperty = BindableProperty.Create(
        nameof(Minimum), typeof(double), typeof(SliderSetting), 0.0);

    public double Minimum
    {
        get => (double)GetValue(MinimumProperty);
        set => SetValue(MinimumProperty, value);
    }


    // Value Property and BindableProperty definitions
    public static readonly BindableProperty ValueProperty = BindableProperty.Create(
        nameof(Value), typeof(double), typeof(SliderSetting),
        propertyChanged: OnValuePropertyChanged);

    static void OnValuePropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var control = (SliderSetting)bindable;
        control.SettingSlider.Value = (double)newValue;
        control.SliderValueEntry.Text = ((double)newValue).ToString("F2");
    }

    public double Value
    {
        get => (double)GetValue(ValueProperty);
        set => SetValue(ValueProperty, value);
    }


    void OnSliderValueChanged(object sender, ValueChangedEventArgs e)
    {
        if (e.NewValue == e.OldValue) return;

        Value = e.NewValue;
    }

    void OnEntryCompleted(object sender, EventArgs e)
    {
        string enteredValue = ((Entry)sender).Text;

        if (!double.TryParse(enteredValue, out double newValue)) return;
        
        if (newValue < Minimum) newValue = Minimum;
        else if (newValue > Maximum) newValue = Maximum;

        Value = newValue;
    }
}