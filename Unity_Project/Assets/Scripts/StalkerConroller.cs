using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StalkerConroller : MonoBehaviour
{

    protected enum STATE { Wait, Follow };

    public float moveSpeed;

    protected ShooterController shooter;

    protected GameObject player;
    protected STATE state = STATE.Wait;
    protected Vector3 target;
    public float maxSight = 10;

    private bool allowLight = true;
    public GameObject movingLight;
    public GameObject movingLightClone;
    public float lightFrequency = 1.1f;
    private SeenHolder seenHolder;

    // Use this for initialization
    void Start()
    {
        player = GameController.getMainPlayer();
        state = STATE.Wait;
        shooter = GetComponent<ShooterController>();
        seenHolder = GetComponent<SeenHolder>();
    }

    // Update is called once per frame
    void Update()
    {
        if (state == STATE.Wait)
        {
        }
        else if (state == STATE.Follow)
        {
            if (Vector3.Distance(target, transform.position) < 0.7f)
            {
                state = STATE.Wait;
            }
            else
            {
                transform.LookAt(new Vector3(target.x, transform.position.y, target.z));
                transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
                if (allowLight) // Light Trail
                {
                    StartCoroutine(LightTrail(lightFrequency));
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (state == STATE.Wait && other.gameObject.layer.Equals(LayerMask.NameToLayer("Light")))
        {
            state = STATE.Follow;
            target = player.transform.position;
            shooter.shootWave(transform.position);
        }
        else if (state == STATE.Follow && other.gameObject.tag == "Home")
        {
            state = STATE.Wait;
        }
    }


    private void OnCollisionEnter(Collision other)
    {
        int layer = other.gameObject.layer;
        string name = other.gameObject.name;

        if (other.gameObject.tag == "VocalCordWave")
        {
            StartCoroutine(StunDuration());
        }

        if (layer != LayerMask.NameToLayer("Enemy") && layer != LayerMask.NameToLayer("Stalker") && layer != LayerMask.NameToLayer("SonarWave") && layer != LayerMask.NameToLayer("EnemyWave"))
        {
            target = transform.position;
            state = STATE.Wait;
        }
        if (layer == LayerMask.NameToLayer("Enemy") && name.Equals("Snapper"))
        {
            state = STATE.Wait;
            Destroy(this.gameObject);
        }
        else if (name.Equals("Needle"))
        {
            state = STATE.Wait;
            Destroy(other.gameObject);
        }
    }

    public void setTarget(Vector3 position)
    {
        if (state == STATE.Follow && Vector3.Distance(position, transform.position) < maxSight && Random.Range(0, 100) < 33)
        {
            target = position;
            transform.LookAt(new Vector3(target.x, transform.position.y, target.z));
            shooter.shootWave(transform.position);
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

    IEnumerator StunDuration()
    {
        float currentSpeed = moveSpeed;

        moveSpeed = 0;
        GetComponent<Collider>().enabled = false;

        yield return new WaitForSeconds(2);

        moveSpeed = currentSpeed;
        GetComponent<Collider>().enabled = true;
    }
}
