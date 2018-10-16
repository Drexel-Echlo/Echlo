using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnHit : MonoBehaviour
{
    public GameObject explosion;

    public float explodeDecay;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "LightEmUp")
        {
            GameObject explosionClone = Instantiate(explosion, transform.position, transform.rotation);
            GameObject.Destroy(explosionClone, explodeDecay);
            Destroy(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
