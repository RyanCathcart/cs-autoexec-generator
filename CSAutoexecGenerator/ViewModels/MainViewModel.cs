using CommunityToolkit.Mvvm.ComponentModel;
using CSAutoexecGenerator.Models;
using CSAutoexecGenerator.Services;
using System.Collections.ObjectModel;

namespace CSAutoexecGenerator.ViewModels;

public partial class MainViewModel : ObservableObject
{
    private readonly ConfigService _configService;

    public ObservableCollection<Setting> Settings { get; set; } = new();

    public MainViewModel(ConfigService configService)
    {
        _configService = configService;
        GetConfig();
    }

    void GetConfig()
    {
        var settings = _configService.GetSettings();

        foreach (var setting in settings)
        {
            Settings.Add(setting);
        }
    }
}
