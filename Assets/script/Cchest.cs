using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

/*
    ajouter ce script dans le chest
    j'ai un chest dans le Cimport, vous pouvez l'utiliser si non il y a un dans Mimport aussi
*/
public class Cchest : MonoBehaviour
{
    public Animator anim;
    public bool isOpen = false;
    private bool ActPlayer = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
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

