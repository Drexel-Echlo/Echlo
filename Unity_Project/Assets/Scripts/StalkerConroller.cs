﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StalkerConroller : MonoBehaviour {

    protected enum STATE { Wait, Follow };

    public float moveSpeed;

    public NPCWandering movement;
    protected ShooterController shooter;

    protected GameObject player;
    protected STATE state = STATE.Wait;
    protected Vector3 target;

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        state = STATE.Wait;
        shooter = GetComponent<ShooterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (state == STATE.Wait)
        {
            movement.Wander();
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
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer.Equals(LayerMask.NameToLayer("Light")))
        {
            state = STATE.Follow;
            target = player.transform.position;
            shooter.shootWave(transform.position);
        }
    }


    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.layer != LayerMask.NameToLayer("Enemy") && other.gameObject.layer != LayerMask.NameToLayer("Stalker") && other.gameObject.layer != LayerMask.NameToLayer("SonarWave") && other.gameObject.layer != LayerMask.NameToLayer("EnemyWave"))
        {
            target = transform.position;
            state = STATE.Wait;
        }
    }

    public void setTarget(Vector3 position)
    {
        if (state == STATE.Follow)
        {
            print("Shoot");
            target = position;
            transform.LookAt(new Vector3(target.x, transform.position.y, target.z));
            shooter.shootWave(transform.position);
        }

    }
}
