using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnHit : MonoBehaviour
{
    public GameObject explosion;
    public GameObject foodExplosion;

    public Transform collisionPoint;

    public float explodeDecay;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 11)
        {
            GameObject foodExplosionClone = Instantiate(foodExplosion, collisionPoint.position, transform.rotation);
            GameObject.Destroy(foodExplosionClone, explodeDecay);
            Destroy(this.gameObject);
        }
        else if (collision.gameObject.tag == "LightEmUp")
        {
            GameObject explosionClone = Instantiate(explosion, collisionPoint.position, transform.rotation);
            GameObject.Destroy(explosionClone, explodeDecay);
            Destroy(this.gameObject);
        }
        
        else
        {
            Destroy(this.gameObject);
        }
    }
}
