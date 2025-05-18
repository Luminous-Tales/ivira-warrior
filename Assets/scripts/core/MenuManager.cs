using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private Canvas creditsCanvas;
    [SerializeField] private Canvas optionsCanvas;


    public void ShowCredits()
    {
        creditsCanvas.gameObject.SetActive(true);
    }

    public void HideCredits()
    {
        creditsCanvas.gameObject.SetActive(false);
    }

    public void ShowOptions()
    {
        optionsCanvas.gameObject.SetActive(true);
    }

    public void HideOptions()
    {

        optionsCanvas.gameObject.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit Game");
    }

    public void StartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }
}
