using UnityEngine;
using System.Collections;

public class Winner : MonoBehaviour {

	public Player1 p1;
	public Player2 p2;

	public GameObject one;
	public GameObject two;
	public GameObject three;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (p1.dead == true && p2.dead) {
			if (p1.totalScore > p2.totalScore) {
				one.SetActive(true);
			}
			if (p1.totalScore < p2.totalScore) {
				two.SetActive(true);
			}
			if (p1.totalScore == p2.totalScore) {
				three.SetActive(true);
			}
		}
	
	}
}
