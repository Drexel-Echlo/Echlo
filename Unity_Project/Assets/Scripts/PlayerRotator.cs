using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotator : MonoBehaviour {

	private Camera main;

	void Start () {
		main = FindObjectOfType<Camera> ();
	}
		
	void Update () {
		Ray cameraRay = main.ScreenPointToRay (Input.mousePosition);
		Plane gound = new Plane (Vector3.up, Vector3.zero);
		float rayLength;

		if (gound.Raycast (cameraRay, out rayLength)) {
			Vector3 point = cameraRay.GetPoint (rayLength);

			//Debug.DrawLine (cameraRay.origin, point, Color.blue);

			//Vector3 vec = new Vector3 (point.x - transform.position.x, 0, point.z - transform.position.z);
			float angle = Mathf.Atan2 (point.x - transform.position.x, point.z - transform.position.z) * Mathf.Rad2Deg;

			Debug.Log (angle);

			this.gameObject.transform.eulerAngles = new Vector3 (0, angle,transform.rotation.y);
		}
	}
}
