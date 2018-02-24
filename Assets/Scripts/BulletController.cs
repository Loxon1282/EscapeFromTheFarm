using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour {

	Rigidbody rb;
	public float force = 5.0f;
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
		rb.isKinematic = true;
		StartCoroutine ("TestfireDelay");
	}

	public void Fire(){

		rb.AddForce (new Vector3 (1, 0.3f,0) * force);
		rb.AddTorque (new Vector3 (0, 0, 0.005f)*force/5);


	}

	IEnumerator TestfireDelay(){
		yield return new WaitForSeconds (2);
		rb.isKinematic = false;
		Fire ();

	}
	void OnMouseDown(){
		Fire ();
	}
}
