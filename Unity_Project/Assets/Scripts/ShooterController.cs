using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterController : MonoBehaviour { 

    public PlayerController player;

    public GameObject wave;
    public GameObject waveClone;

    public GameObject foodSpit;

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

        if (Input.GetKeyDown(KeyCode.Mouse1) && allowSpitFire && player.carryCount != 0 && !player.isHome)
        {
            StartCoroutine(SpitFire(fireRate));
        }
    }

    IEnumerator WaveFire(float fireRate)
    {
        allowWaveFire = false;

        waveClone = Instantiate(wave, shooter.position, shooter.rotation) as GameObject;
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

        GameObject foodSpitClone = Instantiate(foodSpit, shooter.position, Quaternion.identity);
        foodSpitClone.GetComponent<Rigidbody>().AddForce(transform.forward * (shotPower));

        WaitForSeconds spawnDelay = new WaitForSeconds(destroyTime - .05f);
        yield return spawnDelay;

        Instantiate(food, foodSpitClone.transform.position, Quaternion.identity);

        WaitForSeconds delay = new WaitForSeconds(fireRate / 100);
        yield return delay;
        
        allowSpitFire = true;
    }
}
