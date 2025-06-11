using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ParticleMover : MonoBehaviour
{
    public float speed = 5f;

    void Update()
    {
        transform.position += speed * Time.deltaTime * Vector3.left;
    }
}

