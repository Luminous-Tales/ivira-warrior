using UnityEngine;

public class ObstacleMovement : MonoBehaviour
{
    public GameObject particlePrefab;

    private void OnDestroy()
    {
        if (GameManager.instance != null && GameManager.instance.isExiting)
        {
            return;
        }
        if (particlePrefab != null && Application.isPlaying)
        {
            Instantiate(particlePrefab, new Vector3(transform.position.x, transform.position.y + 2, -2), Quaternion.identity);
        }
    }

    void Update()
    {
        if (GameManager.instance.isGameOver) return;

        float currentSpeed = GameManager.instance.currentGameSpeed;
        transform.Translate(currentSpeed * Time.deltaTime * Vector2.left);
        if (transform.position.x < -15)
        {
            Destroy(gameObject);
        }
    }
}
