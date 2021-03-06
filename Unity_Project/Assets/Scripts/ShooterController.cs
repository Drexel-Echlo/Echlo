﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterController : MonoBehaviour { 

    public GameObject wave;
    private GameObject waveClone;

    public GameObject vocalCordWave;
    private GameObject vocalCordWaveClone;

    public GameObject foodSpit;

    public GameObject food;

    public Transform shooter;

    public float shotPower;
    public float fireRate;
    public float foodFireRate;
    public float fireDelay = -1;
    public float destroyTime;

    private bool allowSpitFire = true;

    public void Update()
    {
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            fireDelay = -1;
        }
    }

    public void shootWave(Vector3 position)
    {
        if (fireDelay >= fireRate)
        {
            fireDelay = 0;
        }
        else
        {
            fireDelay++;
        }
        if (fireDelay == 0)
        {
            Quaternion rotation = Quaternion.Euler(90, 0, 0);
            waveClone = Instantiate(wave, shooter.position, shooter.rotation * rotation) as GameObject;
            waveClone.GetComponent<Rigidbody>().AddForce(transform.forward * shotPower);

            waveClone.AddComponent(typeof(PositionHolder));
            waveClone.GetComponent<PositionHolder>().position = position;
        }
    }

    public void shootPowerWave(Vector3 position)
    {
        Quaternion rotation = Quaternion.Euler(90, 0, 0);
        vocalCordWaveClone = Instantiate(vocalCordWave, shooter.position, shooter.rotation * rotation) as GameObject;
        vocalCordWaveClone.GetComponent<Rigidbody>().AddForce(transform.forward * shotPower * 1.5f);

        vocalCordWaveClone.AddComponent(typeof(PositionHolder));
        vocalCordWaveClone.GetComponent<PositionHolder>().position = position;
    }

    public bool spitFood()
    {
        bool rv = allowSpitFire;
        if (rv)
        {
            StartCoroutine(SpitFire(fireRate));
        }
        return rv;
    }

    IEnumerator SpitFire(float fireRate)
    {
        allowSpitFire = false;

        GameObject foodSpitClone = Instantiate(foodSpit, shooter.position, Quaternion.identity);
        foodSpitClone.GetComponent<Rigidbody>().AddForce(transform.forward * (shotPower));

        WaitForSeconds delay = new WaitForSeconds(foodFireRate);
        yield return delay;

        allowSpitFire = true;
       
        WaitForSeconds spawnDelay = new WaitForSeconds(destroyTime);
        yield return spawnDelay;

        if (foodSpitClone != null)
        {
            Instantiate(food, foodSpitClone.transform.position, Quaternion.identity);
            GameObject.Destroy(foodSpitClone, .1f);
        }
    }
}
