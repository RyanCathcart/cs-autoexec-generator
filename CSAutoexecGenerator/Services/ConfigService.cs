using CSAutoexecGenerator.Models;
using System.Diagnostics;
using System.Text.Json;

namespace CSAutoexecGenerator.Services;

public class ConfigService
{
    readonly List<SettingGroup> _settingList = new();
    readonly List<string> _excludeList = new() { "//", "host_writeconfig", "echo" };

    public async Task<List<SettingGroup>> LoadDefaultConfigAsync()
    {
        var fileName = "default_config.json";

        using (var stream = await FileSystem.OpenAppPackageFileAsync(fileName))
        {
            using var reader = new StreamReader(stream);

            var json = reader.ReadToEnd();

            var temp = JsonSerializer.Deserialize<List<SettingGroupDto>>(json);

            foreach (var settingGroup in temp)
            {
                _settingList.Add(new SettingGroup(settingGroup.Name, settingGroup.Settings));
            }
        }

        _settingList.Add(new SettingGroup("Other", new List<Setting> { new OtherSettings() }));

        return _settingList;
    }

    public static async Task ExportConfigAsync(string savePath, ICollection<SettingGroup> settings)
    {
        await Task.Delay(1000);

        using StreamWriter outputFile = new(savePath);

        foreach (var settingGroup in settings)
        {
            await outputFile.WriteLineAsync($"// {settingGroup.Name}");

            foreach (var setting in settingGroup)
            {
                if (setting is OtherSettings otherSettings)
                {
                    await outputFile.WriteAsync(otherSettings.Text);

                    continue;
                }

                var command = setting.Name;
                string value;

                if (setting is IntSetting intSetting) 
                    value = intSetting.Value.ToString();
                else if (setting is DoubleSetting doubleSetting) 
                    value = doubleSetting.Value.ToString();
                else 
                    value = ((BooleanSetting)setting).Value ? "1" : "0";

                await outputFile.WriteLineAsync($"{command} \"{value}\"");
            }

            await outputFile.WriteAsync($"\n\n");
        }

        await outputFile.WriteLineAsync(
            $"host_writeconfig\n" +
            $"echo \"\"\n" +
            $"echo \"\"\n" +
            $"echo \"autoexec.cfg loaded\"\n" +
            $"echo \"\"\n" +
            $"echo \"\"");
    }

    public async Task<List<SettingGroup>> ImportConfigAsync(FileResult file, ICollection<SettingGroup> settings)
    {
        using (var fileStream = await file.OpenReadAsync())
        using (var reader = new StreamReader(fileStream))
        {
            string line;

            while ((line = reader.ReadLine()) != null)
            {
                if (ShouldExclude(line)) continue;

                string[] props = line.Split(' ');

                var settingName = props[0];
                var settingValue = props[1].Trim('"');

                bool included = false;

                foreach (var settingGroup in settings)
                {
                    foreach (var setting in settingGroup)
                    {
                        if (setting.Name == settingName)
                        {
                            if (setting is IntSetting intSetting)
                            {
                                intSetting.Value = int.Parse(settingValue);
                            }
                            else if (setting is DoubleSetting doubleSetting)
                            {
                                doubleSetting.Value = double.Parse(settingValue);
                            }
                            else
                            {
                                ((BooleanSetting)setting).Value = settingValue == "1";
                            }
                            included = true;
                        }
                    }
                }

                if (!included) 
                {
                    var otherSettings = (OtherSettings)settings.FirstOrDefault(x => x.Name == "Other")[0];
                    otherSettings.Text += $"{line}\n";
                }
                
            }
        }

        return new List<SettingGroup>(settings);
    }

    private bool ShouldExclude(string line)
    {
        if (string.IsNullOrWhiteSpace(line)) return true;

        foreach(var prefix in _excludeList)
        {
            if (line.StartsWith(prefix)) return true;
        }

        return false;
    }
}
