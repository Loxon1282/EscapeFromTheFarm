using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchingControllerKOT : MonoBehaviour {

	public GameObject Spoon;

	[Range(-75.0f,-10.0f)]
	public float spoonRotation;


	void Start () {
		spoonRotation = -15.0f;
	}
	

	void Update () {
		
	}
}
