using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightDecay : MonoBehaviour {

    public Light lit;

    public float decayRate;

    public AudioSource sfxSource;

    public AudioClip sfxClip;

    // Use this for initialization
    void Start () {
        lit = GetComponent<Light>();
	}
	
	// Update is called once per frame
	void Update () {
        lit.intensity -= decayRate;
        sfxSource.PlayOneShot(sfxClip);
    }
}
