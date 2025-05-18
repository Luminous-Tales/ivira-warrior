using UnityEngine;
using UnityEngine.UI;

public class HealtBar : MonoBehaviour
{
    private PlayerController player;
    private Image barFill;
    private int maxHealth;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        barFill = GameObject.FindWithTag("barFill").GetComponent<Image>();
        maxHealth = player.health;
    }

    private void Update()
    {
        UpdateHealthBar();
    }

    void UpdateHealthBar()
    {
        float fill = Mathf.Clamp01((float)player.health / (float)maxHealth);
        barFill.fillAmount = fill;
    }
}
