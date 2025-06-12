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
    // ESTE É O MÉTODO CENTRAL DA CORREÇÃO
    public void ReturnObstacleToPool()
    {
        // VERIFICAÇÃO DE SEGURANÇA:
        // Se o jogo já acabou (isGameOver) ou se a pool não existe mais,
        // NÃO tente criar uma nova partícula.
        if (GameManager.instance != null && !GameManager.instance.isGameOver && ObjectPooler.Instance != null)
        {
            ObjectPooler.Instance.SpawnFromPool(particleTag, new(transform.position.x, transform.position.y + 2), Quaternion.identity);
        }

        // Tenta devolver este obstáculo para a pool se a pool existir.
        if (ObjectPooler.Instance != null)
        {
            ObjectPooler.Instance.ReturnToPool(obstacleTag, gameObject);
        }
        else
        {
            // Se a pool já foi destruída (no fim da cena), apenas desativa o objeto.
            gameObject.SetActive(false);
        }
    }
}
