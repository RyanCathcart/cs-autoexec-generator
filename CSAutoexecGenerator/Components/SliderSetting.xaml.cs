namespace CSAutoexecGenerator.Components;

public partial class SliderSetting : ContentView
{
    public static readonly BindableProperty TitleProperty = BindableProperty.Create(
		nameof(Title), typeof(string), typeof(SliderSetting), 
		propertyChanged: (bindable, oldValue, newValue) => 
            ((SliderSetting)bindable).TitleLabel.Text = (string)newValue);

	public string Title 
	{
		get => (string)GetValue(TitleProperty); 
		set => SetValue(TitleProperty, value);
	}


    public static readonly BindableProperty MaximumProperty = BindableProperty.Create(
        nameof(Maximum), typeof(double), typeof(SliderSetting),
        propertyChanged: (bindable, oldValue, newValue) =>
        {
            var control = (SliderSetting)bindable;
            control.SettingSlider.Maximum = (double)newValue;
            control.MaxLabel.Text = ((double)newValue).ToString();
        });

    public double Maximum
    {
        get => (double)GetValue(MaximumProperty);
        set => SetValue(MaximumProperty, value);
    }


    public static readonly BindableProperty MinimumProperty = BindableProperty.Create(
        nameof(Minimum), typeof(double), typeof(SliderSetting),
        propertyChanged: (bindable, oldValue, newValue) =>
        {
            var control = (SliderSetting)bindable;
            control.SettingSlider.Minimum = (double)newValue;
            control.MinLabel.Text = ((double)newValue).ToString();
        });

    public double Minimum
    {
        get => (double)GetValue(MinimumProperty);
        set => SetValue(MinimumProperty, value);
    }


    public static readonly BindableProperty ValueProperty = BindableProperty.Create(
        nameof(Value), typeof(double), typeof(SliderSetting),
        propertyChanged: (bindable, oldValue, newValue) =>
        {
            var control = (SliderSetting)bindable;
            control.SettingSlider.Value = (double)newValue;
            control.SliderValueEntry.Text = ((double)newValue).ToString("F2");
        });

    public double Value
    {
        get => (double)GetValue(ValueProperty);
        set => SetValue(ValueProperty, value);
    }


    public SliderSetting()
	{
		InitializeComponent();
	}

    void OnSliderValueChanged(object sender, ValueChangedEventArgs args)
    {
        double newValue = args.NewValue;
        Value = newValue;
    }

    void OnEntryCompleted(object sender, EventArgs args)
    {
        string enteredValue = ((Entry)sender).Text;

        if (!double.TryParse(enteredValue, out double newValue)) return;
        
        if (newValue < Minimum) newValue = Minimum;
        else if (newValue > Maximum) newValue = Maximum;

        Value = newValue;
    }
}