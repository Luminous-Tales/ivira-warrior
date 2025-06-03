using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[System.Serializable]
public class MarketItem
{
    public int id;
    public string name;
    public int price;
    public Sprite sprite;

    public MarketItem(int id, string name, int price, Sprite sprite)
    {
        this.id = id;
        this.name = name;
        this.price = price;
        this.sprite = sprite;
    }
}

public class MarketController : MonoBehaviour
{
    [Header("UI References")]
    public TextMeshProUGUI scoreText;
    public GameObject itemSlotPrefab;
    public Transform contentParent;

    [Header("Market Items")]
    public List<Sprite> itemSprites;

    private List<MarketItem> marketItems;
    private SaveData saveData;

    void Start()
    {
        InitializeMarket();
        RefreshUI();
    }

    void InitializeMarket()
    {
        marketItems = new List<MarketItem>
        {
            new (0, "glaive padrão", 0, itemSprites[0]),
            new(1, "Glaive", 1500, itemSprites[1]),
            new(2, "+1 Vida", 100, itemSprites[2]),
            new(3, "2X pontos", 5000, itemSprites[3])
        };
    }

    void RefreshUI()
    {
        saveData = SaveManager.GetData();

        // Atualiza pontuação
        if (scoreText != null)
            scoreText.text = $"Score: {saveData.currentScore}";

        // Limpa slots existentes
        foreach (Transform child in contentParent)
            Destroy(child.gameObject);

        // Cria slots para cada item
        foreach (var item in marketItems)
        {
            CreateItemSlot(item);
        }
    }

    void CreateItemSlot(MarketItem item)
    {
        GameObject slot = Instantiate(itemSlotPrefab, contentParent);
        ItemSlot itemSlot = slot.GetComponent<ItemSlot>();

        bool isLifeItem = item.name == "+1 Vida";
        bool canBuyMoreLives = SaveManager.GetExtraLives() < 5;

        bool isWeapon = item.name.Contains("Arma") || item.name.Contains("Glaive");
        bool isUnlocked = item.id == 0 || SaveManager.IsItemUnlocked(item.id);
        bool canAfford = saveData.currentScore >= item.price;

        bool canBuy = isLifeItem ? canAfford && canBuyMoreLives : !isUnlocked && canAfford;

        itemSlot.Setup(item, isUnlocked, canBuy, OnBuyItem);

    }

    void OnBuyItem(int itemId)
    {
        // Item inexistente (caso especial do botão de "remover" anterior)
        if (itemId == -1)
        {
            RefreshUI();
            return;
        }

        var item = marketItems.Find(x => x.id == itemId);
        if (item == null) return;

        // +1 Vida
        if (item.name == "+1 Vida")
        {
            if (SaveManager.TryBuyLife(item.price))
            {
                Debug.Log("+1 Vida comprada!");
                RefreshUI();
            }
            return;
        }

        // Armas (inclui a arma padrão id == 0)
        bool isWeapon = item.id == 0 || item.name.Contains("Glaive");

        if (isWeapon)
        {
            // Arma padrão nunca precisa ser comprada
            if (item.id == 0)
            {
                SaveManager.SetEquippedWeapon(0);
                Debug.Log("Arma padrão equipada.");
                RefreshUI();
                return;
            }

            if (!SaveManager.IsItemUnlocked(itemId))
            {
                if (SaveManager.TryBuyItem(itemId, item.price))
                {
                    SaveManager.SetEquippedWeapon(itemId);
                    Debug.Log("Arma comprada e equipada!");
                    RefreshUI();
                }
                else
                {
                    Debug.Log("Não foi possível comprar a arma.");
                }
            }
            else
            {
                SaveManager.SetEquippedWeapon(itemId);
                Debug.Log("Arma equipada!");
                RefreshUI();
            }
            return;
        }

        // Outros itens
        if (SaveManager.TryBuyItem(itemId, item.price))
        {
            Debug.Log($"Item {item.name} comprado com sucesso!");
            RefreshUI();
        }
        else
        {
            Debug.Log("Não foi possível comprar o item");
        }
    }


}