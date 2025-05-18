using UnityEngine;

public class ButtonsController : MonoBehaviour
{

    public GameObject canvaMenu;
    public GameObject canvaConfig;

    public static ButtonsController instance;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public void OpenMenu()
    {
        canvaMenu.SetActive(true);
        Time.timeScale = 0f;
    }
    public void CloseMenu()
    {
        canvaMenu.SetActive(false);
        Time.timeScale = 1f;
    }
    public void ShowOptions()
    {
        canvaConfig.SetActive(true);
    }

    public void HideOptions()
    {

        canvaConfig.SetActive(false);
    }
}
