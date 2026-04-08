using UnityEngine;


using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public Image[] slots; // les 3 slots UI
    public Sprite defaultSprite; // sprite vide

    void Update()
    {
        UpdateUI();
    }

    void UpdateUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (Inventory.instance.items[i] != null)
            {
                slots[i].sprite = GetSpriteFromItem(Inventory.instance.items[i]);
                slots[i].color = Color.white;
            }
            else
            {
                slots[i].sprite = defaultSprite;
                slots[i].color = new Color(1, 1, 1, 0); // invisible
            }
        }
    }

    Sprite GetSpriteFromItem(string itemName)
    {
        // IMPORTANT: le sprite doit être dans Resources
        return Resources.Load<Sprite>(itemName);
    }
}
