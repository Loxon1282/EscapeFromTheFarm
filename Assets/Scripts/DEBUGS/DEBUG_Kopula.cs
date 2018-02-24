using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DEBUG_Kopula : MonoBehaviour {

	public GameObject player;
	public Material meterials;
	public Shader shader;


	void Update () {
		gameObject.transform.position = new Vector3 (player.transform.position.x, transform.position.y, transform.position.z);

	}
}
