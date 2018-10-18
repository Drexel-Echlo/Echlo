using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveGrow : MonoBehaviour {
    
	// Update is called once per frame
	void Update () {
        this.transform.localScale += new Vector3(Time.deltaTime / 2, Time.deltaTime / 2, 0);
	}
}
