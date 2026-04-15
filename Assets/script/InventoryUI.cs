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

    [Header("Slot par défaut")]
    public Sprite defaultSlotSprite; //  image ici dans l'Inspector

    private bool isOpen = false;

    void Start()
    {
        inventoryPanel.SetActive(true);//inventaire allumer par defaut 
        ApplyDefaultSprites(); //  Appliquer image par défaut au démarrage
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

    void ApplyDefaultSprites()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].sprite = defaultSlotSprite;
            slots[i].color = Color.white;
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

            if (!string.IsNullOrEmpty(item))
            {
                Sprite icon = GetSprite(item);
                if (icon != null)
                {
                    slots[i].sprite = icon; // Affiche l'icône de l'item
                    slots[i].color = Color.white;
                }
                else
                {
                    slots[i].sprite = defaultSlotSprite; // fallback si sprite manquant
                    slots[i].color = Color.white;
                }
            }
            else
            {
                slots[i].sprite = defaultSlotSprite; //  Slot vide = image par défaut
                slots[i].color = Color.white;
            }
        }
    }

    
    Sprite GetSprite(string itemName)
    {
        Debug.Log("Recherche sprite pour : '" + itemName + "' - DB size: " + itemDatabase.Length);
        foreach (ItemUI item in itemDatabase)
        {
            Debug.Log("Comparaison : '" + item.itemName + "' == '" + itemName + "' ? " + (item.itemName == itemName));
            if (item.itemName == itemName)
                return item.icon;
        }
        Debug.LogWarning($"Sprite introuvable pour : {itemName}");
        return null;
    }
}