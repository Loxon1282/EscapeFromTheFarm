using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DEBUG_player : MonoBehaviour {

	public float speed;
	public float jumpVelocity;

	public GameObject camera;
	public Vector3 offset;

	void Awake() {
		offset = new Vector3 (camera.transform.position.x - gameObject.transform.position.x,
			                 camera.transform.position.y,
			camera.transform.position.z);
	}

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
		if (Input.GetKeyDown (KeyCode.Space)) {
			gameObject.GetComponent<Rigidbody> ().velocity += Vector3.up * jumpVelocity;
		}
	}
	void Update() {
		camera.transform.position = new Vector3 (gameObject.transform.position.x, gameObject.transform.position.y, 0) + offset;
	}
}
