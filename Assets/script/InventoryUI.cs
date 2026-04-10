using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class ItemUI
{
    public string itemName;
    public Sprite icon;
}

public class InventoryUI : MonoBehaviour
{
    [Header("Références")]
    public GameObject inventoryPanel;
    public Image[] slots;
    public ItemUI[] itemDatabase;

    private bool isOpen = false;

    void Start()
    {
        inventoryPanel.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            isOpen = !isOpen;
            inventoryPanel.SetActive(isOpen);

            if (isOpen)
                UpdateUI();
        }
    }

    // ✅ Appelable depuis dehors (ex: quand un item est ramassé)
    public void UpdateUI()
    {
        if (Inventory.instance == null)
        {
            Debug.LogError("Inventory.instance est null !");
            return;
        }

        for (int i = 0; i < slots.Length; i++)
        {
            string item = Inventory.instance.items[i];

            // ✅ IsNullOrEmpty, compatible avec string[]
            if (!string.IsNullOrEmpty(item))
            {
                slots[i].sprite = GetSprite(item);
                slots[i].color = Color.white;
            }
            else
            {
                slots[i].sprite = null;
                slots[i].color = new Color(1, 1, 1, 0);
            }
        }
    }

    Sprite GetSprite(string itemName)
    {
        foreach (ItemUI item in itemDatabase)
        {
            if (item.itemName == itemName)
                return item.icon;
        }
        Debug.LogWarning($"Sprite introuvable pour : {itemName}");
        return null;
    }
}