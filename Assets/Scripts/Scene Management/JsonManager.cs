using System.IO;
using UnityEngine;
using Structs;

public class JsonManager : MonoBehaviour
{
    // Settings
    [SerializeField] private SettingsInfo settingsInfo;
    public float volume, music;

    //Json config
    [SerializeField] private string path;
    [SerializeField] private string saveFileName;
    
    // Save Json
    public void Save()
    {
        // Set path to save
        path = Path.Combine(Application.dataPath + "/Info", saveFileName);
        
        // Set data
        settingsInfo = new SettingsInfo
        {
            volume = volume,
            music = music
        };
        
        // Converting data to JSON
        string json = JsonUtility.ToJson(settingsInfo, true);
        
        // Save json-file
        File.WriteAllText(path, json);
    }
    
    // load Json
    public void Load()
    {
        // Set path to load
        path = Path.Combine(Application.dataPath + "/Info", saveFileName);

        // Get json-file as string
        string json = File.ReadAllText(path);

        // Converting string to data
        SettingsInfo settings = JsonUtility.FromJson<SettingsInfo>(json);
        volume = settings.volume;
        music = settings.music;
    }
}
