using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class gameOverScreen : MonoBehaviour
{
    

    public void ShowEndScreen()
    {
        gameObject.SetActive(true);
        
    }

    public void RestartButton()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainGameScene");
    }

    public void ExitButton()
    {
       
    }
}
