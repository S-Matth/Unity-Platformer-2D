using UnityEngine;

/*
   script que pour le PLayer de Matthias 
*/

public class CPlayerRespawnMatthias : MonoBehaviour
{

    void Awake()
    {
        if (PlayerPrefs.GetInt("hasCheckpoint", 0) == 1)
        {
            float x = PlayerPrefs.GetFloat("checkpointX");
            float y = PlayerPrefs.GetFloat("checkpointY");

            transform.position = new Vector2(x, y);
        }
    }

}
