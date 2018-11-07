using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnHit : MonoBehaviour
{
    //public GameObject explosion;
    public GameObject foodExplosion;
    public GameObject enemyExplosion;

    public GameObject wallExplosionBlue;
    public GameObject wallExplosionYel;
    public GameObject wallExplosionPink;

    public Transform collisionPoint;

    public float explodeDecay;

    private void OnCollisionEnter(Collision collision)
    {
        GameObject explosionClone = null;
        if (collision.gameObject.layer == LayerMask.NameToLayer("Food"))
        {
            explosionClone = Instantiate(foodExplosion, collisionPoint.position, transform.rotation);
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            explosionClone = Instantiate(enemyExplosion, collisionPoint.position, transform.rotation);
        }
        else if (collision.gameObject.tag == "LightEmUp")
        {
            float rdm = Random.Range(0.0f, 5.0f);
            if (rdm <= 1.0f)
            {
                explosionClone = Instantiate(wallExplosionBlue, collisionPoint.position, transform.rotation);
            }
            else if (1.0f <= rdm && rdm <= 3.0f)
            {
                explosionClone = Instantiate(wallExplosionYel, collisionPoint.position, transform.rotation);
            }
            else if (rdm > 3.0f)
            {
                explosionClone = Instantiate(wallExplosionPink, collisionPoint.position, transform.rotation);
            }


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
