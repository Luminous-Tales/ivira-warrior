using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemSlot : MonoBehaviour
{
    [Header("UI Components")]
    public Image iconImage;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI descriptionText;
    public TextMeshProUGUI priceText;
    public Button buyButton;

    private int itemId;

    public void Setup(MarketItem item, bool isUnlocked, bool canAfford, System.Action<int> onBuyAction)
    {
        itemId = item.id;

        if (iconImage != null) iconImage.sprite = item.sprite;
        if (nameText != null) nameText.text = item.name;
        if (priceText != null) priceText.text = item.price.ToString();

        if (buyButton != null)
        {
            buyButton.onClick.RemoveAllListeners();
            buyButton.onClick.AddListener(() => onBuyAction(itemId));

            var buttonText = buyButton.GetComponentInChildren<TextMeshProUGUI>();

            // CASO SEJA UMA ARMA EQUIPÁVEL
            bool isWeapon = item.id == 0 || item.id == 1;

            if (isWeapon && isUnlocked)
            {
                int equipped = SaveManager.GetEquippedWeapon();
                buttonText = buyButton.GetComponentInChildren<TextMeshProUGUI>();

                if (equipped == item.id)
                {
                    buyButton.interactable = false;
                    buttonText.text = "EQUIPADA";
                }
                else
                {
                    buyButton.interactable = true;
                    buttonText.text = "EQUIPAR";
                    buyButton.onClick.RemoveAllListeners();
                    buyButton.onClick.AddListener(() => onBuyAction(itemId));
                }
                return;
            }

            // ITENS COMUNS
            buyButton.interactable = !isUnlocked && canAfford;

            if (buttonText != null)
            {
                if (isUnlocked) buttonText.text = "OWNED";
                else if (!canAfford) buttonText.text = item.price.ToString();
                else buttonText.text = item.price.ToString();
            }
        }
    }

}