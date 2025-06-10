using System;
using UnityEngine;

public class ParallaxScroll : MonoBehaviour
{
    [Range(0f, 1f)]
    public float depthFactor = 0.5f;
    public float scrollSpeed;

    private MeshRenderer quadRenderer;
    private float currentOffset;

    void Start()
    {
        quadRenderer = GetComponent<MeshRenderer>();
        currentOffset = 0f;
    }

    private void Update()
    {
        if (GameManager.instance.isGameOver) return;

        currentOffset += (GameManager.instance.currentGameSpeed * scrollSpeed) * Time.deltaTime;
        currentOffset %= 1f;
        Vector2 newOffset = new(currentOffset, 0);
        quadRenderer.material.mainTextureOffset = newOffset;
    }
}
