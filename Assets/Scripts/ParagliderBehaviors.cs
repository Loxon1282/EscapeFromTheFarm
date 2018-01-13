using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParagliderBehaviors : MonoBehaviour {

	Rigidbody rb;


	void Start () {
		rb = GetComponent<Rigidbody> ();
	}
	

	void Update () {
		Paragliding ();
	}

	void Paragliding(){
		if (rb.velocity.x >0) {
			if (transform.rotation.x < 0.5f&& transform.rotation.x > -0.5f) {



			} else
				print ("Not OK");

		}
	}
}
