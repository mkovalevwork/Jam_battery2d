using UnityEngine;

public class PauseManager : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    public bool isPause;
    private void Update()
    {
        // Open pause menu
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPause = !isPause;
        }
        
        // Show/Hide pause menu
        panel.SetActive(isPause);
    }

    public void Resume()
    {
        isPause = false;
    }

    public void Pause()
    {
        isPause = true;
    }
}
