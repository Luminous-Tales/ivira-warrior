using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private Image panelBlack;
    private Color color;
    private new AudioSource audio;
    public AudioClip buttonPlay;
    public AudioClip musicMenu;
    private void Awake()
    {
        audio = GetComponent<AudioSource>();
        color = panelBlack.color;
    }
    private void Start()
    {
        panelBlack.gameObject.SetActive(false);
    }
    public void QuitGame()
    {
        Application.Quit();
    }

    public void StartGame()
    {
        StartCoroutine(AnimateStartGame());
    }

    IEnumerator AnimateStartGame()
    {
        panelBlack.gameObject.SetActive(true);
        audio.clip = musicMenu;
        audio.Stop();
        audio.clip = buttonPlay;
        audio.Play();
        for (float i = 0; i <= 1.1; i += 0.01f )
        {
            color.a = i;
            panelBlack.color = color;
            yield return new WaitForSeconds(0.01f);
        }
        yield return new WaitForSeconds(0.5f);

        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }
    
}
