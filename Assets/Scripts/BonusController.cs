using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusController : MonoBehaviour {

	Rigidbody rb;

	public float forageForce = 5.0f;
	void Start () {
		rb = GetComponent<Rigidbody> ();

	}


	void OnTriggerEnter(Collider other) {

		if (other.tag == "Forage") {
			rb.AddForce (new Vector3 (1, 0.8f, 0) * forageForce);
		}

		else if (other.tag == "Mine") {
			Destroy (gameObject);
			//Tutaj funkcja endu poziomu
		}


	}
}
