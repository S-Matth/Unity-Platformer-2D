using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [Header("Inventaire")]
    public bool hasMask = true;

    [HideInInspector]
    public bool isPressingE;

    private void Update()
    {
        // Input centralisé ici
        isPressingE = Input.GetKey(KeyCode.E);
    }
}