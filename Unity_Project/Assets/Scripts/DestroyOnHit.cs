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
        int layer = collision.gameObject.layer;
        string tag = collision.gameObject.tag;
        GameObject explosionClone = null;
        GameObject explosion = null;


        if (layer == LayerMask.NameToLayer("Food"))
        {

            explosion = foodExplosion;
            
        }
        else if (layer == LayerMask.NameToLayer("Enemy") || layer == LayerMask.NameToLayer("Stalker"))
        {
            explosion = enemyExplosion;
            SeenHolder seenHolder = collision.gameObject.GetComponent<SeenHolder>();
            if (seenHolder != null)
            {
                seenHolder.seen = true;
            }
        }
        else if (tag == "Player")
        {

            explosion = wallExplosionBlue;
           
        }
        else if (tag == "LightEmUp")
        {

            float rdm = Random.Range(0.0f, 5.0f);



            if (rdm <= 1.0f)
            {
                explosion = wallExplosionBlue;
            }
            else if (1.0f <= rdm && rdm <= 3.0f)
            {
                explosion = wallExplosionYel;
            }
            else if (rdm > 3.0f)
            {
                explosion = wallExplosionPink;
            }
        }

        if(explosion != null)
        {
            explosionClone = Instantiate(explosion, collisionPoint.position, transform.rotation);
            GameObject.Destroy(explosionClone, explodeDecay);
            explosionClone.AddComponent(typeof(PositionHolder));
            explosionClone.GetComponent<PositionHolder>().position = GetComponent<PositionHolder>().position;
        }

        Destroy(this.gameObject);
    }
}
