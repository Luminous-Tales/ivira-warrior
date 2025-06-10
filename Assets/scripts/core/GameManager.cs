using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float DistanceInMeters { get; private set; }
    public bool IsPlayerRunningFast { get; private set; }
    public float speedToRunFast = 6f;

    public TMP_Text distanceText;
    public float initialSpeed = 5f;
    public float maxSpeed = 15f;
    public float speedIncreaseRate = 0.1f;

    public float currentGameSpeed;

    public static GameManager instance;

    public bool isGameOver = false;

    public float decelerationRate = 5f;
    private bool isDecelerating = false;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        currentGameSpeed = initialSpeed;
        DistanceInMeters = 0f;
        Time.timeScale = 1f;
    }

    void Update()
    {
        if (isGameOver) return;

        if (isDecelerating)
        {
            currentGameSpeed = Mathf.MoveTowards(currentGameSpeed, initialSpeed, decelerationRate * Time.deltaTime);

            if (currentGameSpeed == initialSpeed)
            {
                isDecelerating = false;
            }
        }
        else if (currentGameSpeed < maxSpeed)
        {
            currentGameSpeed += speedIncreaseRate * Time.deltaTime;
        }
        IsPlayerRunningFast = (currentGameSpeed >= speedToRunFast);
        DistanceInMeters += currentGameSpeed * Time.deltaTime;
        if (distanceText != null)
        {
            distanceText.text = DistanceInMeters.ToString("F0");
        }
    }
    public void GameOver()
    {
        isGameOver = true;
        GameOverManager.Instance.ShowGameOver();
    }

    public void ResetSpeedOnDamage()
    {
        isDecelerating = true;
    }
}

