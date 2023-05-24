using CSAutoexecGenerator.Models;

namespace CSAutoexecGenerator.Components;

public class SettingDataTemplateSelector : DataTemplateSelector
{
    public DataTemplate DoubleValueTemplate { get; set; }
    public DataTemplate BooleanValueTemplate { get; set; }

    protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
    {
        return item is DoubleSetting ? DoubleValueTemplate : BooleanValueTemplate;
    }
}
