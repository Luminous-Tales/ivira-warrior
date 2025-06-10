using UnityEngine;

public class ParticleMover : MonoBehaviour
{
    public float speed = 5f;

    void Update()
    {
        if(GameManager.instance.isGameOver) return;
        transform.position += speed * Time.deltaTime * Vector3.left;
    }
}

