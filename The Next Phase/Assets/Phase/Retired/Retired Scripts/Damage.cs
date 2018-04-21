using UnityEngine;
using System.Collections;

public class Damage : MonoBehaviour {
	public GameObject core;
	public Material mat;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter(Collision col){
		if (col.collider.tag == "Bullet" || col.collider.tag == "STBullet") {
			Debug.Log ("Damage");
			mat.color = Color.Lerp (Color.red, Color.gray, 1f); 
		}
	}
}
