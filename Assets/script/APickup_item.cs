using UnityEngine;

public class PickupItem : MonoBehaviour
{
    public string itemName;
    private bool playerInRange = false;

    public GameObject pressEUI; // UI "PRESS E"

    void Start()
    {
        if (pressEUI != null)
            pressEUI.SetActive(false);
    }

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            Inventory.instance.AddItem(itemName);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = true;
            if (pressEUI != null)
                pressEUI.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = false;
            if (pressEUI != null)
                pressEUI.SetActive(false);
        }
    }
}