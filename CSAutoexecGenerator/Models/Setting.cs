using System.Text.Json.Serialization;

namespace CSAutoexecGenerator.Models;

[JsonDerivedType(typeof(IntSetting), typeDiscriminator: "int")]
[JsonDerivedType(typeof(DoubleSetting), typeDiscriminator: "double")]
[JsonDerivedType(typeof(BooleanSetting), typeDiscriminator: "boolean")]
public abstract class Setting
{
    public string Name { get; set; }
    public string Description { get; set; }
}
