using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DEBUG_player : MonoBehaviour {

	public float speed;

	void FixedUpdate() {
		/*
		if (Input.GetKey (KeyCode.W)) {
			gameObject.transform.position += Vector3.up * speed;
		} else if (Input.GetKey (KeyCode.S)) {
			gameObject.transform.position += Vector3.down * speed;
		}
		if (Input.GetKey (KeyCode.A)) {
			gameObject.transform.position += Vector3.left * speed;
		} else if (Input.GetKey (KeyCode.D)) {
			gameObject.transform.position += Vector3.right * speed;
		}
		if (Input.GetKey (KeyCode.Q)) {
			gameObject.transform.position += Vector3.forward * speed;
		} else if (Input.GetKey (KeyCode.E)) {
			gameObject.transform.position += Vector3.back * speed;
		}
		*/
		if (Input.GetKey (KeyCode.A)) {
			gameObject.GetComponent<Rigidbody> ().velocity += Vector3.left * speed;
		} else if (Input.GetKey (KeyCode.D)) {
			gameObject.GetComponent<Rigidbody> ().velocity += Vector3.right * speed;
		}

	}
}
