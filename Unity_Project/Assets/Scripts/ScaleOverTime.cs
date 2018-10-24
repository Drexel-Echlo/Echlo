using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleOverTime : MonoBehaviour {

    public float scaleFactor;

    public bool isShrinking;

	// Update is called once per frame
	void Update () {

        var scaleX = transform.localScale.x;
        var scaleY = transform.localScale.y;
        var scaleZ = transform.localScale.z;

        if (isShrinking && scaleX >= 0 && scaleY >= 0 && scaleZ >= 0)
        {
            this.transform.localScale -= new Vector3(Time.deltaTime / scaleFactor, Time.deltaTime / scaleFactor, 0);
        }
        else if (!isShrinking && scaleX >= 0 && scaleY >= 0 && scaleZ >= 0)
        {
            this.transform.localScale += new Vector3(Time.deltaTime / scaleFactor, Time.deltaTime / scaleFactor, 0);
        }
	}
}
