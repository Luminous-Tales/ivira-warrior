using System;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class ParallaxScroll : MonoBehaviour
{
    [Range(0f, 1f)]
    public float depthFactor = 0.5f;

    private MeshRenderer meshRenderer;

    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    private void Update()
    {
        if (VelocityController.Instance == null || GameManager.Instance.isGameOver) return;

        float playerSpeed = VelocityController.Instance.CurrentSpeed;
        float parallaxSpeed = (playerSpeed * depthFactor) / 25;

        Vector2 offset = meshRenderer.material.mainTextureOffset;
        offset.x += parallaxSpeed * Time.deltaTime;
        meshRenderer.material.mainTextureOffset = offset;
    }
}
