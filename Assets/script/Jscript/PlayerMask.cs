using UnityEngine;

public class PlayerMask : MonoBehaviour
{
    [Header("Water_Mask")]
    public bool hasMask = true;

    [HideInInspector]
    public bool isPressingE;

    private void Update()
    {
        // Input centralisé ici
        isPressingE = Input.GetKey(KeyCode.E);
    }
}