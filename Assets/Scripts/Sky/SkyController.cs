using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyController : MonoBehaviour {

	Transform dome;

	void Awake(){

		dome = gameObject.transform;

	}
	void Start () {
		
	}

	void Update () {
		
		dome.Rotate (Vector3.up/70.0f);
	}
}
