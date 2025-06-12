using UnityEngine;

public class ObstacleMovement : MonoBehaviour
{
    public string obstacleTag;
    public string particleTag = "smoke";
/*    private void OnDestroy()
    {
        if (GameManager.instance != null && GameManager.instance.isExiting)
        {
            return;
        }
        if (particlePrefab != null && Application.isPlaying)
        {
            Instantiate(particlePrefab, new Vector3(transform.position.x, transform.position.y + 2, -2), Quaternion.identity);
        }
    }*/

    void Update()
    {
        if (GameManager.instance.isGameOver) return;

        float currentSpeed = GameManager.instance.currentGameSpeed;
        transform.Translate(currentSpeed * Time.deltaTime * Vector2.left);

        if (transform.position.x < -15)
        {
            // Em vez de Destroy(gameObject);
            ObjectPooler.Instance.ReturnToPool(obstacleTag, gameObject);
        }

    }
    // ESTE � O M�TODO CENTRAL DA CORRE��O
    public void ReturnObstacleToPool()
    {
        // VERIFICA��O DE SEGURAN�A:
        // Se o jogo j� acabou (isGameOver) ou se a pool n�o existe mais,
        // N�O tente criar uma nova part�cula.
        if (GameManager.instance != null && !GameManager.instance.isGameOver && ObjectPooler.Instance != null)
        {
            ObjectPooler.Instance.SpawnFromPool(particleTag, new(transform.position.x, transform.position.y + 2), Quaternion.identity);
        }

        // Tenta devolver este obst�culo para a pool se a pool existir.
        if (ObjectPooler.Instance != null)
        {
            ObjectPooler.Instance.ReturnToPool(obstacleTag, gameObject);
        }
        else
        {
            // Se a pool j� foi destru�da (no fim da cena), apenas desativa o objeto.
            gameObject.SetActive(false);
        }
    }
}
