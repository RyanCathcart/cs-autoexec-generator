using CSAutoexecGenerator.ViewModels;

namespace CSAutoexecGenerator.Views;

public partial class CreateConfigPage : ContentPage
{
	public CreateConfigPage(CreateConfigViewModel createConfigViewModel)
	{
		InitializeComponent();
		BindingContext = createConfigViewModel;
	}
}
