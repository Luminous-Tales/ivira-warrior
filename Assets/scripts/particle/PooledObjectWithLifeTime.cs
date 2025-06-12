using UnityEngine;

public class PooledObjectWithLifetime : MonoBehaviour, IPooledObject
{
    [Header("Configura��o da Pool")]
    [Tooltip("A tag deste objeto na pool (ex: smoke_particle)")]
    public string poolTag;

    [Header("Comportamento")]
    [Tooltip("Tempo em segundos que este objeto ficar� ativo")]
    public float lifeTime = 2f;

    [Tooltip("Velocidade de movimento para a esquerda")]
    public float moveSpeed = 5f;

    private float timer;

    // Este m�todo � da interface IPooledObject.
    // Ele � chamado pelo ObjectPooler toda vez que o objeto � ativado.
    public void OnObjectSpawn()
    {
        // Reinicia o contador de tempo de vida sempre que o objeto � reutilizado.
        timer = lifeTime;
    }

    void Update()
    {
        // 1. Movimenta o objeto
        transform.position += Vector3.left * moveSpeed * Time.deltaTime;

        // 2. Controla o tempo de vida
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                // O tempo acabou! Devolve o objeto para a pool.
                ObjectPooler.Instance.ReturnToPool(poolTag, gameObject);
            }
        }
    }
}