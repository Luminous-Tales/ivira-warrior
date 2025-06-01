using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private VelocityController velocityController;
    public bool isGameOver = false;
    private int fps = 30;
    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void GameOver()
    {
        isGameOver = true;
        VelocityController.Instance.CurrentSpeed = 0f;
        GameOverManager.Instance.ShowGameOver();
    }

    public IEnumerator SlowDown()
    {
        while (velocityController.CurrentSpeed > 0)
        {
            velocityController.CurrentSpeed -= velocityController.accelerationRate * 2 * Time.deltaTime;
            yield return null;
        }
        velocityController.CurrentSpeed = 0;
    }

    public void AlternateFPS()
    {
        if (fps == 30)
        {
            Application.targetFrameRate = 60;
        }
        else
        {
            Application.targetFrameRate = 30;
        }
    }
}

