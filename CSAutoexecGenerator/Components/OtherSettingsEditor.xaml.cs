namespace CSAutoexecGenerator.Components;

public partial class OtherSettingsEditor : ContentView
{
	public OtherSettingsEditor()
	{
		InitializeComponent();
	}

    // Text Property and BindableProperty definitions
    public static readonly BindableProperty TextProperty = BindableProperty.Create(
        nameof(Text), typeof(string), typeof(OtherSettingsEditor));

    public string Text
    {
        get => (string)GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }
}