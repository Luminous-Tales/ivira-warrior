using UnityEngine;

public class MenuManager : MonoBehaviour
{

    public void QuitGame()
    {
        Application.Quit();
    }

    public void StartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }
}
