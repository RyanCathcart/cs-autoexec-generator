using CSAutoexecGenerator.Models;

namespace CSAutoexecGenerator.Services;

public class ConfigService
{
    List<Setting> _settingList = new();

    public List<Setting> GetSettings()
    {
        var filePath = "C:\\Users\\Ryan Cathcart\\Documents\\dev\\repos\\cs-autoexec-generator\\CSAutoexecGenerator\\Resources\\Raw\\DefaultConfig.txt";

        using (var reader = new StreamReader(filePath))
        {
            string line;

            while ((line = reader.ReadLine()) != null)
            {
                string[] sections = line.Split(' ');

                var settingName = sections[0];
                var settingValue = double.Parse(sections[1].Trim('"'));

                _settingList.Add(new Setting
                {
                    Name = settingName,
                    Value =  settingValue
                });
            }
        }

        return _settingList;
    }
}
