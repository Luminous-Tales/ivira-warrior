using System.Collections.Generic;
using UnityEngine;

// Interface opcional para que objetos possam "resetar" seu estado ao serem pegos da pool.
public interface IPooledObject
{
    void OnObjectSpawn();
}

public class ObjectPooler : MonoBehaviour
{
    public static ObjectPooler Instance;

    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }

    public List<Pool> pools;
    public Dictionary<string, Queue<GameObject>> poolDictionary;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();
            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }
            poolDictionary.Add(pool.tag, objectPool);
        }
    }

    // --- LÓGICA DE SPAWN CORRIGIDA ---
    public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("A pool com a tag " + tag + " não existe.");
            return null;
        }

        if (poolDictionary[tag].Count == 0)
        {
            Debug.LogWarning("A pool com a tag " + tag + " está vazia. Considere aumentar o tamanho inicial no Inspector.");
            return null;
        }

        // Pega o objeto do INÍCIO da fila.
        GameObject objectToSpawn = poolDictionary[tag].Dequeue();

        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;

        IPooledObject pooledObj = objectToSpawn.GetComponent<IPooledObject>();
        if (pooledObj != null)
        {
            pooledObj.OnObjectSpawn();
        }

        // O objeto agora está "em uso" e FORA da fila.
        return objectToSpawn;
    }

    // --- LÓGICA DE DEVOLUÇÃO CORRIGIDA ---
    public void ReturnToPool(string tag, GameObject objectToReturn)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("A pool com a tag " + tag + " não existe.");
            return;
        }

        // PASSO 1: Desativa o objeto para que ele suma da cena.
        objectToReturn.SetActive(false);
        // PASSO 2: Coloca o objeto desativado de volta no FINAL da fila.
        poolDictionary[tag].Enqueue(objectToReturn);
    }
}