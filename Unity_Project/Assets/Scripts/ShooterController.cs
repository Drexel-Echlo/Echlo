using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterController : MonoBehaviour { 

    public PlayerController player;

    public GameObject wave;
    public GameObject waveClone;

    public GameObject foodSpit;
    public GameObject foodSpitClone;

    public GameObject food;

    public Transform shooter;

    public AudioSource sendOut;

    public float shotPower;
    public float fireRate;
    public float destroyTime;

    private bool allowWaveFire = true;
    private bool allowSpitFire = true;
    
    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && allowWaveFire)
        {
            sendOut.Play();
            StartCoroutine(WaveFire(fireRate));
        }

        if (Input.GetKeyDown(KeyCode.Mouse1) && allowSpitFire && player.carryCount != 0 /*&& !player.isHome*/)
        {
            StartCoroutine(SpitFire(fireRate));
        }
    }

    IEnumerator WaveFire(float fireRate)
    {
        allowWaveFire = false;

        waveClone = Instantiate(wave, shooter.position, shooter.rotation);
        waveClone.GetComponent<Rigidbody>().AddForce(transform.forward * shotPower);
        waveClone.AddComponent(typeof(PositionHolder));
        waveClone.GetComponent<PositionHolder>().position = player.transform.position;
        GameObject.Destroy(waveClone, destroyTime);

        WaitForSeconds delay = new WaitForSeconds(fireRate);
        yield return delay;

        allowWaveFire = true;
    }

    IEnumerator SpitFire(float fireRate)
    {
        player.carryCount--;
        allowSpitFire = false;

        foodSpitClone = Instantiate(foodSpit, shooter.position, Quaternion.identity) as GameObject;
        foodSpitClone.GetComponent<Rigidbody>().AddForce(transform.forward * (shotPower));
        GameObject.Destroy(foodSpitClone, destroyTime);

        WaitForSeconds delay = new WaitForSeconds(fireRate);
        yield return delay;

        allowSpitFire = true;

        WaitForSeconds delay02 = new WaitForSeconds(fireRate -.05f);
        yield return delay02;

        Instantiate(food, foodSpitClone.transform.position, Quaternion.identity);
    }
}
