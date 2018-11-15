using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterController : MonoBehaviour { 

    public GameObject wave;
    public GameObject waveClone;

    public GameObject foodSpit;

    public GameObject food;

    public Transform shooter;

    public AudioSource sfxSource;

    public AudioClip[] sfxClips;

    public float shotPower;
    public float fireRate;
    public float foodFireRate;
    public float fireDelay = -1;
    public float destroyTime;

    public float minPitch;
    public float maxPitch;

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
        if (fireDelay == 0){

            sfxSource.pitch = Random.Range(minPitch, maxPitch);

            sfxSource.PlayOneShot(sfxClips[0]);

            Quaternion rotation = Quaternion.Euler(-90, -90, 0);
            waveClone = Instantiate(wave, shooter.position, shooter.rotation * rotation) as GameObject;
            waveClone.GetComponent<Rigidbody>().AddForce(transform.forward * shotPower);

            waveClone.AddComponent(typeof(PositionHolder));
            waveClone.GetComponent<PositionHolder>().position = position;
        }
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
