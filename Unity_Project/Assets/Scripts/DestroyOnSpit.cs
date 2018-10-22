using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnSpit : MonoBehaviour {

    public GameObject food;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer != 10)
        {
            Instantiate(food, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
