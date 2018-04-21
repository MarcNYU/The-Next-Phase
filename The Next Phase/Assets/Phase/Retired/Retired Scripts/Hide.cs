using UnityEngine;
using System.Collections;

public class Hide : MonoBehaviour {
	// Use this for initialization
	public GameObject p;
	private Light l;
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnTriggerEnter(Collider col){
		if (p.activeInHierarchy) {
			if (col.GetComponent<Collider> ().tag == "Wall") {
				col.gameObject.GetComponent<Renderer> ().enabled = false;
				//wall.GetComponent<Renderer> ().enabled = false;
			}
		}
	}
	void OnTriggerStay(Collider col){
		if (p.activeInHierarchy) {
			if (col.GetComponent<Collider> ().tag == "Wall") {
				col.gameObject.GetComponent<Renderer> ().enabled = false;
				//wall.GetComponent<Renderer> ().enabled = false;
			}
		}
	}
	void OnTriggerExit(Collider col){
		if (col.GetComponent<Collider> ().tag == "Wall") {
			col.gameObject.GetComponent<Renderer> ().enabled = true;
		}
	}
}
