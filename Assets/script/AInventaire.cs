using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;
    public string[] items = new string[3];

    void Awake()
    {
        if (instance == null)
            instance = this;
    }

    public void AddItem(string itemName)
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (string.IsNullOrEmpty(items[i]))
            {
                items[i] = itemName;
                Debug.Log(itemName + " ajouté au slot " + i);

                InventoryUI ui = FindFirstObjectByType<InventoryUI>(FindObjectsInactive.Include);
                Debug.Log("InventoryUI trouvé : " + (ui != null)); //  vérifie si trouvé
                if (ui != null)
                    ui.UpdateUI();

                EquipItem(itemName);
                return;
            }
        }
        Debug.Log("Inventaire plein !");
    }
    void EquipItem(string itemName)
    {
        Debug.Log("Item équipé : " + itemName);
    }
}