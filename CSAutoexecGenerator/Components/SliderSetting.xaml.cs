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
            ((SliderSetting)bindable).SettingSlider.Maximum = (double)newValue);

    public double Maximum
    {
        get => (double)GetValue(MaximumProperty);
        set => SetValue(MaximumProperty, value);
    }


    public static readonly BindableProperty MinimumProperty = BindableProperty.Create(
        nameof(Minimum), typeof(double), typeof(SliderSetting),
        propertyChanged: (bindable, oldValue, newValue) =>
            ((SliderSetting)bindable).SettingSlider.Minimum = (double)newValue);

    public double Minimum
    {
        get => (double)GetValue(MinimumProperty);
        set => SetValue(MinimumProperty, value);
    }


    public SliderSetting()
	{
		InitializeComponent();
	}

    void OnSliderValueChanged(object sender, ValueChangedEventArgs args)
    {
        double value = args.NewValue;
        SliderValueEntry.Text = value.ToString("F2");
    }

    void OnEntryCompleted(object sender, EventArgs args)
    {
        string enteredValue = ((Entry)sender).Text;

        if (!double.TryParse(enteredValue, out double newValue)) return;
        
        if (newValue < Minimum) newValue = Minimum;
        else if (newValue > Maximum) newValue = Maximum;

        ((Entry)sender).Text = newValue.ToString("F2");
        SettingSlider.Value = newValue; 
    }
}