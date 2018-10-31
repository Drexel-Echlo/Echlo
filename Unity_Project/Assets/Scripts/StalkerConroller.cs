using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StalkerConroller : MonoBehaviour {

    protected enum STATE { Wait, Follow };

    public float moveSpeed;
    public float randomMin;
    public float randomMax;

    public NPCWandering movement;

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
            movement.Wander(randomMin, randomMax);
        }
        else if (state == STATE.Follow)
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
        if (state == STATE.Wait && other.gameObject.layer.Equals(LayerMask.NameToLayer("Light")))
        {
            state = STATE.Follow;
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
