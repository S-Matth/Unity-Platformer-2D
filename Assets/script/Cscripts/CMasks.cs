using UnityEngine;
using UnityEngine.InputSystem;

/*  
    ajouter ce script dans le mask. Le mask doit etre un enfant du player
    quand tout le monde aura son mask je modifierait le script pour qu'il appelle le bon mask dans la bonne scene
*/

public class Masks : MonoBehaviour
{
    public SpriteRenderer sp;
    public Cchest chest;    // appeler ici car le mask n'est pas un enfant de chest
    public playerController player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sp = GetComponent<SpriteRenderer>();
        player = GetComponentInParent<playerController>();
    }

    // Update is called once per frame
    void Update()
    {
        activeMAsk();
        // suit les mouvement du player
        sp.flipX = player.sp.flipX;
    }

    private void activeMAsk()
    {
        // verifie si le coffre existe et qu'il a ete ouvert 
        if (chest != null && chest.isOpen)
        {
            sp.enabled = true;
        }
        else
        {
            sp.enabled = false;
        }
    }
}
