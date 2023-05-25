using CommunityToolkit.Maui.Storage;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CSAutoexecGenerator.Models;
using CSAutoexecGenerator.Services;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace CSAutoexecGenerator.ViewModels;

public partial class CreateConfigViewModel : ObservableObject
{
    public const string DEFAULT_FILE_NAME = "autoexec";

    private readonly ConfigService _configService;
    private readonly IFolderPicker _folderPicker;

    [ObservableProperty]
    ObservableCollection<SettingGroup> _settings = new();

    [ObservableProperty]
    string _title;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsNotBusy))]
    bool _isBusy;

    public bool IsNotBusy => !IsBusy;

    [ObservableProperty]
    string _saveDestination = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

    [ObservableProperty]
    string _saveFileName = DEFAULT_FILE_NAME;

    public CreateConfigViewModel(ConfigService configService, IFolderPicker folderPicker)
    {
        Title = "Create Config";
        _configService = configService;
        _folderPicker = folderPicker;
    }

    [RelayCommand]
    public async Task GetDefaultConfigAsync()
    {
        if (Settings.Any()) return;

        var settings = await _configService.LoadDefaultConfigAsync();

        foreach (var settingGroup in settings)
        {
            Settings.Add(settingGroup);
        }

        Settings.Add(new SettingGroup("Other", new List<Setting>()));
    }

    [RelayCommand]
    public async Task GetConfigAsync()
    {
        PickOptions options = new()
        {
            FileTypes = new FilePickerFileType(
                new Dictionary<DevicePlatform, IEnumerable<string>>()
                {
                    { DevicePlatform.WinUI, new[] { ".cfg" } }
                })
        };

        var file = await FilePicker.Default.PickAsync(options);

        if (file == null) return;

        Settings = new(await _configService.ImportConfigAsync(file, Settings));
    }

    [RelayCommand]
    public async Task UpdateSaveDestinationAsync(CancellationToken cancellationToken)
    {
        var result = await _folderPicker.PickAsync(cancellationToken);

        if (result.IsSuccessful)
        {
            SaveDestination = result.Folder.Path;
        }
    }

    [RelayCommand]
    public async Task CreateConfigAsync()
    {
        if (IsBusy) return;

        try
        {
            IsBusy = true;

            string savePath = Path.Combine(SaveDestination, $"{SaveFileName}.cfg");

            await ConfigService.ExportConfigAsync(savePath, Settings);
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Unable to create config: {ex.Message}");

            if (FileNameContainsBadSymbols()) 
                await Application.Current.MainPage.DisplayAlert("Error: Could not create config.", 
                    "File name cannot contain any of the following characters:\n\t\\ / : * ? \" < > |", 
                    "OK");
            else
            {
                await Application.Current.MainPage
                    .DisplayAlert("Error: Could not create config.", ex.Message, "OK");
            }
        }
        finally
        {
            IsBusy = false;
        }
    }

    private bool FileNameContainsBadSymbols()
    {
        var badSymbols = new List<char> { '\\', '/', ':', '*', '?', '"', '<', '>', '|' };
        
        foreach (char symbol in badSymbols)
        {
            if (SaveFileName.Contains(symbol)) return true;
        }

        return false;
    }

    [RelayCommand]
    public void OnFileNameEntryCompleted()
    {
        if (string.IsNullOrWhiteSpace(SaveFileName))
        {
            SaveFileName = DEFAULT_FILE_NAME;
        }
    }
}
