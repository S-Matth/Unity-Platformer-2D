using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class MFalling_bridge : MonoBehaviour
{
    // Delai avant que le pont ne tombe après que le joueur soit dessus
    [SerializeField] private float fallDelay = 1f;
    // Delai avant que le pont ne soit détruit après être tombé
    [SerializeField] private float destroyDelay = 2f;

    private bool isFalling = false;

    [SerializeField] private Rigidbody2D rb;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Si le pont est déjà en train de tomber, on ne fait rien
        if (isFalling) return;

        // Si le joueur entre en collision avec le pont, on commence le processus de chute
        if (collision.transform.tag == "Player") StartCoroutine(StartFall());
    }

    private IEnumerator StartFall()
    {
        // On marque le pont comme étant en train de tomber pour éviter de déclencher plusieurs fois la chute
        isFalling = true;

        // On attend le délai avant de faire tomber le pont
        yield return new WaitForSeconds(fallDelay);

        // On rend le pont dynamique pour qu'il puisse tomber sous l'effet de la gravité
        rb.bodyType = RigidbodyType2D.Dynamic;

        // On attend le délai avant de détruire le pont
        Destroy(gameObject, destroyDelay); 
    }
}
