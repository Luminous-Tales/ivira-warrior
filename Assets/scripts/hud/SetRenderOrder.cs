using UnityEngine;

// Coloque este script no seu Quad
[RequireComponent(typeof(Renderer))]
public class SetRenderOrder : MonoBehaviour
{
    [Tooltip("Nome do Sorting Layer que deve ser usado. Deve ser igual ao criado em 'Project Settings'.")]
    public string sortingLayerName = "Default";

    [Tooltip("Ordem dentro do Sorting Layer. Valores maiores ficam mais na frente.")]
    public int orderInLayer = 0;

    void Awake()
    {
        // Pega o Renderer do objeto (seja MeshRenderer ou outro)
        Renderer objectRenderer = GetComponent<Renderer>();

        // Define o Sorting Layer e a Ordem
        objectRenderer.sortingLayerName = sortingLayerName;
        objectRenderer.sortingOrder = orderInLayer;
    }

    // OnValidate � chamado no editor sempre que o script � carregado ou um valor � alterado.
    // Isso � �til para ver o efeito no modo de edi��o sem precisar dar play.
    void OnValidate()
    {
        Renderer objectRenderer = GetComponent<Renderer>();
        objectRenderer.sortingLayerName = sortingLayerName;
        objectRenderer.sortingOrder = orderInLayer;
    }
}