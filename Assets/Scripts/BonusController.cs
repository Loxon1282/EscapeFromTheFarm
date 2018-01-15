using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusController : MonoBehaviour {

	Rigidbody rb;

	public float forageForce = 5.0f;
	bool baloon = false;
	GameObject baloonObj;
	void Start () {
		rb = GetComponent<Rigidbody> ();

	}
	void Update(){
		if (baloon) {
			BallonBonus ();

		}
	}


	void OnTriggerEnter(Collider other) {

		if (other.tag == "Forage") {
			rb.AddForce (new Vector3 (1, 0.8f, 0) * forageForce);
		}

		else if (other.tag == "Mine") {
			Destroy (gameObject);
			//Tutaj funkcja endu poziomu
		}

		else if (other.tag == "Baloon") {
			
			baloon = true;
			StartCoroutine ("BaloonTime");

			baloonObj = other.gameObject;

		}




	}
	IEnumerator BaloonTime(){
		yield return new WaitForSeconds (2);
		baloon = false;
		Destroy (baloonObj);
	}

	void BallonBonus(){
		baloonObj.transform.position = new Vector3(transform.position.x,transform.position.y+baloonObj.transform.localScale.y,transform.position.z);
		rb.AddForce (new Vector3 (0, 5.0f, 0));
	}

	public void FartPower(float AmountOfGas){
		

	}


}
