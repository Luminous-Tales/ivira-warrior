using System.Collections.Generic;
using TMPro;
using UnityEngine;

[System.Serializable]
public class MarketItem
{
    public int image;
    public int price;
    public string description;
    public bool unlocked;
}

public class MarketController : MonoBehaviour
{
    public TextMeshProUGUI score;
    public GameObject itemSlotPrefab;
    public Transform contentParent;
    public List<Sprite> itemSprites;

    private SaveData data;

    void Start()
    {
        
        data = SaveManager.Load();
        if (data.market == null || data.market.Count == 0)
        {
            // inicializa market com base em sprites
            data.market = new List<MarketItem>();
            for (int i = 0; i < itemSprites.Count; i++)
            {
                data.market.Add(new MarketItem { image = i, price = 100 * (i + 1), description = "Item " + i, unlocked = false });
            }
            SaveManager.Save(data);
        }
        GerarLoja();
    }

    private void Update()
    {
        score.text = SaveManager.Load().scoreTotal.ToString();
    }
    void GerarLoja()
    {
        foreach (Transform t in contentParent) Destroy(t.gameObject);
        data = SaveManager.Load();
        for (int i = 0; i < data.market.Count; i++)
        {
            var item = data.market[i];
            GameObject slot = Instantiate(itemSlotPrefab, contentParent);
            var script = slot.GetComponent<ItemSlot>();
            Sprite sprite = (item.image >= 0 && item.image < itemSprites.Count)
                            ? itemSprites[item.image]
                            : null;
            script.Setup(item, sprite, OnBuyItem);
        }
    }

    void OnBuyItem(int id)
    {
        var item = data.market[id];
        bool success = SaveManager.ComprarArma(id, item.price);
        if (success)
        {
            // Atualiza UI
            GerarLoja();
        }
        else
            Debug.LogWarning("Compra falhou para item " + id);
    }
}