using UnityEngine;
using UnityEngine.UIElements;

public class JennemiMove : MonoBehaviour
{
    public float speed;
    public Transform[] waypoint;

    public SpriteRenderer graphics;
    private Transform target;
    private int destpoint;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        target = waypoint[0];
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

        // si l'ennemi est quasi arrivé a destination
        if (Vector3.Distance(transform.position, target.position) < 0.3f)
        {
            destpoint = (destpoint + 1) % waypoint.Length;
            target = waypoint[destpoint];
            graphics.flipX = !graphics.flipX;
        }
    }
}

