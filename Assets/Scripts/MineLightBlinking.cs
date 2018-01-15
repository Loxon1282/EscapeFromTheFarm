using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineLightBlinking : MonoBehaviour {

	private Light light;
	private float range;

	void Start () {
		light = gameObject.GetComponent<Light> ();
		range = light.range;
		InvokeRepeating ("Blink", Random.value, 0.7f);
	}

	void Blink() {
		if (light.range != 0) {
			light.range = 0;
		} else {
			light.range = range;
		}
	}
}
