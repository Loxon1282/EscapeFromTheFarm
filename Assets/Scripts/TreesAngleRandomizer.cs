using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreesAngleRandomizer : MonoBehaviour {

	public bool freezeX, freezeY, freezeZ;

	void Start () {
		float rotX, rotY, rotZ;
		if (freezeX) {
			rotX = gameObject.transform.rotation.eulerAngles.x;
		} else {
			rotX = Random.Range (-2.0f, 2.0f) + gameObject.transform.rotation.eulerAngles.x;	
		}
		if (freezeY) {
			rotY = gameObject.transform.rotation.eulerAngles.y;
		} else {
			rotY = Random.Range (0, 180) + gameObject.transform.rotation.eulerAngles.y;	
		}
		if (freezeZ) {
			rotZ = gameObject.transform.rotation.eulerAngles.z;
		} else {
			rotZ = Random.Range (-2.0f, 2.0f) + gameObject.transform.rotation.eulerAngles.z;	
		}
		gameObject.transform.rotation = Quaternion.Euler( new Vector3(rotX, rotY, rotZ));
	}

}
