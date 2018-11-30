using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public GameObject player;

    public float cameraSmoothing;

    private Vector3 offset;

    void Start()
    {
        this.offset = this.transform.position - this.player.transform.position;
    }

    void LateUpdate()
    {
        this.transform.position = Vector3.Lerp(this.transform.position, this.player.transform.position + this.offset, Time.deltaTime * cameraSmoothing);
    }
}
