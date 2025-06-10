using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public static GameOverManager Instance;
    public TextMeshProUGUI newRecordText;
    public GameObject gameOverPanel;
    public GameObject hudPanel;
    public GameObject uiButtons;
    public TextMeshProUGUI metersText;
    public TextMeshProUGUI pointsText;
    public TextMeshProUGUI totalScoreText;
    private ParticleSystem smoke;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void ShowGameOver()
    {
        gameOverPanel.SetActive(true);


        int metersReached = (int)GameManager.instance.DistanceInMeters;
        int points = PointsManager.instance.GetPoints();
        float total = metersReached + points;
        NewRecord(points);
        metersText.text = metersReached.ToString();
        pointsText.text = points.ToString();
        uiButtons.SetActive(false);
        hudPanel.SetActive(false);
        Save(points);
        StartCoroutine(AnimateScore(total));
    }

    private void Save(int point)
    {
        SaveManager.SaveScore(point);
    }

    IEnumerator AnimateScore(float finalScore)
    {
        int displayedScore = 0;

        while (displayedScore < finalScore)
        {

            displayedScore += Mathf.CeilToInt(finalScore / 30f);
            if (displayedScore > finalScore)
                displayedScore = (int)finalScore;

            if (totalScoreText != null)
                totalScoreText.text = displayedScore.ToString();

            yield return new WaitForSeconds(0.02f);
        }
    }

    public void NewRecord(int score)
    {
        if (SaveManager.GetData().bestScore <= score)
        {
            newRecordText.gameObject.SetActive(true);
        }
    }

    public void ReturnMenu()
    {
        newRecordText.gameObject.SetActive(false);
        smoke = FindFirstObjectByType<ParticleSystem>();
        if (smoke != null) 
            Destroy(smoke);
        Time.timeScale = 1f;
        SceneManager.LoadScene("menu");
    }

    public void RestartGame()
    {
        newRecordText.gameObject.SetActive(false);
        smoke = FindFirstObjectByType<ParticleSystem>();
        if (smoke != null)
            Destroy(smoke);
        Time.timeScale = 1f;
        StopAllCoroutines();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }
}
