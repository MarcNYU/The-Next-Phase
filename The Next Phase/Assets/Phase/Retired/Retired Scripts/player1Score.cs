using UnityEngine;
using System.Collections;

public class player1Score : MonoBehaviour {
	public Player1 p;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		string s = p.totalScore.ToString ();
		GetComponent<TextMesh> ().text = s;
	
	}
}
