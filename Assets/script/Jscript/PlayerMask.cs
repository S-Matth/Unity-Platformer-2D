using NUnit.Framework;
using UnityEngine;

public class PlayerMask : MonoBehaviour
{
    [HideInInspector]
    public bool isPressingE;

    [Header("Masques possédés")]
    public bool hasWaterMask;
    public bool hasGrapinMask;
    public bool hasDashMask;


    private void Update()
    {
        isPressingE = Input.GetKey(KeyCode.E);

        // Met à jour les masques disponibles en fonction de l'inventaire
        HasMaskInInventory();
    }

    // Méthode pour vérifier la présence des masques dans l'inventaire et mettre à jour les variables correspondantes
    private void HasMaskInInventory()
    { 
        if (Inventory.instance == null) return;

        string[] items = Inventory.instance.items;

        hasDashMask = Contains(items, "Dash_Mask");
        hasWaterMask = Contains(items, "Water_Mask");
        hasGrapinMask = Contains(items, "Grapin_Mask");
    }

    // Méthode utilitaire pour vérifier la présence d'une valeur dans un tableau de chaînes
    private bool Contains(string[] array, string value)
    {
        foreach (string item in array)
        {
            if (item == value)
                return true;
        }
        return false;
    }

    // Méthode de test pour vérifier la présence d'un masque dans l'inventaire
    private bool HasMask(string maskName)
    {
        return Contains(Inventory.instance.items, maskName);
    }
}