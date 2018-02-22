using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DEBUG_FPS : MonoBehaviour {

	float deltaTime =0.0f;
	Text fps;
	// Use this for initialization
	void Start () {
		fps = GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
		deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
		fps.text = "FPS: " + 1.0f / deltaTime + " msec: " + deltaTime * 1000.0f;
	}
}
