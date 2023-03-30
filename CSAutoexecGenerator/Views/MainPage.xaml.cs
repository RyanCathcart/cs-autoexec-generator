using CSAutoexecGenerator.ViewModels;

namespace CSAutoexecGenerator.Views;

public partial class MainPage : ContentPage
{
	int _count = 0;

	public MainPage(MainViewModel mainViewModel	)
	{
		InitializeComponent();
		BindingContext = mainViewModel;
	}

    private void OnCreateClicked(object sender, EventArgs e)
	{
		_count++;

		if (_count == 1)
            CreateButton.Text = $"Clicked {_count} time";
		else
            CreateButton.Text = $"Clicked {_count} times";

		SemanticScreenReader.Announce(CreateButton.Text);
	}
}

