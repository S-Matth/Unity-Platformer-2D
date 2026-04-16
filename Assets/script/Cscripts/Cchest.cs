using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

/*
    ajouter ce script dans le chest
    j'ai un chest dans le Cimport, vous pouvez l'utiliser si non il y a un dans Mimport aussi
*/
public class Cchest : MonoBehaviour
{
    public Animator anim;
    public bool isOpen = false;
    private bool ActPlayer = false;
    void Update()
    {
        OpenChest();

    }

    public void OpenChest()
    {
        if (ActPlayer && Input.GetKeyDown(KeyCode.E))
        {
            anim.SetTrigger("open");

            //isOpen = true;
            Debug.Log("isopen");
            StartCoroutine(OpenAfterDelay());
        }
    }

    IEnumerator OpenAfterDelay()
    {
        yield return new WaitForSeconds(2f);
        isOpen = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ActPlayer = true;
        }
    }
}