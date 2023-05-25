using CSAutoexecGenerator.Models;

namespace CSAutoexecGenerator.Components;

public class SettingDataTemplateSelector : DataTemplateSelector
{
    public DataTemplate IntValueTemplate { get; set; }
    public DataTemplate DoubleValueTemplate { get; set; }
    public DataTemplate BooleanValueTemplate { get; set; }

    protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
    {
        if (item is IntSetting) return IntValueTemplate;
        if (item is DoubleSetting) return DoubleValueTemplate;
        return BooleanValueTemplate;
    }
}
