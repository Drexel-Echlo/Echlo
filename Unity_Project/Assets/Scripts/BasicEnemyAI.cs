using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyAI : MonoBehaviour {

    protected enum STATE { Wait, Follow, Trail };

    public float moveSpeed;

    protected GameObject player;
    protected STATE state = STATE.Wait;
    protected Vector3 target;
    protected float trailSpeed;

    private bool allowLight = true;
    public GameObject movingLight;
    public GameObject movingLightClone;
    public float lightFrequency = 1.1f;
    private SeenHolder seenHolder;

    public AudioSource sfxSource;

    public AudioClip sfxClip;

    // Use this for initialization
    void Start () {
        player = GameController.getMainPlayer();
        state = STATE.Wait;
        seenHolder = GetComponent<SeenHolder>();
    }

    // Update is called once per frame
    void Update () {
        if (state == STATE.Wait)
        {
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
                sfxSource.PlayOneShot(sfxClip);
                if (allowLight) // Light Trail
                {
                    StartCoroutine(LightTrail(lightFrequency));
                }
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
                if (allowLight) // Light Trail
                {
                    StartCoroutine(LightTrail(lightFrequency));
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (state == STATE.Wait && other.gameObject.layer.Equals(LayerMask.NameToLayer("Light"))) {
            PositionHolder holder = other.gameObject.GetComponent<PositionHolder>();
            if (holder != null)
            {
                state = STATE.Follow;
                target = holder.position;
            } else
            {
                print("no holder");
            }
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
    IEnumerator LightTrail(float frequency)
    {
        if (seenHolder.seen)
        {
            allowLight = false;

            movingLightClone = Instantiate(movingLight, transform.position, transform.rotation);
            movingLightClone.AddComponent(typeof(PositionHolder));
            movingLightClone.GetComponent<PositionHolder>().position = transform.position;
            GameObject.Destroy(movingLightClone, 2.5f);

            WaitForSeconds delay = new WaitForSeconds(frequency);
            yield return delay;

            allowLight = true;
        }
    }
}
