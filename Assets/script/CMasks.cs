using UnityEditorInternal;
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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sp = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        SwitchSens();
        activeMAsk();
    }

    // change e sens de la masque
    private void SwitchSens()
    {
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.Q))
        {
            sp.flipX = true;
        }
        else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            sp.flipX = false;
        }
    }
    
    private void activeMAsk()
    {
        
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
