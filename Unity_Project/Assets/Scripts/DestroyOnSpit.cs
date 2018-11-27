using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnSpit : MonoBehaviour {

    public GameObject food;

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(this.gameObject);

        if (collision.gameObject.layer != LayerMask.NameToLayer("Enemy"))
        {
            Instantiate(food, transform.position, Quaternion.identity);
        }        
    }
}
