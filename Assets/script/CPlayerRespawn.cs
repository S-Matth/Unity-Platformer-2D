using UnityEngine;

/*
 Ajouter ce script dans le Player, il va controler la position du MainRespawn et update chaque nouveau checkPoint
*/


public class PlayerRespawn : MonoBehaviour
{
    private Vector3 CheckPointPosition;
    private Vector3 MainCheckPoint;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CheckPointPosition = transform.position;
        MainCheckPoint = transform.position;
    }
    
    // mise a jour du checkpoint
    public void SetCheckPoint(Vector3 newPosition)
    {
        CheckPointPosition = newPosition;
        Debug.Log("newpos");
    }

    // reaparait a cette position apres la void
    public void Respawn()
    {
        transform.position = CheckPointPosition;
        Debug.Log("Spawn");
    }

    // sauvegarde le spawn qu'il apparait la premiere fois et où il apparaitra quand il n'aura plus de vie
    public void MainRespawn()
    {
        transform.position = MainCheckPoint;
        Debug.Log("Mainpos");
        
    }
}
