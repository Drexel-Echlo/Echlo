using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCWandering : MonoBehaviour {

    public float moveDelayMin;
    public float moveDelayMax;

    public float wanderSpeed;

    private bool isAtTarget;

    Vector3 target;

    private void Start()
    {
        target = getNewTarget();
    }

    // Update is called once per frame
    public void Wander(float randomMin, float randomMax)
    {
        float wanderX = Random.Range(randomMin, randomMax);
        float wanderZ = Random.Range(randomMin, randomMax);

        if (!isAtTarget)
        {
            StartCoroutine(Wandering(wanderX, wanderZ));
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer.Equals(LayerMask.NameToLayer("Walls")))
        {
            target = getNewTarget();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer.Equals(LayerMask.NameToLayer("Walls")))
        {
            target = getNewTarget();
        }
    }

    IEnumerator Wandering(float moveAmountX, float moveAmountZ)
    {
        transform.LookAt(new Vector3(target.x, transform.position.y, target.z));

        if (Vector3.Distance(target, transform.position) < .5f)
        {
            isAtTarget = true;

            WaitForSeconds moveDelay = new WaitForSeconds(Random.Range(moveDelayMin, moveDelayMax));
            yield return moveDelay;

            target = new Vector3(target.x + moveAmountX, 0, target.z + moveAmountZ);

            isAtTarget = false;
        }
        else
        {
            transform.Translate(Vector3.forward * wanderSpeed * Time.deltaTime);
        }
    }

    public Vector3 getNewTarget()
    {
        float x, y, z, range;
        x = transform.position.x;
        z = transform.position.z;
        range = 2.0f;
        return new Vector3(Random.Range(x - range, x + range), 0, Random.Range(z - range, z + range));
    }
}
