using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightDecay : MonoBehaviour {

    public Light light;

    public float decayRate;

    // Use this for initialization
    void Start () {
        light = GetComponent<Light>();
	}
	
	// Update is called once per frame
	void Update () {
        light.intensity -= decayRate;
	}
}
