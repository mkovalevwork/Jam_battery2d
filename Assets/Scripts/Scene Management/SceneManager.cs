using UnityEngine;

public class SceneManager : MonoBehaviour
{
    public void SceneLoad(string scene)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(scene);
    }
}
