using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class VolumeManager : MonoBehaviour
{
    [SerializeField] private JsonManager jsonManager;
    [SerializeField] private Slider volumeSlider, musicSlider;

    // Start Loader
    private void Awake()
    {
        StartCoroutine(LoadJson());
    }

    // Save settings
    public void SaveValue()
    {
        // Save slider-data in json-data
        jsonManager.volume = volumeSlider.value;
        jsonManager.music = musicSlider.value;
        
        // Save json
        jsonManager.Save();
    }

    private IEnumerator LoadJson()
    {
        // For Repeating Loader
        while(true)
        {
            // Load Json
            jsonManager.Load();
            
            //Set sliders values
            volumeSlider.value = jsonManager.volume;
            musicSlider.value = jsonManager.music;
            yield return null;
        }
    }
}
