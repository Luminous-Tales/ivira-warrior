using UnityEngine;

public class HitboxAttack : MonoBehaviour
{
    private int pointsAttack = 100;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("breakableObstacle"))
        {
            PointsManager.instance.AddScore(pointsAttack);
            ObstacleMovement obstacle = collision.GetComponent<ObstacleMovement>();
            if (obstacle != null)
            {
                // Perfeito! Ele chama o nosso método seguro.
                obstacle.ReturnObstacleToPool();
            }
        }
    }
}
