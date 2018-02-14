 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSpoon : MonoBehaviour {

	float currentAngle;
	float destinationAngle;
	float speed = 3;
	bool canMove = false;
	bool playerReadyToLounch;
	bool goingUp;

	public Transform playerStart;
	public Transform animalSpot;

	public GameObject player;
	public GameObject newParent;
	public Rigidbody rb;

	void Start () {
		playerStart = player.transform;


		transform.rotation = Quaternion.Euler(new Vector3 (0, 0, 315));
		currentAngle = transform.rotation.eulerAngles.z;
		playerReadyToLounch = true;
		canMove = false;
		rb = player.GetComponent<Rigidbody> ();
		Debug.Log (currentAngle);
	}

	void Update () {

		if (Input.GetKeyDown (KeyCode.F1)) {
			SetToLounch ();
		} else if (Input.GetKeyDown (KeyCode.F2)) {
			Fire ();
		} else if (Input.GetKeyDown (KeyCode.F3)) {
			ResetPosition ();
		} else if (Input.GetKeyDown (KeyCode.F4)) {
			ResetPlayer ();
		}
	}

	void FixedUpdate() {

		Debug.Log (transform.rotation.eulerAngles.z);

		if (canMove) {
			currentAngle = currentAngle + (Time.deltaTime * speed);
			transform.rotation = Quaternion.Euler(new Vector3 (0, 0, currentAngle));

			if (playerReadyToLounch && currentAngle <= 300) {
				playerReadyToLounch = false;
				player.transform.SetParent (newParent.transform);
				rb.isKinematic = false;
				rb.velocity = new Vector3 (30, 30, 30);
				rb.AddTorque (new Vector3 (32, 63, 12));
			}

			if (goingUp && destinationAngle >= currentAngle) {
				canMove = false;
			} else if (!goingUp && destinationAngle <= currentAngle) {
				canMove = false;
			}
		}



	}

	void ResetPlayer() {
		rb.isKinematic = true;
		player.transform.SetParent( animalSpot.transform);
		player.transform.localPosition = new Vector3 (0, 0, 0);
		player.transform.localRotation = Quaternion.Euler(new Vector3 (315, 90, 0));
		playerReadyToLounch = true;
	}

	void SetToLounch() {
		destinationAngle = 345;
		speed = 30;
		canMove = true;
		goingUp = false;
	}

	void Fire() {
		destinationAngle = 270;
		speed = -300;
		canMove = true;
		goingUp = true;
	}

	void ResetPosition() {
		destinationAngle = 315;
		speed = 30;
		canMove = true;
		goingUp = false;
	}


}
