using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterController : MonoBehaviour
{

    public GameObject wave;
    public GameObject waveClone;
    public Transform shooter;

    public float shotPower;
    public float fireRate;
    public float destroyTime;

    private bool allowFire = true;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && (allowFire))
        {
            StartCoroutine(WaveFire(fireRate));
        }
        else
        {
            return;
        }
    }

    IEnumerator WaveFire(float fireRate)
    {
        allowFire = false;

        waveClone = Instantiate(wave, shooter.position, shooter.rotation) as GameObject;
        waveClone.GetComponent<Rigidbody>().AddForce(transform.right * shotPower);
        GameObject.Destroy(waveClone, destroyTime);

        WaitForSeconds delay = new WaitForSeconds(fireRate);
        yield return delay;

        allowFire = true;
    }
}
