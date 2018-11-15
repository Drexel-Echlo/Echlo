using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapperController : MonoBehaviour {
    protected enum STATE { Wait, Attack };

    public float moveSpeed;

    public AudioSource sfxSource;

    public AudioClip[] sfxClips;

    protected GameObject player;
    protected STATE state = STATE.Wait;
    protected Vector3 target;

    // Use this for initialization
    void Start()
    {
        player = GameController.getMainPlayer();
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

                sfxSource.PlayOneShot(sfxClips[0]);
                transform.LookAt(new Vector3(target.x, transform.position.y, target.z));
                transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        string tag = other.gameObject.tag;
        int layer = other.gameObject.layer;
        if (state == STATE.Wait && (tag == "Player" || layer == LayerMask.NameToLayer("Stalker") || layer == LayerMask.NameToLayer("Enemy")))
        {
            state = STATE.Attack;
            target = other.transform.position;
        } else if (state == STATE.Attack  && tag == "Home")
        {
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
