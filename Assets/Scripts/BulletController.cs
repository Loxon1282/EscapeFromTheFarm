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
	
	// Update is called once per frame
	void Update () {
		
	}
	public void Fire(){

		rb.AddForce (new Vector3 (1, 1,0) * force);

	}

	IEnumerator TestfireDelay(){
		yield return new WaitForSeconds (2);
		rb.isKinematic = false;
		Fire ();

	}
}
