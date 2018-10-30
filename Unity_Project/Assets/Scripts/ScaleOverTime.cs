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

        if (scaleX >= 0 && scaleY >= 0 && scaleZ >= 0)
        {
            Vector3 deltaScale = new Vector3(Time.deltaTime / scaleFactor, Time.deltaTime / scaleFactor, 0);
            if (isShrinking)
            {
                this.transform.localScale -= deltaScale;

            }
            else
            {
                this.transform.localScale += deltaScale;

            }
        }
	}
}
