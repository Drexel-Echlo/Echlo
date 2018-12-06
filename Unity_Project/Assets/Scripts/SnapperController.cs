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
        switch (this.state)
        {
            case STATE.Wait:
                break;

            case STATE.Attack:
                sfxSource.PlayOneShot(sfxClips[0]);

                transform.LookAt(new Vector3(target.x, transform.position.y, target.z));
                transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);

                break;
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
        }
        else if (state == STATE.Attack  && tag == "Home")
        {
            state = STATE.Wait;
        }

    }


    private void OnCollisionEnter(Collision other)
    {
        int layer = other.gameObject.layer;

        if (other.gameObject.tag == "VocalCordWave")
        {
            StartCoroutine(StunDuration());
        }

        if (layer != LayerMask.NameToLayer("Enemy") && layer != LayerMask.NameToLayer("Stalker") && layer != LayerMask.NameToLayer("SonarWave") && layer != LayerMask.NameToLayer("EnemyWave"))
        {
            target = transform.position;
            state = STATE.Wait;
        }
    }

    IEnumerator StunDuration()
    {
        float currentSpeed = moveSpeed;

        moveSpeed = 0;
        GetComponent<Collider>().enabled = false;

        yield return new WaitForSeconds(4);

        moveSpeed = currentSpeed;
        GetComponent<Collider>().enabled = true;
    }
}
