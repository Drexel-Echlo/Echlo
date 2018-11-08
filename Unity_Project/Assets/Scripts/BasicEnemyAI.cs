using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyAI : MonoBehaviour {

    protected enum STATE { Wait, Follow, Trail };

    public float moveSpeed;

    public NPCWandering movement;

    protected GameObject player;
    protected STATE state = STATE.Wait;
    protected Vector3 target;
    protected float trailSpeed;

	// Use this for initialization
	void Start () {
        player = GameController.getMainPlayer();
        state = STATE.Wait;
    }

    // Update is called once per frame
    void Update () {
        if (state == STATE.Wait)
        {
            movement.Wander();
        }
        else if (state == STATE.Follow)
        {
            if (Vector3.Distance(target, transform.position) < 2.0f)
            {
                state = STATE.Trail;
                trailSpeed = moveSpeed;
            }
            else
            {
                transform.LookAt(new Vector3(target.x, transform.position.y, target.z));
                transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
            }
        } else if (state == STATE.Trail)
        {
            if (trailSpeed < 1)
            {
                state = STATE.Wait;
            } else
            {
                transform.Translate(Vector3.forward * trailSpeed * Time.deltaTime);
                trailSpeed -= (moveSpeed / 2.0f) * Time.deltaTime;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (state == STATE.Wait && other.gameObject.layer.Equals(LayerMask.NameToLayer("Light"))) {
            state = STATE.Follow;
            target = other.gameObject.GetComponent<PositionHolder>().position;
        } else if ((state == STATE.Follow || state == STATE.Trail) && other.gameObject.tag == "Home") {
            state = STATE.Wait;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        int layer = other.gameObject.layer;
        if (layer != LayerMask.NameToLayer("Enemy") && layer != LayerMask.NameToLayer("Stalker") && layer != LayerMask.NameToLayer("SonarWave") && layer != LayerMask.NameToLayer("EnemyWave"))
        {
            target = transform.position;
            state = STATE.Wait;
        }
    }
}
