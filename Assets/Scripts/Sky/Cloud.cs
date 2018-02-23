using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour {

	Vector3 StartPos, EndPosition;
	float speed;
	// Use this for initialization
	void Start () {
		speed = Random.Range (8.0f, 15.0f);
		StartPos = transform.position;
		EndPosition = new Vector3 (StartPos.x, StartPos.y, StartPos.z + 300.0f);
	}
	
	// Update is called once per frame
	void Update () {
			
		transform.position = Vector3.Lerp(transform.position, EndPosition, Time.deltaTime / speed);

		if (transform.position == EndPosition) {
			Vector3 temp = EndPosition;
			EndPosition = StartPos;
			StartPos = temp;
		
		}
	}
}
