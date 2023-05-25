namespace CSAutoexecGenerator.Components;

public partial class IntSliderSetting : ContentView
{
	public IntSliderSetting()
	{
		InitializeComponent();
	}

    // Title Property and BindableProperty definitions
    public static readonly BindableProperty TitleProperty = BindableProperty.Create(
        nameof(Title), typeof(string), typeof(IntSliderSetting), "Default Title");

    public string Title
    {
        get => (string)GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }


    // Description Property and BindableProperty definitions
    public static readonly BindableProperty DescriptionProperty = BindableProperty.Create(
        nameof(Description), typeof(string), typeof(IntSliderSetting), "Default description.");

    public string Description
    {
        get => (string)GetValue(DescriptionProperty);
        set => SetValue(DescriptionProperty, value);
    }


    // Maximum Property and BindableProperty definitions
    public static readonly BindableProperty MaximumProperty = BindableProperty.Create(
        nameof(Maximum), typeof(int), typeof(IntSliderSetting), 1);


    public int Maximum
    {
        get => (int)GetValue(MaximumProperty);
        set => SetValue(MaximumProperty, value);
    }


    // Minimum Property and BindableProperty definitions
    public static readonly BindableProperty MinimumProperty = BindableProperty.Create(
        nameof(Minimum), typeof(int), typeof(IntSliderSetting), 0);

    public int Minimum
    {
        get => (int)GetValue(MinimumProperty);
        set => SetValue(MinimumProperty, value);
    }


    // Value Property and BindableProperty definitions
    public static readonly BindableProperty ValueProperty = BindableProperty.Create(
        nameof(Value), typeof(int), typeof(IntSliderSetting), propertyChanged: OnValueChanged);

    private static void OnValueChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var control = (IntSliderSetting)bindable;

        int value = (int)newValue;

        if (value < control.Minimum) value = control.Minimum;
        else if (value > control.Maximum) value = control.Maximum;

        control.Slider.Value = value;
        control.Entry.Text = value.ToString("G");
    }

    public int Value
    {
        get => (int)GetValue(ValueProperty);
        set => SetValue(ValueProperty, value);
    }

    void OnEntryCompleted(object sender, EventArgs e)
    {
        var entry = (Entry)sender;

        if (int.TryParse(entry.Text, out int newValue) == false)
        {
            entry.Text = Value.ToString("G");
            return;
        }

        if (newValue < Minimum) newValue = Minimum;
        else if (newValue > Maximum) newValue = Maximum;

        entry.Text = newValue.ToString("G");

        Value = newValue;
    }
}