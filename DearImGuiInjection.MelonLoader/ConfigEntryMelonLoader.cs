using MelonLoader;
using MelonLoader.Utils;

namespace DearImGuiInjection.MelonLoader;

public class ConfigEntryMelonLoader<T> : IConfigEntry<T>
{
    private MelonPreferences_Entry<T> _configEntry;
    
    public ConfigEntryMelonLoader(MelonPreferences_Entry<T> entry) => _configEntry = entry;
    
    public T GetValue() => _configEntry.Value;

    public T SetValue(T value) => _configEntry.Value = value;
}