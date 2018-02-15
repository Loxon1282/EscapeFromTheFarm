using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchingController : MonoBehaviour {

	public GameObject spoon;
	public GameObject animalSpot;
	public GameObject worldTransform;
	public Vector3 launchingVelocity;
	public Vector3 launchingTorque;

	public GameObject player;
	public Rigidbody playerRb;

	// Lower: 345 = -15(in editor), Uppder: 270 = -90(in editor)
	public float upperBound;
	public float lowerBound;
	public float launchingBound;
	public float centerAngle;

	public float spoonAngle;
	public float destinationAngle;
	public float spoonSpeed;

	public float fastSpeed;
	public float slowSpeed;

	public bool canMove;
	public bool canLaunch;
	public bool movingUpwards;

	void Awake() {
		canMove = false;
		canLaunch = false;

		lowerBound = 345;
		upperBound = 270;
		centerAngle = 270 + ((lowerBound - upperBound) / 2);
		launchingBound = 300;

		playerRb = player.GetComponent<Rigidbody> ();
	}

	void Start () {
		spoon.transform.rotation = Quaternion.Euler (new Vector3 (0, 0, centerAngle));
		spoonAngle = spoon.transform.rotation.eulerAngles.z;

		TestingSpoonMovement ();
	}
	

	void Update () {
		if (Input.GetKeyDown (KeyCode.F1)) {
			SetSpoonToLounch ();
		} else if (Input.GetKeyDown (KeyCode.F2)) {
			FireSpoon ();
		} else if (Input.GetKeyDown (KeyCode.F3)) {
			ResetSpoonPosition ();
		} else if (Input.GetKeyDown (KeyCode.F4)) {
			ResetPlayer ();
		} else if (Input.GetKeyDown (KeyCode.F5)) {
			LaunchPlayer ();
		}
		Debug.Log (spoonAngle);
	}

	void FixedUpdate() {
		if (canMove) {

			if (canLaunch && spoonAngle <= launchingBound) {
				LaunchPlayer ();
			}

			if (movingUpwards && spoonAngle <= destinationAngle) {
				canMove = false;
				return;
			} else if(!movingUpwards && spoonAngle >= destinationAngle){
				canMove = false;
				return;
			}

			if (movingUpwards) {
				spoonAngle -= Time.deltaTime * spoonSpeed;
			} else {
				spoonAngle += Time.deltaTime * spoonSpeed;
			}

			if (movingUpwards && canLaunch && launchingBound >= spoonAngle) {
				canLaunch = false;
				LaunchPlayer ();
			}

			spoon.transform.rotation = Quaternion.Euler (new Vector3 (0, 0, spoonAngle));
		}
	}

	void ResetPlayer() {
		canLaunch = true;

		player.transform.SetParent (animalSpot.transform);
		player.transform.localPosition = new Vector3 (0, 0, 0);


		playerRb.isKinematic = true;


	}

	void LaunchPlayer() {
		player.transform.SetParent (worldTransform.transform);

		playerRb.isKinematic = false;
		playerRb.velocity = launchingVelocity;
		playerRb.AddTorque(launchingTorque);
	}

	void SetSpoonToLounch() {
		canMove = true;
		movingUpwards = false;
		destinationAngle = lowerBound;

		spoonSpeed = slowSpeed;
	}

	void FireSpoon() {
		canMove = true;
		movingUpwards = true;
		destinationAngle = upperBound;

		spoonSpeed = fastSpeed;
	}

	void ResetSpoonPosition() {
		canMove = true;
		destinationAngle = centerAngle;
		movingUpwards = (spoonAngle <= centerAngle) ? false : true	;

		spoonSpeed = slowSpeed;
	}

	void TestingSpoonMovement() {
		
	}

}
