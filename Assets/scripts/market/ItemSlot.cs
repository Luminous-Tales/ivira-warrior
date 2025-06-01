using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemSlot : MonoBehaviour
{
    public Image icon;
    public TextMeshProUGUI descriptionText;
    public TextMeshProUGUI priceText;
    public Button buyButton;

    private int itemId;

    public void Setup(MarketItem item, Sprite sprite, System.Action<int> onBuy)
    {
        itemId = item.image;
        icon.sprite = sprite;
        descriptionText.text = item.description;
        priceText.text = item.price.ToString();
        buyButton.interactable = !item.unlocked;
        buyButton.onClick.RemoveAllListeners();
        buyButton.onClick.AddListener(() => onBuy(itemId));
    }
}