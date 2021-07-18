using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class gameOverScreen : MonoBehaviour
{
    public Text pointsText;

    public void Setup(int score)
    {
        gameObject.SetActive(true);
        pointsText.text = score.ToString() + "Points";
    }

    public void RestartButton()
    {
        //SceneManager.LoadScene("");
    }

    public void ExitButton()
    {
       /// SceneManager.LoadScene("");
    }
}
