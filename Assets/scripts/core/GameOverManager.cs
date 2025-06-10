using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public static GameOverManager Instance;
    public TextMeshProUGUI newRecordText;
    public TextMeshProUGUI text2x;
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
        yield return new WaitForSeconds(1f);
        Debug.Log(SaveManager.GetData().unlockedItems.Contains(3));
        if (SaveManager.GetData().unlockedItems.Contains(3))
        {
            text2x.gameObject.SetActive(true);
            finalScore *= 2;
            while (displayedScore < finalScore)
            {

                displayedScore += Mathf.CeilToInt(finalScore / 30f);
                if (displayedScore > finalScore)
                    displayedScore = (int)finalScore;

                if (totalScoreText != null)
                    totalScoreText.text = displayedScore.ToString();

                yield return new WaitForSeconds(0.02f);
            }
            NewRecord(displayedScore);
            Save(displayedScore);
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
        StopAllCoroutines();
        text2x.gameObject.SetActive(false);
        newRecordText.gameObject.SetActive(false);
        smoke = FindFirstObjectByType<ParticleSystem>();
        if (smoke != null)
            Destroy(smoke);
        Time.timeScale = 1f;

        SceneManager.LoadScene("menu");
    }
    private void OnDestroy()
    {
        smoke = FindFirstObjectByType<ParticleSystem>();
        if (smoke != null)
            DontDestroyOnLoad(smoke);
    }
    public void RestartGame()
    {
        StopAllCoroutines();
        text2x.gameObject.SetActive(false);
        newRecordText.gameObject.SetActive(false); 
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }
}
