using UnityEngine;
using System.Collections;

public class colliderChecker : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter(Collision col){
			//get rid of enemies
			// check if its an enemy
			if (col.collider.tag == "Enemy") {
				//set enemy to false
				col.collider.gameObject.GetComponent<Enemies> ().HP -= 3;
				col.collider.gameObject.GetComponent<Enemies> ().Killed();
				
			}
			

	}
}
