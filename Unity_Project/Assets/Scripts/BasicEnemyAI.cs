using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyAI : MonoBehaviour {

    protected enum STATE { Wait, Follow };

    public float moveSpeed;
    public float cooldown;

    protected GameObject player;
    protected STATE state = STATE.Wait;
    protected Vector3 target;

    [SerializeField]
    private bool canDash = true;

	// Use this for initialization
	void Start () {
        player = GameObject.FindWithTag("Player");
        state = STATE.Wait;
    }

    // Update is called once per frame
    void Update () {
        if (state == STATE.Wait)
        {
        }
        else if (state == STATE.Follow)
        {
            if (Vector3.Distance(target, transform.position) < 0.5f)
            {
                state = STATE.Wait;
            }
            else
            {
                StartCoroutine(ChasePlayer(cooldown));
            }
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        if (state == STATE.Wait && other.gameObject.layer.Equals(LayerMask.NameToLayer("Light")) && canDash) {
            state = STATE.Follow;
            target = other.gameObject.GetComponent<PositionHolder>().position;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.layer != LayerMask.NameToLayer("Enemy"))
        {
            target = transform.position;
            state = STATE.Wait;
        }
    }

    IEnumerator ChasePlayer(float cooldown)
    {
        canDash = false;

        transform.LookAt(new Vector3(target.x, transform.position.y, target.z));
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);

        WaitForSeconds delay = new WaitForSeconds(cooldown);
        yield return delay;

        canDash = true;
    }
}
