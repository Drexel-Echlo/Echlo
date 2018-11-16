using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCWandering : MonoBehaviour
{

    public float wanderSpeed;

    public Transform[] patrolPoint;

    private int destPoint = 0;

    // Update is called once per frame
    public void Update()
    {
        if (patrolPoint.Length == 0)
            return;

        transform.LookAt(patrolPoint[destPoint].position);
        transform.Translate(Vector3.forward * wanderSpeed * Time.deltaTime);

        if (Vector3.Distance(patrolPoint[destPoint].position, transform.position) < 1.2f)
            destPoint = (Random.Range(0, patrolPoint.Length) + 1) % patrolPoint.Length;
    }
}

//    private void OnTriggerEnter(Collider other)
//    {
//        handleTrigger(other);
//    }

//    private void OnTriggerStay(Collider other)
//    {
//        handleTrigger(other);
//    }

//    IEnumerator Wandering(float moveAmountX, float moveAmountZ)
//    {
//        transform.LookAt(new Vector3(target.x, transform.position.y, target.z));

//        if (Vector3.Distance(target, transform.position) < 1.2f)
//        {
//            isAtTarget = true;

//            WaitForSeconds moveDelay = new WaitForSeconds(Random.Range(moveDelayMin, moveDelayMax));
//            yield return moveDelay;

//            target = new Vector3(target.x + moveAmountX, 0, target.z + moveAmountZ);

//            isAtTarget = false;
//        }
//        else
//        {
//            transform.Translate(Vector3.forward * wanderSpeed * Time.deltaTime);
//        }
//    }

//    public void handleTrigger(Collider other)
//    {
//        if (other.gameObject.layer.Equals(LayerMask.NameToLayer("Walls")) || other.gameObject.tag.Equals(LayerMask.NameToLayer("Home")))
//        {
//            target = getNewTarget();
//        }
//    }

//    public Vector3 getNewTarget()
//    {
//        float x, z, range;
//        x = transform.position.x;
//        z = transform.position.z;
//        range = 2.0f;
//        return new Vector3(Random.Range(x - range, x + range), 0, Random.Range(z - range, z + range));
//    }
//}
