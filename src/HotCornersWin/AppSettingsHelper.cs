using System.Text.Json;

namespace HotCornersWin;

public enum SettingsStorageOption
{
    Default = 0,
    Filesystem
}

public class AppSettingsHelper : IAppSettingsHelper
{
    private const string DataFolderName = "data";
    private const string SettingsFileName = "settings.json";

    private readonly JsonSerializerOptions _jsonOptions = new()
    {
        WriteIndented = true,
    };

    private readonly SettingsStorageOption _option;

    private AppSettingsData _data;

    public AppSettingsData Settings => _data;

    public AppSettingsHelper(SettingsStorageOption option)
    {
        _option = option;
        _data = Load(option) ?? new();
    }

    public void Save()
    {
        switch (_option)
        {
            case SettingsStorageOption.Default:
                foreach (var prop in typeof(AppSettingsData).GetProperties())
                {
                    try
                    {
                        if (prop.PropertyType.IsEnum)
                        {
                            Properties.Settings.Default.PropertyValues[prop.Name].PropertyValue = (int)(prop.GetValue(_data) ?? 0);
                        }
                        else
                        {
                            Properties.Settings.Default.PropertyValues[prop.Name].PropertyValue = prop.GetValue(_data);
                        }
                    }
                    catch { }
                }
                Properties.Settings.Default.Save();
                break;
            case SettingsStorageOption.Filesystem:
                try
                {
                    string dataPath = Path.Combine(Environment.CurrentDirectory, DataFolderName);
                    _ = Directory.CreateDirectory(dataPath);
                    string settingsFullPath = Path.Combine(dataPath, SettingsFileName);
                    string serializedSettings = JsonSerializer.Serialize(_data, _jsonOptions);
                    File.WriteAllText(settingsFullPath, serializedSettings);
                }
                catch
                {
                    // TODO error notification?
                }
                break;
        }
    }

    public void Reload()
    {
        _data = Load(_option) ?? new();
    }

    private AppSettingsData? Load(SettingsStorageOption option)
    {
        switch (option)
        {
            case SettingsStorageOption.Default:
                AppSettingsData data = new();
                // Without retrieving a property first,
                // PropertyValues collection is empty.
                // Probably this is a platform bug.
                var enabled = Properties.Settings.Default.IsEnabled;
                data.IsEnabled = enabled;
                foreach (var prop in typeof(AppSettingsData).GetProperties())
                {
                    try
                    {
                        var value = Properties.Settings.Default.PropertyValues[prop.Name].PropertyValue;

                        if (prop.PropertyType.IsEnum && !Enum.IsDefined(prop.PropertyType, value))
                        {
                            prop.SetValue(data, 0);
                        }
                        else
                        {
                            prop.SetValue(data, value);
                        }
                    }
                    catch { }
                }
                return data;
            case SettingsStorageOption.Filesystem:
                string dataPath = Path.Combine(Environment.CurrentDirectory, DataFolderName);
                try
                {
                    _ = Directory.CreateDirectory(dataPath);
                    string settingsFullPath = Path.Combine(dataPath, SettingsFileName);
                    if (File.Exists(settingsFullPath))
                    {
                        string serializedSettings = File.ReadAllText(settingsFullPath);
                        if (!string.IsNullOrWhiteSpace(serializedSettings))
                        {
                            return JsonSerializer.Deserialize<AppSettingsData>(serializedSettings);
                        }
                    }
                }
                catch
                {
                    return null;
                }
                return null;
        }
        return null;
    }
}
