using TMPro;
using UnityEngine;

public class PointsManager : MonoBehaviour
{
    public static PointsManager instance;

    private int pointsJump = 100;
    private int pointsDodge = 150;
    private int score;
    private TextMeshProUGUI scoreText;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        GameObject[] scoreTexts = GameObject.FindGameObjectsWithTag("scoreText");
        if (scoreTexts.Length > 0)
            scoreText = scoreTexts[0].GetComponent<TextMeshProUGUI>();
        else
            Debug.LogWarning("Nenhum objeto com tag 'scoreText' encontrado!");
    }

    public void AddScore(int points)
    {
        int previousScore = score;
        score += points;
        UpdateScoreUI();
    }

    private void UpdateScoreUI()
    {
        if (scoreText != null)
            scoreText.text = score.ToString();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Point") && !gameObject.CompareTag("PlayerHurt"))
        {
            PointType pointType = collision.GetComponent<PointType>();
            if (pointType != null)
            {
                switch (pointType.scoreType)
                {
                    case ScoreType.Dodge:
                        AddScore(pointsDodge);
                        break;
                    case ScoreType.Jump:
                        AddScore(pointsJump);
                        break;
                }
            }
        }
    }

    public void ResetPoints()
    {
        score = 0;
        UpdateScoreUI();
    }

    public int GetPoints()
    {
        return Mathf.FloorToInt(score);
    }
}
