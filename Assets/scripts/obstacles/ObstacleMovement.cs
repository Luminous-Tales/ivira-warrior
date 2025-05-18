using UnityEngine;

public class ObstacleMovement : MonoBehaviour
{
    public GameObject particlePrefab;

    private void OnDestroy()
    {
        if (particlePrefab != null && Application.isPlaying)
        {
            GameObject particleInstance = Instantiate(particlePrefab, new Vector3(transform.position.x, transform.position.y + 2, -2), Quaternion.identity);
            Destroy(particleInstance, 2f);
        }
    }

    void Update()
    {
        if (GameManager.Instance.isGameOver) return;

        transform.position += Time.deltaTime * VelocityController.Instance.CurrentSpeed * Vector3.left;

        if (transform.position.x < -20f)
        {
            Destroy(gameObject);
        }
    }
}
