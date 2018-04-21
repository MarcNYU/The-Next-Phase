using UnityEngine;
using System.Collections;

public class Cams : MonoBehaviour {

	public GameObject player1;
	public GameObject player2;
	public GameObject cam;

	public GameObject p1Cam;
	public GameObject p2Cam;

	private float t = 5f;
	

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		t -= Time.deltaTime;
		//if (t < 0f) {
		if (player1.activeInHierarchy == false ) {
			p1Cam.SetActive (true);
		}
		if (player2.activeInHierarchy == false ) {
			p2Cam.SetActive (true);
		}
		if (player1.activeInHierarchy == false && player2.activeInHierarchy == false ) {
			p1Cam.SetActive (false);
			p2Cam.SetActive (false);
			cam.SetActive (true);
		}
	}
}
