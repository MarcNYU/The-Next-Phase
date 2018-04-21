using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Modifier : MonoBehaviour {
	
	public float speed = 20F;
	public float torque;


	void FixedUpdate () {
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);

		GetComponent<Rigidbody>().AddForce (movement * speed * Time.deltaTime);
		GetComponent<Rigidbody>().AddTorque (Vector3.up * torque * moveHorizontal);
		GetComponent<Rigidbody>().AddTorque (Vector3.up * torque * moveVertical);    
	}
	void OnTriggerEnter(Collider other){
		if (other.gameObject.tag == "IceFloor") {
			GetComponent<Collider>().material.dynamicFriction = 0;
		}
	}

	void OnTriggerExit(Collider other){
		if (other.gameObject.tag == "IceFloor") {
			GetComponent<Collider>().material.dynamicFriction = 1;

		}
	}
}
