using System.Collections;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject[] obstaclePrefabs;
    public Transform spawnPoint;

    private void Start()
    {
        StartCoroutine(SpawnObstaclesLoop());
    }

    IEnumerator SpawnObstaclesLoop()
    {
        while (!GameManager.Instance.isGameOver)
        {
            float tempoDeJogo = VelocityController.Instance.CurrentSpeed;
            float dificuldadePorTempo = Mathf.Clamp01(tempoDeJogo / 120f);
            float baseRate = Mathf.Lerp(2.5f, 4.5f, dificuldadePorTempo);
            float velocidadeInfluence = Mathf.Max(0.8f, 1f - VelocityController.Instance.CurrentSpeed * 0.1f);
            float spawnRate = baseRate + velocidadeInfluence;

            yield return new WaitForSeconds(spawnRate);

            if (obstaclePrefabs.Length > 0)
            {
                int randomIndex = Random.Range(0, obstaclePrefabs.Length);
                GameObject newObstacle = Instantiate(obstaclePrefabs[randomIndex], spawnPoint.position, Quaternion.identity);
                Destroy(newObstacle, 10f);
            }
        }
    }
}
