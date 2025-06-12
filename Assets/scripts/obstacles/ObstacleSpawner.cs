using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public List<string> obstacleTags;
    public float spawnInterval = 2f;
    private float timer;

/*    private void Start()
    {
        StartCoroutine(SpawnObstaclesLoop());
    }*/
    void Update()
    {
        if (GameManager.instance.isGameOver) return;

        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            SpawnObstacle();
            timer = 0;
        }
    }

    void SpawnObstacle()
    {
        // Sorteia uma tag da lista
        string randomTag = obstacleTags[Random.Range(0, obstacleTags.Count)];

        // Em vez de Instantiate(...)
        ObjectPooler.Instance.SpawnFromPool(randomTag, transform.position, Quaternion.identity);
    }



/*    IEnumerator SpawnObstaclesLoop()
    {
        while (!GameManager.instance.isGameOver)
        {
            float gameTime = GameManager.instance.currentGameSpeed;
            float dificultForTime = Mathf.Clamp01(gameTime / 120f);
            float baseRate = Mathf.Lerp(2.5f, 4.5f, dificultForTime);
            float velocityInfluence = Mathf.Max(0.8f, 1f - GameManager.instance.currentGameSpeed * 0.1f);
            float spawnRate = baseRate + velocityInfluence;

            yield return new WaitForSeconds(spawnRate);

            if (obstaclePrefabs.Length > 0)
            {
                int randomIndex = Random.Range(0, obstaclePrefabs.Length);
                GameObject newObstacle = Instantiate(obstaclePrefabs[randomIndex], spawnPoint.position, Quaternion.identity);
                Destroy(newObstacle, 10f);
            }
        }
    }*/
}
