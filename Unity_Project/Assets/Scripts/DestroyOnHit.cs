using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnHit : MonoBehaviour
{
    public GameObject explosion;
    public GameObject foodExplosion;
    public GameObject enemyExplosion;
    public bool isStalker = false;
    public Transform collisionPoint;

    public float explodeDecay;

    private void OnCollisionEnter(Collision collision)
    {
        GameObject explosionClone = null;
        if (collision.gameObject.layer == LayerMask.NameToLayer("Food"))
        {
            explosionClone = Instantiate(foodExplosion, collisionPoint.position, transform.rotation);
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy") || (!isStalker && collision.gameObject.layer == LayerMask.NameToLayer("Stalker")))
        {
            explosionClone = Instantiate(enemyExplosion, collisionPoint.position, transform.rotation);
        }
        else if ((collision.gameObject.tag == "LightEmUp" && (!isStalker || collision.gameObject.layer != LayerMask.NameToLayer("Stalker"))) || (isStalker && collision.gameObject.layer == LayerMask.NameToLayer("Player")))
        {
            explosionClone = Instantiate(explosion, collisionPoint.position, transform.rotation);
        }
        if (explosionClone != null)
        {
            GameObject.Destroy(explosionClone, explodeDecay);
            explosionClone.AddComponent(typeof(PositionHolder));
            explosionClone.GetComponent<PositionHolder>().position = GetComponent<PositionHolder>().position;
        }
        Destroy(this.gameObject);
    }
}
