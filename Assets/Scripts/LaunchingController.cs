using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchingController : MonoBehaviour {

	/* TUTORIAL
	 * F1 to ready the spoon for launch
	 * F2 to launch the player 
	 * F3 to set spoon to a middle angle
	 * F4 to reset players position
	 * WARNING:
	 * Before reseting position use F1 or F3 to fix spoon angle then F4
	*/

	public GameObject spoon;
	public GameObject animalSpot; // gameobject fixed to spoon position
	public GameObject worldTransform; // object that is highest in hierarhy
	public GameObject loader; // crank
	public GameObject player; // animal

	public Vector3 launchingVelocity;
	public Vector3 launchingTorque;

	private Rigidbody playerRb;

	private Quaternion playerAngle; // starting player position

	private float upperBound; // 270, -90 in editor = after launch
	private float lowerBound; // 345, -15 in editor = ready to launch

	private float launchingBound; // around 280/290
	private float centerAngle; // first position, spoon in the middle

	private float spoonAngle; // dynamic angle
	private float loaderAngle; // to rotate crank
	private float destinationAngle; // where spoon is heading
	public float spoonSpeed;
	public int loaderMultiplier;

	public float fastSpeed; // lauching spoon
	public float slowSpeed; // loading spoon

	private bool canMove;
	private bool canLaunch;
	private bool movingUpwards; // true = moving towards 270, false = moving towards 345

	[Range(-75.0f,-10.0f)]
	public float spoonRotation;

	void Awake() {
		canMove = false;
		canLaunch = true;

		lowerBound = 345;
		upperBound = 270;
		centerAngle = 270 + ((lowerBound - upperBound) / 2);
		launchingBound = 300;

		playerRb = player.GetComponent<Rigidbody> ();
	}

	void Start () {
		spoon.transform.rotation = Quaternion.Euler (new Vector3 (0, 0, centerAngle));

		playerAngle = player.transform.localRotation;

		loaderAngle = loader.transform.rotation.eulerAngles.x;
		spoonAngle = spoon.transform.rotation.eulerAngles.z;
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
		}
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
				loaderAngle -= Time.deltaTime * spoonSpeed * loaderMultiplier;
				spoonAngle -= Time.deltaTime * spoonSpeed;
			} else {
				loaderAngle += Time.deltaTime * spoonSpeed * loaderMultiplier;
				spoonAngle += Time.deltaTime * spoonSpeed;
			}
	
			
			if (movingUpwards && canLaunch && launchingBound >= spoonAngle) {
				canLaunch = false;
				LaunchPlayer ();
			}

			spoon.transform.rotation = Quaternion.Euler (new Vector3 (0, 0, spoonAngle));
			loader.transform.rotation = Quaternion.Euler (new Vector3 (loaderAngle , 270, 270));

		}
	}


	// Resets player position after launch
	void ResetPlayer() {
		canLaunch = true;

		player.transform.SetParent (animalSpot.transform);
		player.transform.localPosition = new Vector3 (0, 0, 0);

		playerRb.isKinematic = true;

		player.transform.localRotation = playerAngle;
	}

	// Launches player :3
	void LaunchPlayer() {
		player.transform.SetParent (worldTransform.transform);

		playerRb.isKinematic = false;
		playerRb.velocity = launchingVelocity;
		playerRb.AddTorque(launchingTorque);
	}

	// Sets spoon to lowest position (370)
	void SetSpoonToLounch() {
		canMove = true;
		movingUpwards = false;
		destinationAngle = lowerBound;

		spoonSpeed = slowSpeed;
	}

	// Sets spoon to highets position (260)
	void FireSpoon() {
		canMove = true;
		movingUpwards = true;
		destinationAngle = upperBound;

		spoonSpeed = fastSpeed;
	}

	// Sets spoon to a middle position
	void ResetSpoonPosition() {
		canMove = true;
		destinationAngle = centerAngle;
		movingUpwards = (spoonAngle <= centerAngle) ? false : true	;

		spoonSpeed = slowSpeed;
	}
	void OnMouseDown(){
		FireSpoon ();

	}
}
