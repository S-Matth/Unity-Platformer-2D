using UnityEngine;

public class PlayerMask : MonoBehaviour
{
    [HideInInspector]
    public bool isPressingE;

    // Calculé automatiquement depuis l'inventaire
    public bool hasMask => HasMaskInInventory();

    private void Update()
    {
        isPressingE = Input.GetKey(KeyCode.E);
    }

    private bool HasMaskInInventory()
    {
        if (Inventory.instance == null) return false;

        foreach (string item in Inventory.instance.items)
        {
            if (item == "Mask_Water") return true;
        }
        return false;
    }
}