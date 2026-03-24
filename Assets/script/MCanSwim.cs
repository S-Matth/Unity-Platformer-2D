using UnityEngine;

public class MCanSwim : MonoBehaviour
{
    // Variable SerializeField (actualisable dans l'inspector) 
    [SerializeField] private LayerMask lmWater;
    [SerializeField] private float swimSpeed = 2f;
    [SerializeField] private bool canSwim;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!canSwim)
        {
            PlayerRespawn respawn = GetComponentInParent<PlayerRespawn>();
        }
        
    }
}
