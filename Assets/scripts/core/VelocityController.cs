using System.Collections;
using TMPro;
using UnityEngine;

public class VelocityController : MonoBehaviour
{
    public static VelocityController Instance;

    [Header("Configurações de Velocidade")]
    public float baseSpeed = 5f;
    public float maxSpeed = 20f;
    public float accelerationRate = 0.1f;
    public float CurrentSpeed { get; set; }

    [Header("Referências")]
    public TextMeshProUGUI textMeters;

    private float totalDistance;
    private Coroutine boostCoroutine;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        CurrentSpeed = baseSpeed;
    }

    private void Update()
    {
        if (GameManager.Instance != null && !GameManager.Instance.isGameOver)
        {
            UpdateDistance();
        }
    }

    private void Accelerate()
    {
        CurrentSpeed = Mathf.Min(CurrentSpeed + accelerationRate * Time.deltaTime, maxSpeed);
    }

    private void UpdateDistance()
    {
        totalDistance += CurrentSpeed * Time.deltaTime;
        if (textMeters != null)
            textMeters.text = Mathf.FloorToInt(totalDistance).ToString();
    }

    public void ResetSpeed()
    {
        CurrentSpeed = baseSpeed;
    }

    public void MultiplySpeed(float multiplier, float duration)
    {
        if (boostCoroutine != null)
            StopCoroutine(boostCoroutine);

        boostCoroutine = StartCoroutine(TemporaryBoost(multiplier, duration));
    }

    private IEnumerator TemporaryBoost(float multiplier, float duration)
    {
        float originalSpeed = CurrentSpeed;
        CurrentSpeed = Mathf.Clamp(CurrentSpeed * multiplier, baseSpeed, maxSpeed);

        yield return new WaitForSeconds(duration);

        CurrentSpeed = Mathf.Clamp(originalSpeed, baseSpeed, maxSpeed);
        boostCoroutine = null;
    }

    public int GetDistance()
    {
        return Mathf.FloorToInt(totalDistance);
    }
}
