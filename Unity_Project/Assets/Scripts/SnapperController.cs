using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapperController : MonoBehaviour {
    protected enum STATE { Wait, Attack };

    public float moveSpeed;
    public float attackRange;

    protected GameObject player;
    protected STATE state = STATE.Wait;
    protected Vector3 target;

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        state = STATE.Wait;
    }

    // Update is called once per frame
    void Update()
    {
        if (state == STATE.Wait)
        {
        }
        else if (state == STATE.Attack)
        {
            if (Vector3.Distance(target, transform.position) < 1.0f)
            {
                state = STATE.Wait;
            }
            else
            {
                transform.LookAt(new Vector3(target.x, transform.position.y, target.z));
                transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (state == STATE.Wait && other.gameObject.layer.Equals(LayerMask.NameToLayer("Light")) && Vector3.Distance(transform.position, player.transform.position) < attackRange)
        {
            state = STATE.Attack;
            target = player.transform.position;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.layer != LayerMask.NameToLayer("Enemy") && other.gameObject.layer != LayerMask.NameToLayer("SonarWave"))
        {
            target = transform.position;
            state = STATE.Wait;
        }
    }
}
