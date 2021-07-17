using UnityEngine;

public class VolumeController : MonoBehaviour
{
    [SerializeField] private JsonManager jsonManager;
    [SerializeField] private AudioSource sound;

    void Awake()
    {
        jsonManager.Load();
    }

    void Start()
    {
        sound.volume = jsonManager.volume * jsonManager.music;
    }
}
