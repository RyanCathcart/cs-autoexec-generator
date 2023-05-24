namespace CSAutoexecGenerator.Models;

public class SettingGroup : List<Setting>
{
    public string Name { get; set; }

	public SettingGroup(string name, List<Setting> settings) : base(settings)
	{
		Name = name;
	}
}
