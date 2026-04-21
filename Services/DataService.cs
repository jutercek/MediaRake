// Saves and loads AppData to disc

using Newtonsoft.Json;
using System.IO;
using MediaRake.Models;

namespace MediaRake.Services;

public class DataService
{
    private static readonly string DataFolder = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
        "MediaRake");

    private static readonly string DataFile = Path.Combine(DataFolder, "data.json");
    
    public void Save(AppData data)
    {
        Directory.CreateDirectory(DataFolder);
        var json = JsonConvert.SerializeObject(data, Formatting.Indented);
        File.WriteAllText(DataFile, json);
    }

    public AppData Load()
    {
        if (!File.Exists(DataFile))
            return new AppData();
        var json = File.ReadAllText(DataFile);
        return JsonConvert.DeserializeObject<AppData>(json) ?? new AppData();
    }
}