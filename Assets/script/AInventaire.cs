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
            // string.IsNullOrEmpty au lieu de == null
            if (string.IsNullOrEmpty(items[i]))
            {
                items[i] = itemName;
                Debug.Log(itemName + " ajouté au slot " + i);
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