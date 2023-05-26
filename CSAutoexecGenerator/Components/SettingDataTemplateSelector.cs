using CSAutoexecGenerator.Models;

namespace CSAutoexecGenerator.Components;

public class SettingDataTemplateSelector : DataTemplateSelector
{
    public DataTemplate IntValue { get; set; }
    public DataTemplate DoubleValue { get; set; }
    public DataTemplate BooleanValue { get; set; }
    public DataTemplate OtherSetting { get; set; }

    protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
    {
        if (item is IntSetting) return IntValue;
        if (item is DoubleSetting) return DoubleValue;
        if (item is BooleanSetting) return BooleanValue;
        return OtherSetting;
    }
}
