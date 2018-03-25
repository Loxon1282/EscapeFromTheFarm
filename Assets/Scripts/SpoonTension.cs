using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpoonTension : MonoBehaviour {

	LaunchingControllerKOT Controller;

	void Awake(){

		Controller = GetComponentInParent<LaunchingControllerKOT> ();
	//	Controller.spoonRotation = transform.rotation.z;

	}
	void Start () {
		InvokeRepeating ("TensionEffect", 1, 0.02f);
	}


	void Update () {
		if (Controller.spoonRotation < 285)
			Controller.spoonRotation = 285;
		if (Controller.spoonRotation > 345)
			Controller.spoonRotation = 345;
		if (Input.GetKey (KeyCode.A)) {
			Controller.spoonRotation+= 0.08f;
			Debug.Log ("TOUCH A");
		
		}
		Debug.Log (Controller.spoonRotation);
	}
	void FixedUpdate(){

		if (transform.rotation.eulerAngles.z >= 285 && transform.rotation.eulerAngles.z <= 345) {

			transform.rotation = Quaternion.Euler (new Vector3 (transform.rotation.x, transform.rotation.y, Controller.spoonRotation));
		} //else
		//	Controller.spoonRotation = transform.eulerAngles.z;
	}
	void OnMouseOver(){
		Controller.spoonRotation = Controller.spoonRotation - Input.GetAxis ("Horizontal") * 0.02f ;
		Debug.Log ("AX: " + Input.GetAxis ("Horizontal"));
	}
	void TensionEffect(){
		if (!Input.GetKey (KeyCode.A)) {
			Controller.spoonRotation = transform.rotation.eulerAngles.z;
			Controller.spoonRotation -= 0.5f;
		}
	}
}
