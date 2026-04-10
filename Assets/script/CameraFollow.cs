using UnityEngine;

/*
 Forcer la Main Camera de la Main Scene a suivre le Player 
*/

public class CameraFollow : MonoBehaviour
{
    private Transform target;
    [SerializeField] private Vector3 offset = new Vector3(0, 0, -10f);

    void Start()
    {
        Debug.Log("[CameraFollow] Start");
        FindPlayer();
    }

    void OnEnable()
    {
        Debug.Log("[CameraFollow] OnEnable");
        FindPlayer();
    }

    void FindPlayer()
    {
        GameObject p = GameObject.FindGameObjectWithTag("Player");
        if (p != null)
        {
            target = p.transform;
            Debug.Log("[CameraFollow] Player trouvé : " + p.name);
        }
        else
        {
            Debug.LogWarning("[CameraFollow] Aucun Player trouvé avec le tag 'Player'");
        }
    }

    void LateUpdate()
    {
        if (target == null)
        {
            // On essaie de le retrouver si jamais on l’a perdu
            FindPlayer();
            return;
        }

        Vector3 desiredPos = target.position + offset;
        transform.position = desiredPos;
    }
}
