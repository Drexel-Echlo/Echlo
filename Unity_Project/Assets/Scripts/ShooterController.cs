using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterController : MonoBehaviour { 

    public GameObject wave;
    public GameObject waveClone;

    public GameObject foodSpit;

    public GameObject food;

    public Transform shooter;

    public AudioSource sendOut;

    public float shotPower;
    public float fireRate;
    public float fireDelay;
    public float destroyTime;



    private bool allowSpitFire = true;

    public void shootWave(Vector3 position)
    {
        if (fireDelay == 0){
            sendOut.Play();
            Quaternion rotation = Quaternion.Euler(-90, -90, 0);
            waveClone = Instantiate(wave, shooter.position, shooter.rotation * rotation) as GameObject;
            waveClone.GetComponent<Rigidbody>().AddForce(transform.forward * shotPower);

            waveClone.AddComponent(typeof(PositionHolder));
            waveClone.GetComponent<PositionHolder>().position = position;

            fireDelay++;
        }
        else if (fireDelay == fireRate){
            fireDelay = 0;
        }
        else
        {
            fireDelay++;
        }

    }

    public void spitFood()
    {
        if (allowSpitFire)
        {
            StartCoroutine(SpitFire(fireRate));
        }
    }

    IEnumerator SpitFire(float fireRate)
    {
        allowSpitFire = false;

        GameObject foodSpitClone = Instantiate(foodSpit, shooter.position, Quaternion.identity);
        foodSpitClone.GetComponent<Rigidbody>().AddForce(transform.forward * (shotPower));

        WaitForSeconds delay = new WaitForSeconds(fireRate);
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
