using UnityEngine;

public class MGrappin_Joueur : MonoBehaviour
{
    // Références aux composants nécessaires du joueur
    //Rigidbody2D rb;
    LineRenderer lr;
    DistanceJoint2D dj;

    // Masque de couche pour les objets avec lesquels le grappin peut s'accrocher
    public LayerMask grappleLayer;

    // Etats du grappin
    private bool isGrappling;

    // Point d'accroche du grappin
    private Vector3 grapplePoint;

    void Start()
    {
        // Récupération des composants nécessaires
        //rb = GetComponent<Rigidbody2D>();
        lr = GetComponent<LineRenderer>();
        dj = GetComponent<DistanceJoint2D>();

        // On désactive le rendu de la corde et le point d'ancrage du grappin au départ
        lr.enabled = false;
        dj.enabled = false;
        //lr.positionCount = 2;
    }

    void Update()
    {
        // Quand on clique avec la souris, on tente de lancer le grappin
        if (Input.GetMouseButtonDown(0))
        {
            // Conversion de la position de la souris en coordonnées du monde
            grapplePoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            grapplePoint.z = 0; // On le met à 0 pour être sûr qu'il soit sur le même plan que le joueur car nous sommes en 2D

            // On vérifie si le point cliqué est sur un objet du layer "grappleLayer"
            if (Physics2D.OverlapCircle(grapplePoint, 0.1f, grappleLayer))
            {
                isGrappling = true;

                // activation du LineRenderer pour dessiner la corde du grappin
                lr.enabled = true;
                lr.SetPosition(0, transform.position);
                lr.SetPosition(1, grapplePoint);

                // activation du DistanceJoint2D pour connecter le joueur au point d'accroche
                dj.enabled = true;

                // Le joint s'accroche au point fixe d'accroche du monde
                dj.connectedAnchor = grapplePoint;

                // Distance initiale entre le joueur et le point d'accroche
                dj.distance = Vector2.Distance(transform.position, grapplePoint);
            }
        }

        // Quand on relâche le clic, on désactive le grappin
        if (Input.GetMouseButtonUp(0))
        {
            isGrappling = false;
            lr.enabled = false;
            dj.enabled = false;
        }

        // Mise à jour de la position du LineRenderer pour suivre le mouvement du joueur pendant que le grappin est actif
        if (isGrappling)
        {
            lr.SetPosition(0, transform.position);  // Position du joueur
            lr.SetPosition(1, grapplePoint);        // Position du point d'accroche
        }
    }
}
