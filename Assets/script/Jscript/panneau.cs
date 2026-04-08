 using UnityEngine;
using TMPro;

public class Panneau : MonoBehaviour
{
    public GameObject panelTexte;
    public TextMeshProUGUI texteUI;

    [TextArea]
    public string message;

    private bool joueurProche = false;

    void Start()
    {
        panelTexte.SetActive(false);
        texteUI.text = message;
    }

    void Update()
    {
        if (joueurProche && Input.GetKeyDown(KeyCode.E))
        {
            panelTexte.SetActive(!panelTexte.activeSelf);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            joueurProche = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            joueurProche = false;
            panelTexte.SetActive(false);
        }
    }
}