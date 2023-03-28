namespace CSAutoexecGenerator.Components;

public partial class SwitchSetting : ContentView
{
    public static readonly BindableProperty TitleProperty = BindableProperty.Create(
        nameof(Title), typeof(string), typeof(SwitchSetting),
        propertyChanged: (bindable, oldValue, newValue) =>
            ((SwitchSetting)bindable).TitleLabel.Text = (string)newValue);

    public string Title
    {
        get => (string)GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }


    public static readonly BindableProperty DescriptionProperty = BindableProperty.Create(
        nameof(Description), typeof(string), typeof(SwitchSetting),
        propertyChanged: (bindable, oldValue, newValue) =>
            ((SwitchSetting)bindable).DescriptionLabel.Text = (string)newValue);

    public string Description
    {
        get => (string)GetValue(DescriptionProperty);
        set => SetValue(DescriptionProperty, value);
    }


    public static readonly BindableProperty ValueProperty = BindableProperty.Create(
        nameof(Value), typeof(bool), typeof(SwitchSetting),
        propertyChanged: (bindable, oldValue, newValue) =>
        {
            var control = (SwitchSetting)bindable;
            control.SettingSwitch.IsToggled = (bool)newValue;
        });

    public bool Value
    {
        get => (bool)GetValue(ValueProperty);
        set => SetValue(ValueProperty, value);
    }


    public SwitchSetting()
	{
		InitializeComponent();
	}

    void OnSwitchToggled(object sender, ToggledEventArgs args)
    {
        Value = args.Value;
    }
}