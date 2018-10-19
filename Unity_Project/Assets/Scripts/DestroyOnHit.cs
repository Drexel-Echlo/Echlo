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
        if (collision.gameObject.layer == LayerMask.NameToLayer("Food"))
        {
            GameObject foodExplosionClone = Instantiate(foodExplosion, collisionPoint.position, transform.rotation);
            GameObject.Destroy(foodExplosionClone, explodeDecay);
        }
        else if (collision.gameObject.tag == "LightEmUp")
        {
            GameObject explosionClone = Instantiate(explosion, collisionPoint.position, transform.rotation);
            GameObject.Destroy(explosionClone, explodeDecay);
        }
        Destroy(this.gameObject);
    }
}
