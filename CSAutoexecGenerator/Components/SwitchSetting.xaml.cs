namespace CSAutoexecGenerator.Components;

public partial class SwitchSetting : ContentView
{
    public SwitchSetting()
    {
        InitializeComponent();
    }

    // Title Property and BindableProperty definitions
    public static readonly BindableProperty TitleProperty = BindableProperty.Create(
        nameof(Title), typeof(string), typeof(SwitchSetting), "Default Title");

    public string Title
    {
        get => (string)GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }


    // Description Property and BindableProperty definitions
    public static readonly BindableProperty DescriptionProperty = BindableProperty.Create(
        nameof(Description), typeof(string), typeof(SwitchSetting), "Default description.");

    public string Description
    {
        get => (string)GetValue(DescriptionProperty);
        set => SetValue(DescriptionProperty, value);
    }


    // Value Property and BindableProperty definitions
    public static readonly BindableProperty ValueProperty = BindableProperty.Create(
        nameof(Value), typeof(bool), typeof(SwitchSetting));

    public bool Value
    {
        get => (bool)GetValue(ValueProperty);
        set => SetValue(ValueProperty, value);
    }
}